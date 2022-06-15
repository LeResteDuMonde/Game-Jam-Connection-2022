using System.Collections;
using UnityEngine;
using UnityEngine.Audio;

public class AudioManager : MonoBehaviour
{
	[SerializeField] private AudioClip mapMusic;

	[SerializeField] private AudioSource musicAudioSource;
	[SerializeField] private AudioSource atmosphereAudioSource;

	[SerializeField] private AudioMixer mainMixer;

	[SerializeField] private AudioMixerGroup soundEffectMixer;
	[SerializeField] private AudioMixerGroup dialogMixer;

	public AudioClip testMusic;
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
		musicAudioSource.clip = mapMusic;
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

	public void ResetMusic()
	{
		ChangeMusic(mapMusic);
	}
}
