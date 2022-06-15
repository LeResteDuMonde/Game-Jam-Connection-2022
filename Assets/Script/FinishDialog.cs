using UnityEngine;
using UnityEngine.SceneManagement;

public class FinishDialog : MonoBehaviour
{
    [SerializeField] private GameObject finishButton;
    [SerializeField] private GameObject confirm;
    [SerializeField] private GameObject yes;
    private Camera mainCamera;
    [SerializeField] private Transform endSceneBackgroundPosition;

    void OnEnable() {
        finishButton.SetActive(true);
        confirm.SetActive(false);
    }

    public void OnClickedFinish() {
        finishButton.SetActive(false);
        confirm.SetActive(true);
        yes.SetActive(false); yes.SetActive(true); // dont ask why.. don t work correctly otherwise
    }

    public void OnClickedNo() {
        confirm.SetActive(false);
        finishButton.SetActive(true);
    }

    public void OnClickedYes() {
        // Turn off help box
        HelpBox.instance.gameObject.SetActive(false);

        // Turn off some interactions
        InputController.instance.OnDisable();

        // Load end scene
        SceneManager.LoadScene("EndingScene", LoadSceneMode.Additive);
        mainCamera = GameObject.FindGameObjectWithTag("MainCamera").GetComponent<Camera>();
        mainCamera.transform.position = new Vector3(endSceneBackgroundPosition.transform.position.x, endSceneBackgroundPosition.transform.position.y, mainCamera.transform.position.z);
        BulletinBoardManager.instance.transform.parent.gameObject.SetActive(false);
    }
}
