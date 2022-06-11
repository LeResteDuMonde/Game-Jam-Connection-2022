using UnityEngine;

public class Collectible : MonoBehaviour, IClicked
{
	[SerializeField] private CollectibleData data;
	private Inventory inventory;

	private void Start()
	{
		inventory = Inventory.instance;

		if (inventory.IsInInventory(gameObject)) { Destroy(gameObject); }
	}
	public void onCancelClicked()
	{
	}

	public void onClicked()
	{
		inventory.AddToInventory(gameObject);
		Destroy(gameObject);
	}

	public CollectibleData GetData()
	{
		return data;
	}

	public void SetData(CollectibleData newData)
	{
		data = newData;
		GetComponent<SpriteRenderer>().sprite = data.sprite;
		transform.position = data.position;
		transform.localScale = data.scale;
	}
}
