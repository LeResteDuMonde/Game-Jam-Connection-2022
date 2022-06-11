using UnityEngine;

public class LocationManager : MonoBehaviour
{
	private LocationData data;
	[SerializeField] private SpriteRenderer background;

	[SerializeField] private GameObject collectible;

	private MapManager mM;
	private Inventory inventory;

	#region instance

	public static LocationManager instance;

	void Awake()
	{
		instance = this;
	}

	#endregion

	private void Start()
	{
		mM = MapManager.instance;
		inventory = Inventory.instance;
		SetData();
		SpawnCollectible();
	}

	public void SetData()
	{
		data = mM.GetLocation();
		background.sprite = data.background;
	}

	void SpawnCollectible()
	{
		if (data.collectible != null && !inventory.IsInInventory(gameObject))
		{
			GameObject newcollectible = Instantiate(collectible);
			newcollectible.transform.SetParent(gameObject.transform);
			newcollectible.GetComponent<Collectible>().SetData(data.collectible);
		}
	}
}
