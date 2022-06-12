using System.Collections;
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

    private List<Bulletin> bulletins;

    void Start() {
        var bull = BulletinBoardManager.instance.GetBulletins();
        bulletins = new List<Bulletin>();
        foreach(var b in bull) {
            bulletins.Add(b.GetComponent<Bulletin>());
        }
        FillGraphs();
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

    void FillGraphs() {
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

        for(var i = 0; i < bulletins.Count; i++) {
            var b = bulletins[i];
            foreach(var c in b.connections) {
                var j = bulletins.IndexOf(c.bulletin.GetComponent<Bulletin>());
                providedGraph[i][j] = ScoringConnectionTypeOfConnectionType(c.type);

            }
        }

        // TODO add to expected graph
    }

    int ScoreConnection(ScoringConnectionType provided, ScoringConnectionType expected) {
        switch(expected) {
            // TODO
            default:
                return 0;
        }
    }

    int ScoreGraph(ScoringConnectionType[][] provided, ScoringConnectionType[][] expected) {
        int score = 0;
        for(var i = 0; i < bulletins.Count; i++) {
            for(var j = 0; j < bulletins.Count; j++) {
                score += ScoreConnection(provided[i][j], expected[i][j]);
            }
        }
        return score;
    }

    public int Score() {
        return ScoreGraph(providedGraph, expectedGraph);
    }

}
