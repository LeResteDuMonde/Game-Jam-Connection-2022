using System;
using UnityEngine;

[Serializable]
public class State {
    public string name;
    public string? location;
}

[Serializable]
public class Transition {
    public string name;
    public string origin;
    public string destination;
}

[Serializable]
public class StateMachine {
    public string name;
    public State[] states;
    public Transition[] transitions;
    public string[] initialStates;

    private State[] currentStates;

    private State FindState(string name) {
        foreach(var s in states) {
            if (s.name == name) return s;
        }
        return null;
    }

    public void Start() {
        currentStates = new State[initialStates.Length];
        for (var i = 0; i < initialStates.Length; i++) {
            currentStates[i] = FindState(initialStates[i]);
        }
    }

    public State[] GetCurrentStates() {
        return currentStates;
    }

    public bool CheckState(string state) {
        foreach (var s in currentStates) {
            if(s.name == state) return true;
        }
        return false;
    }

    public void TriggerTransition(string transitionName) {
        foreach(var t in transitions) {
            if(t.name == transitionName) {
                for (var i = 0; i < currentStates.Length; i++) {
                    if(currentStates[i].name == t.origin) {
                        Debug.Log(name + " : leaving " + currentStates[i].name + " and entering " + t.destination);
                        currentStates[i] = FindState(t.destination);
                    }
                }
            }
        }
    }

    public override string ToString() {
        var str = "current states for " + name + " : ";
        foreach(var s in currentStates) {
            str += " " + s.name;
        }
        return str;
    }
}
