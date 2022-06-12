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
		GameObject alternateSprite = GameObject.Find("Alternative Sprite");
		if(data.alternativeSprite != null)
		{
			alternateSprite.GetComponent<Image>().sprite = data.alternativeSprite;
			alternateSprite.SetActive(true);
		}
	}
}
