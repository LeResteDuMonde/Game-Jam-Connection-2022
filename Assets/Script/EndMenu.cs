using UnityEngine;
using UnityEngine.SceneManagement;

public class EndMenu : MonoBehaviour
{
    public void OnClickedQuit() {
        SceneManager.LoadScene("StartingScene");
    }

    public void OnClickedContinue() {
        SceneManager.UnloadSceneAsync("EndingScene");
    }
}
