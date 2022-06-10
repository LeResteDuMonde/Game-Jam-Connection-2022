using UnityEngine;
using UnityEngine.SceneManagement;

public class MapManager : MonoBehaviour
{
    private bool isBulletinBoardOpen;
    #region instance

    public static MapManager instance;

    void Awake()
    {
        instance = this;
    }
    #endregion

    public void OpenBulletinBoard()
	{
        SceneManager.LoadScene("BulletinBoard", LoadSceneMode.Additive);
        isBulletinBoardOpen = true;
    }

    public void CloseBulletinBoard()
	{
        SceneManager.UnloadSceneAsync("BulletinBoard");
    }
}
