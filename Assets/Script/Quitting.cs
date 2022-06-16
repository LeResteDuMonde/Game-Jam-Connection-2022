using UnityEngine.SceneManagement;
using UnityEngine;

public class Quitting : MonoBehaviour
{
	[SerializeField] GameObject quittingPanel;

	public static Quitting instance;
	void Awake()
	{
		instance = this;
	}

	public void TogglePanel()
	{
		quittingPanel.SetActive(!quittingPanel.activeSelf);
		MapManager.instance.EnableLocationChange(!quittingPanel.activeSelf);
	}

	public bool isQuitting()
	{
		return quittingPanel.activeSelf;
	}

	public void QuitGame()
	{
		if (isQuitting())
		{
			SceneManager.LoadScene("StartingScene");
		}
	}
}
