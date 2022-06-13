using UnityEngine;
using System.Collections.Generic;

public class CharacterManager : MonoBehaviour
{

	public static CharacterManager instance;
	public List<GameObject> characters;
	private GameObject currentCharacter;

	void Start() {
		// Here is the big load
		foreach(var c in characters) {
			c.GetComponent<Character>().Initialize();
		}
	}

	void Awake() {
		instance = this;
	}


	public void TriggerTransition(string transition) {
		foreach(GameObject g in characters) {
			var a = g.GetComponent<Character>();
			a.TriggerTransition(transition);
		}
	}

	public void LoadLocationCharacters(string location) {
		foreach(GameObject g in characters) {
			var a = g.GetComponent<Character>();
			a.LoadInLocation(location);
		}
	}

	public void UnloadCharacters() {
		foreach(GameObject g in characters) {
			if(g != null) g.SetActive(false);
		}
	}

	public bool WasEncountered(CharacterData data) {
		foreach(var go in characters) {
			var c = go.GetComponent<Character>();
			if(c.GetData() == data && c.WasEncountered()) return true;
		}
		return false;
	}

	public void SetCurrentCharacter(GameObject newCharacter)
	{
		currentCharacter = newCharacter;

	}

	public GameObject GetCurrentCharacter()
	{
		return currentCharacter;
	}
}
