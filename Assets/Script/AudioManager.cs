using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Audio;

public class AudioManager : MonoBehaviour
{
    [SerializeField] private AudioClip[] playlist;

	[SerializeField] private AudioMixer mainMixer;
	[SerializeField] private AudioMixerGroup soundEffectMixer;

	public AudioSource PlayClipAt(AudioClip clip, Vector3 pos = default(Vector3), string tag = "Sound")
	{
		GameObject tempGO = new GameObject("TempAudio");
		tempGO.tag = tag;
		tempGO.transform.position = pos;
		
		AudioSource audioSource = tempGO.AddComponent<AudioSource>();
		audioSource.clip = clip;
		audioSource.outputAudioMixerGroup = soundEffectMixer;
		audioSource.Play();
		Destroy(tempGO, clip.length);
		return audioSource;
	}
}
