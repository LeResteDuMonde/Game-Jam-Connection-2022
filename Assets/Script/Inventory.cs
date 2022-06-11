using System.Collections.Generic;
using UnityEngine;

public class Inventory : MonoBehaviour
{
	private List<CollectibleData> collectibles;
	[SerializeField] private GameObject inventoryPanel;
	[SerializeField] private GameObject collectibleIcon;

	#region instance

	public static Inventory instance;

	void Awake()
	{
		instance = this;
	}
	#endregion
	private void Start()
	{
		collectibles = new List<CollectibleData>();
	}

	public void AddToInventory(GameObject collectible)
	{
		collectible.TryGetComponent(out Collectible collectibleComp);
		CollectibleData data = collectibleComp?.GetData();
		collectibles.Add(data);

		GameObject newCollectibleIcon = Instantiate(collectibleIcon);
		newCollectibleIcon.GetComponent<CollectibleIcon>().SetData(data);
		newCollectibleIcon.transform.SetParent(inventoryPanel.transform);
		newCollectibleIcon.transform.localScale = new Vector3(1, 1, 1);
	}

	public bool IsInInventory(GameObject collectible)
	{
		collectible.TryGetComponent(out Collectible collectibleComp);
		return collectibles.Contains(collectibleComp?.GetData());
	}

	public void OpenIventory()
	{
		inventoryPanel.SetActive(!inventoryPanel.activeSelf);
	}
	public void CloseIventory()
	{
		inventoryPanel.SetActive(false);
	}
}
