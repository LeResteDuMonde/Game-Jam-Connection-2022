using UnityEditor;

[CustomEditor(typeof(FinishButton))]
public class FinishButtonEditor : Editor
{
	public override void OnInspectorGUI()
	{
		FinishButton finishButton = (FinishButton)target;
		DrawDefaultInspector();
	}
}
