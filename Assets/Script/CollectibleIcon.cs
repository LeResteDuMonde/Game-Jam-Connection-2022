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
}
