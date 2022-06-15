using UnityEngine;
using UnityEngine.SceneManagement;

public class EndMenu : MonoBehaviour
{
    private Camera mainCamera;
    public void OnClickedQuit() {
        SceneManager.LoadScene("Credits");
    }

    public void OnClickedContinue() {
        HelpBox.instance.gameObject.SetActive(true);
        InputController.instance.OnEnable();
        SceneManager.UnloadSceneAsync("EndingScene");
        mainCamera = GameObject.FindGameObjectWithTag("MainCamera").GetComponent<Camera>();
        mainCamera.transform.position = new Vector3(0, 0, mainCamera.transform.position.z);
        MapManager.instance.EnableLocationChange(true);
    }
}
