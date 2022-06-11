using System;

[Serializable]
public class State {
    public string name;
}

[Serializable]
public class Transition {
    public string name;
    public string origin;
    public string destination;
}

[Serializable]
public class StateMachine {
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
                        currentStates[i] = FindState(t.destination);
                    }
                }
            }
        }
    }

    public override string ToString() {
        var str = "Current states:";
        foreach(var s in currentStates) {
            str += " " + s.name;
        }
        return str;
    }
}
