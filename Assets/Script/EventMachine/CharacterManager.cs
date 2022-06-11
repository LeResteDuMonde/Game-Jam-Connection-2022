using UnityEngine;
using System.Collections.Generic;

public class CharacterManager : MonoBehaviour
{

    public static CharacterManager instance;
    private List<GameObject> actors;
    private List<StateMachine> loadedMachines;

    void Start() {
        actors = new List<GameObject>();
        loadedMachines = new List<StateMachine>();
    }

    void Awake() {
        instance = this;
    }


    public StateMachine GetMachine(string name) {
        foreach(StateMachine sm in loadedMachines) {
            if(sm.name == name) return sm;
        }

        // If the machine isnt yet loaded, load it
        var smJson = Resources.Load<TextAsset>("StateMachines/" + name);
        var machine = JsonUtility.FromJson<StateMachine>(smJson.text);
        machine.Start();
        loadedMachines.Add(machine);

        return machine;
    }

    public void Register(GameObject actor) {
        actors.Add(actor);
    }

    public void TriggerTransition(string transition) {
        actors.RemoveAll(s => s == null);
        foreach(GameObject g in actors) {
            var a = g.GetComponent<Character>();
            a.TriggerTransition(transition);
        }
    }

    void UpdateActors() {
        actors.RemoveAll(s => s == null);
        foreach(GameObject g in actors) {
            var a = g.GetComponent<Character>();
            //a.UpdateBehaviours();
        }
    }
}
