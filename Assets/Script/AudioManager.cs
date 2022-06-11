using UnityEngine;
using UnityEngine.Audio;

public class AudioManager : MonoBehaviour
{
	[SerializeField] private AudioClip currentMusic;
	[SerializeField] private AudioSource musicAudioSource;

	[SerializeField] private AudioMixer mainMixer;
	[SerializeField] private AudioMixerGroup soundEffectMixer;

	#region instance

	public static AudioManager instance;

	void Awake()
	{
		instance = this;
	}

	#endregion

	private void Start()
	{
		musicAudioSource.clip = currentMusic;
	}

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
