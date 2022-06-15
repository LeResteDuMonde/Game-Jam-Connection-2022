using UnityEngine;

public class ChooseLanguage : MonoBehaviour
{
	[SerializeField] private SpriteRenderer introLetter;
	[SerializeField] private Sprite introLetterFR;
	[SerializeField] private Sprite introLetterEN;
	[SerializeField] private AudioClip paperSound;

	private void Start()
	{
		UpdateIntroLetter();
	}

	public void ChangeLanguage(string language)
	{
		AudioManager.instance.PlayClip(paperSound);

		PlayerPrefs.SetString("language", language);
		UpdateIntroLetter();
	}

	private void UpdateIntroLetter()
	{
		switch (PlayerPrefs.GetString("language", "French"))
		{
			case "French":
				introLetter.sprite = introLetterFR; break;
			case "English":
				introLetter.sprite = introLetterEN; break;
		}
	}
}
