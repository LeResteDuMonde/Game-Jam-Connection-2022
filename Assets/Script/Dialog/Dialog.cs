using System;

[Serializable]
public class Dialog {
    public DialogLine[] lines;
    public Choice[]? choice;

    public override String ToString() {
        String str = "";
        foreach(var l in lines) {
            str += l.ToString();
        }
        return str;
    }
}


[Serializable]
public class DialogLine : Dialog {
    public string line;
    public string? transition;

    public override String ToString() { return line; }
}

[Serializable]
public class Choice {
    public string text;
    public Dialog next;
}
