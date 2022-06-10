using UnityEngine;
using UnityEngine.SceneManagement;

public class Location : MonoBehaviour, IClicked
{
	[SerializeField] private string sceneToLoad;
	public void onCancelClicked()
	{
	}

	public void onClicked()
	{
		Debug.Log("click");
		SceneManager.LoadScene(sceneToLoad);
	}
}
