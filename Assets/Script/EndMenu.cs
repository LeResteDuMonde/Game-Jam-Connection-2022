using UnityEngine;
using UnityEngine.SceneManagement;

public class EndMenu : MonoBehaviour
{
    public void OnClickedQuit() {
        SceneManager.LoadScene("StartingScene");
    }

    public void OnClickedContinue() {
        HelpBox.instance.gameObject.SetActive(true);
        InputController.instance.OnEnable();
        SceneManager.UnloadSceneAsync("EndingScene");
    }
}
