using UnityEngine;

public class LocationManager : MonoBehaviour
{
	private LocationData data;
	[SerializeField] private SpriteRenderer background;

	[SerializeField] private GameObject collectible;
	[SerializeField] private Camera mainCamera;

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
		mainCamera = GameObject.FindGameObjectWithTag("MainCamera").GetComponent<Camera>();
		mainCamera.transform.position = new Vector3(background.transform.position.x, background.transform.position.y, mainCamera.transform.position.z);
		mM = MapManager.instance;
		inventory = Inventory.instance;
		SetData();
		SpawnCollectible();
		CharacterManager.instance.LoadLocationCharacters(data.locationName);
	}

	void OnDisable() {
		CharacterManager.instance.UnloadCharacters();
		CameraManager.instance.ResetCamera();
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
			Debug.Log(data.collectible.collectibleName);
			if (data.collectible.collectibleName == "Key" && !CharacterManager.instance.getCharacter("Colombin").GetMachine().CheckState("keyCreated")){
				Debug.Log("abord spawn");
				return ;
			}
			Debug.Log("spawning "+data.collectible.collectibleName);
			GameObject newcollectible = Instantiate(collectible);
			newcollectible.transform.SetParent(background.transform);
			newcollectible.GetComponent<Collectible>().SetData(data.collectible);
		}
	}
}
