using UnityEngine;
using UnityEngine.SceneManagement;

public class FinishDialog : MonoBehaviour
{
    private GameObject finishButton;
    private GameObject confirm;

    void Start() {
        finishButton = transform.Find("FinishButton").gameObject;
        confirm = transform.Find("Confirm").gameObject;
    }

    void OnEnable() {
        if(finishButton) finishButton.SetActive(true);
        if(confirm) confirm.SetActive(false);
    }

    public void OnClickedFinish() {
        finishButton.SetActive(false);
        confirm.SetActive(true);
    }

    public void OnClickedNo() {
        confirm.SetActive(false);
        finishButton.SetActive(true);
    }

    public void OnClickedYes() {
        SceneManager.LoadScene("EndingScene", LoadSceneMode.Additive);
        BulletinBoardManager.instance.transform.parent.gameObject.SetActive(false);
    }
}
