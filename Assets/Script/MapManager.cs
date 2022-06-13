using UnityEngine;
using UnityEngine.SceneManagement;

public class MapManager : MonoBehaviour
{	
	public GameObject bulletinParent;
	private bool isLocationOpen;
	private string openScene;
	[SerializeField] private AudioClip locationTransitionSound;
	[SerializeField] private LocationData location;

	#region instance

	public static MapManager instance;

	void Awake()
	{
		SceneManager.LoadScene("BulletinBoard", LoadSceneMode.Additive);
		instance = this;

	}
	#endregion

	public void ToggleBulletinBoard()
	{
		bulletinParent.SetActive(!bulletinParent.activeSelf);
		Camera mainCamera = GameObject.FindGameObjectWithTag("MainCamera").GetComponent<Camera>();
		bulletinParent.transform.position = 
			new Vector3 (mainCamera.transform.position.x, mainCamera.transform.position.y,transform.position.z);
	}

	public bool IsBulletinBoardOpen() {
		return bulletinParent.activeSelf;
	}

	public void LoadLocation(string scene)
	{
		if (!IsBulletinBoardOpen() && !isLocationOpen)
		{
			SceneManager.LoadScene(scene, LoadSceneMode.Additive);
			openScene = scene;
			isLocationOpen = true;
			AudioManager.instance.PlayClip(locationTransitionSound);
		}
	}
	public void CloseLocation()
	{
		if (!IsBulletinBoardOpen() && isLocationOpen)
		{
			SceneManager.UnloadSceneAsync(openScene);
			isLocationOpen = false;
		}
	}

	public void SetLocation(LocationData newLocation)
	{
		location = newLocation;
	}

	public LocationData GetLocation()
	{
		return location;
	}
}
