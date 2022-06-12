using UnityEngine;
using UnityEngine.SceneManagement;

public class StartMenu : MonoBehaviour
{
	public void StartGame()
	{
		SceneManager.LoadScene("Map");
	}

	public void QuitGame()
	{
		Application.Quit();
	}
}
