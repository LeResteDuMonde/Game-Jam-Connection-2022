using UnityEngine;
using System;

[Serializable]
public class State {
    public string name;
}

[Serializable]
public class Transition {
    public string name;
    public string originState;
    public string destinationState;
}

[Serializable]
public class StateMachine : MonoBehaviour {
    public State[] states;
    public Transition[] transitions;

    private State currentState;

    public void Start() {
        currentState = states[0];
    }

    public void TriggerTransition(string transitionName) {
        foreach(var t in transitions) {
            if(t.name == transitionName && t.originState == currentState.name) {
                foreach(var s in states) {
                    if(s.name == t.destinationState) {
                        currentState = s;
                        break;
                    }
                }
                break;
            }
        }
    }
}
