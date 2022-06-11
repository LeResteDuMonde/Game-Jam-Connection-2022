using System.Collections.Generic;
using UnityEngine;

public class Inventory : MonoBehaviour
{
	private List<CollectibleData> collectibles;

	#region instance

	public static Inventory instance;

	void Awake()
	{
		instance = this;
	}

	private void Start()
	{
		collectibles = new List<CollectibleData>();
	}

	#endregion
	public void AddToInventory(GameObject collectible)
	{
		collectible.TryGetComponent(out Collectible collectibleComp);
		collectibles.Add(collectibleComp?.GetData());
	}

	public bool IsInInventory(GameObject collectible)
	{
		collectible.TryGetComponent(out Collectible collectibleComp);
		return collectibles.Contains(collectibleComp?.GetData());
	}

	public void OpenIventory()
	{

	}
}
