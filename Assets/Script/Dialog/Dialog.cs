using System;

[Serializable]
public class Dialog {
    public DialogLine[] lines;
    public Choice[] choices;

    public override String ToString() {
        String str = "";
        foreach(var l in lines) {
            str += l.ToString();
        }
        return str;
    }
}


[Serializable]
public class DialogLine {
    public string[] inStates;
    public string line;
    public string? transition;
    public bool terminal;

    public override String ToString() { return line; }
}

[Serializable]
public class Choice {
    public string[] inStates;
    public string answer;
    public DialogLine[] lines;
}
