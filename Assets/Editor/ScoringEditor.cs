using UnityEditor;
using UnityEngine;

[CustomEditor(typeof(Scoring))]
public class ScoringEditor : Editor
{
	public override void OnInspectorGUI()
	{
		Scoring scoring = (Scoring)target;
		DrawDefaultInspector();

		if (GUILayout.Button("Get Score"))
		{
			Debug.Log(scoring.Score());
		}
	}
}
