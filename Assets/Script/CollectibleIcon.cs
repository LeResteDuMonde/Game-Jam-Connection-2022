using UnityEngine;
using UnityEngine.UI;

public class CollectibleIcon : MonoBehaviour
{
	[SerializeField] private CollectibleData data;
	public void SetData(CollectibleData newData)
	{
		data = newData;
		GetComponent<Image>().sprite = data.sprite;
	}

	public void ShowAlternateSprite()
	{
		GameObject alternateSprite = GameObject.Find("AlternativeSprite");
		Image image = alternateSprite.GetComponent<Image>();

		Sprite inventorySprite = LocalizationManager.instance.GetLanguage() == "French" ? data.frenchSprite : data.englishSprite;
		if (inventorySprite != null)
		{
			
			image.enabled = true;
			image.sprite = inventorySprite;
			alternateSprite.SetActive(true);
		}
		else
		{
			image.enabled = false;
		}
	}
}
