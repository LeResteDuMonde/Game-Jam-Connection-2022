using UnityEngine;
using System.Collections.Generic;

public class CharacterManager : MonoBehaviour
{

    public static CharacterManager instance;
    public List<GameObject> characters;

    void Start() {
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
            g.SetActive(false);
        }
    }
}
