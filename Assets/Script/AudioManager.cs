using System.Collections;
using UnityEditor;
using UnityEngine;
using UnityEngine.Audio;

public class AudioManager : MonoBehaviour
{
	[SerializeField] private AudioClip currentMusic;
	[SerializeField] private AudioClip mapMusic;

	[SerializeField] private AudioSource musicAudioSource;
	[SerializeField] private AudioSource atmosphereAudioSource;

	[SerializeField] private AudioMixer mainMixer;

	[SerializeField] private AudioMixerGroup soundEffectMixer;
	[SerializeField] private AudioMixerGroup dialogMixer;

	[SerializeField] private AudioClip testMusic;
	private float fadeVolume = -80;
	[SerializeField] private float musicDefaultVolume = 0;
	private float musicVolume = 0;
	private float musicVolumeLerp = 0;
	[SerializeField] private float fadeTime;
	private bool musicFading;

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
		musicAudioSource.Play();
	}

	private void Update()
	{
		if (musicFading)
		{
			SetMusicVolume(Mathf.Lerp(musicDefaultVolume, fadeVolume, musicVolumeLerp));
			musicVolumeLerp += Time.deltaTime / fadeTime;
		}
	}

	public AudioSource PlayClip(AudioClip clip, string mixer = "Sound", Vector3 pos = default(Vector3))
	{
		GameObject tempGO = new GameObject("TempAudio");
		//tempGO.tag = "Sound";
		tempGO.transform.position = pos;
		tempGO.transform.SetParent(gameObject.transform);
		AudioSource audioSource = tempGO.AddComponent<AudioSource>();
		audioSource.clip = clip;
		audioSource.outputAudioMixerGroup = soundEffectMixer;
		switch (mixer)
		{
			case "Dialog":
				audioSource.outputAudioMixerGroup = dialogMixer;
				break;
		}
		
		audioSource.Play();
		Destroy(tempGO, clip.length);
		return audioSource;
	}

	public void ChangeMusic(AudioClip newMusic)
	{
		musicFading = true;
		musicVolumeLerp = 0;

		StartCoroutine(ChangeMusicCoroutine(newMusic));
	}

	public IEnumerator ChangeMusicCoroutine(AudioClip newMusic)
	{
		yield return new WaitForSeconds(fadeTime);

		musicFading = false;

		musicVolume = musicDefaultVolume;
		SetMusicVolume(musicVolume);

		musicAudioSource.clip = newMusic;
		musicAudioSource.Play();
	}

	private void SetMusicVolume(float volume)
	{
		mainMixer.SetFloat("MusicVolume", volume);
	}

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
}
