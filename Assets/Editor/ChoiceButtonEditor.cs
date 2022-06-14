using UnityEditor;

[CustomEditor(typeof(ChoiceButton))]
public class MenuButtonEditor : Editor
{
    public override void OnInspectorGUI()
    {
        ChoiceButton choiceButton = (ChoiceButton)target;
        DrawDefaultInspector();
    }
}
