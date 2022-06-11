using UnityEngine;
using UnityEngine.SceneManagement;

public class MapManager : MonoBehaviour
{	
	private bool isBulletinBoardOpen;
	public GameObject bulletinParent;
	private bool isLocationOpen;
	private string openScene;

	#region instance

	public static MapManager instance;

	void Awake()
	{
		SceneManager.LoadScene("BulletinBoard", LoadSceneMode.Additive);
		instance = this;

	}
	#endregion

	public void OpenBulletinBoard()
	{
		if (!isBulletinBoardOpen) { 
			isBulletinBoardOpen = true;

			bulletinParent.active = true;
		}
		
	}

	public void CloseBulletinBoard()
	{
		if (isBulletinBoardOpen){
			//SceneManager.UnloadSceneAsync("BulletinBoard");
			isBulletinBoardOpen = false;
			bulletinParent.active = false;
		}
	}

	public void LoadLocation(string scene)
	{
		if (!isBulletinBoardOpen && !isLocationOpen)
		{
			SceneManager.LoadScene(scene, LoadSceneMode.Additive);
			openScene = scene;
			isLocationOpen = true;
		}
	}
	public void CloseLocation()
	{
		if (!isBulletinBoardOpen && isLocationOpen)
		{
			SceneManager.UnloadSceneAsync(openScene);
			isLocationOpen = false;
		}
	}
}
