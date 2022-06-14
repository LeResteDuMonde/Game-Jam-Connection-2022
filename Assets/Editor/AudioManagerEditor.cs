using UnityEditor;
using UnityEngine;

[CustomEditor(typeof(AudioManager))]
public class AudioManagerEditor : Editor
{
	public override void OnInspectorGUI()
	{
		AudioManager audioManager = (AudioManager)target;
		DrawDefaultInspector();

		if (GUILayout.Button("Change Music"))
		{
			audioManager.ChangeMusic(audioManager.testMusic);
		}
	}
}