using UnityEngine;
using TMPro;
using UnityEngine.InputSystem;

public class DialogBox : MonoBehaviour
{
    public static DialogBox instance;

    void Awake() {
        instance = this;
    }

    [SerializeField] private InputAction advanceDialog;

    private GameObject textBox;

    // Start is called before the first frame update
    void Start() {
        textBox = transform.Find("TextBox").gameObject;
    }

    void OnEnable() {
        advanceDialog.Enable();
        advanceDialog.performed += AdvanceDialog;
    }

    void OnDisable() {
        advanceDialog.performed -= AdvanceDialog;
        advanceDialog.Disable();
    }

    private Dialog currentDialog;
    private int dialogPosition;

    private void ShowDialogLine() {
        textBox.SetActive(true);
        var text = textBox.GetComponent<TextMeshPro>();
        var line = currentDialog.lines[dialogPosition];
        text.SetText(line.ToString());

        // Transition
        if (line.transition is string transition) {
            ActorManager.instance.TriggerTransition(transition);
        }
    }

    private void HideDialog() {
        textBox.SetActive(false);
    }

    public void ShowNewDialog(Dialog dialog) {
        currentDialog = dialog;
        dialogPosition = 0;
        ShowDialogLine();
    }

    public void AdvanceDialog(InputAction.CallbackContext _) {
        if(dialogPosition >= currentDialog.lines.Length - 1) {
            // TODO choice
            HideDialog();
        } else {
            dialogPosition++;
            ShowDialogLine();
        }
    }
}
