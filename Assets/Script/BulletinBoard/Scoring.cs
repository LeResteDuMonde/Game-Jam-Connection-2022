using System.Collections.Generic;
using UnityEngine;

public enum ScoringConnectionType
{
	Love,
	Hate,
	Shit,
	None
}

public class Scoring : MonoBehaviour
{

	public static Scoring instance;

	void Awake()
	{
		instance = this;
	}

	private List<Bulletin> bulletins;

	void Start() {
		var bull = BulletinBoardManager.instance.GetBulletins();
		bulletins = new List<Bulletin>();
		foreach(var b in bull) {
			bulletins.Add(b.GetComponent<Bulletin>());
		}
		InitGraphs();
	}

	ScoringConnectionType[][] providedGraph;
	ScoringConnectionType[][] expectedGraph;

	ScoringConnectionType ScoringConnectionTypeOfConnectionType(ConnectionType ty) {
		switch(ty) {
			case ConnectionType.Love:
				return ScoringConnectionType.Love;
			case ConnectionType.Hate:
				return ScoringConnectionType.Hate;
			case ConnectionType.Shit:
				return ScoringConnectionType.Shit;
			default:
				return ScoringConnectionType.None;
		}
	}

	int FindCharaIndex(string name) {
		for(var i = 0; i < bulletins.Count; i++) {
			var data = bulletins[i].GetData();
			if(data.name == name) return i;
		}
		return -1;
	}

	void InitGraphs() {
		providedGraph = new ScoringConnectionType[bulletins.Count][];
		expectedGraph = new ScoringConnectionType[bulletins.Count][];
		for(var i = 0; i < bulletins.Count; i++) {
			providedGraph[i] = new ScoringConnectionType[bulletins.Count];
			expectedGraph[i] = new ScoringConnectionType[bulletins.Count];
			for(var j = 0; j < bulletins.Count; j++) {
				providedGraph[i][j] = ScoringConnectionType.None;
				expectedGraph[i][j] = ScoringConnectionType.None;
			}
		}

		// Fill expected graph
		for(var i = 0; i < bulletins.Count; i++) {
			var b = bulletins[i];
			for(var j = 0; j < b.GetData().connections.Count; j++) {
				var k = FindCharaIndex(b.GetData().connections[j].name);
				expectedGraph[i][k] = ScoringConnectionTypeOfConnectionType(b.GetData().connectionsType[j]);
			}
		}
	}

	void FillProvidedGraph() {
		if(bulletins == null) return;
		for(var i = 0; i < bulletins.Count; i++) {
			var b = bulletins[i];
			if(b.connections == null) continue;
			foreach(var c in b.connections) {
				var j = FindCharaIndex(c.bulletin.GetComponent<Bulletin>().GetData().name);
				providedGraph[i][j] = ScoringConnectionTypeOfConnectionType(c.type);
			}
		}
	}

	int ScoreConnection(ScoringConnectionType provided, ScoringConnectionType expected) {

		int score = 0;
		switch (provided, expected)
		{
			case (ScoringConnectionType.None, ScoringConnectionType.None): score = 0; break;
			case (ScoringConnectionType.None, ScoringConnectionType.Love): score = -1; break;
			case (ScoringConnectionType.None, ScoringConnectionType.Hate): score = -1; break;
			case (ScoringConnectionType.None, ScoringConnectionType.Shit): score = -2; break;

			case (ScoringConnectionType.Love, ScoringConnectionType.None): score = -1; break;
			case (ScoringConnectionType.Love, ScoringConnectionType.Love): score = +2; break;
			case (ScoringConnectionType.Love, ScoringConnectionType.Hate): score = -2; break;
			case (ScoringConnectionType.Love, ScoringConnectionType.Shit): score = -3; break;

			case (ScoringConnectionType.Hate, ScoringConnectionType.None): score = -1; break;
			case (ScoringConnectionType.Hate, ScoringConnectionType.Love): score = -2; break;
			case (ScoringConnectionType.Hate, ScoringConnectionType.Hate): score = +2; break;
			case (ScoringConnectionType.Hate, ScoringConnectionType.Shit): score = -3; break;

			case (ScoringConnectionType.Shit, ScoringConnectionType.None): score = -2; break;
			case (ScoringConnectionType.Shit, ScoringConnectionType.Love): score = -3; break;
			case (ScoringConnectionType.Shit, ScoringConnectionType.Hate): score = -3; break;
			case (ScoringConnectionType.Shit, ScoringConnectionType.Shit): score = +3; break;

		}

		return score;
	}

	int ScoreGraph(ScoringConnectionType[][] provided, ScoringConnectionType[][] expected) {
		int score = 0;
		if(bulletins==null) return 0;
		for(var i = 0; i < bulletins.Count; i++) {
			for(var j = 0; j < bulletins.Count; j++) {
				score += ScoreConnection(provided[i][j], expected[i][j]);
			}
		}
		return score;
	}

	public int Score() {
		FillProvidedGraph();
		return ScoreGraph(providedGraph, expectedGraph) + 21;
	}
}
