using UnityEngine;

public class ChooseLanguage : MonoBehaviour
{
	[SerializeField] private SpriteRenderer introLetter;
	[SerializeField] private Sprite introLetterFR;
	[SerializeField] private Sprite introLetterEN;

	public void ChangeLanguage(string language)
	{
		PlayerPrefs.SetString("language", language);
		switch (language)
		{
			case "French":
				introLetter.sprite = introLetterFR; break;
			case "English":
				introLetter.sprite = introLetterEN; break;
		}
		
	}
}
