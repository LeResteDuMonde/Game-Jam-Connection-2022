using UnityEngine;
using TMPro;
using UnityEngine.InputSystem;
using UnityEngine.UI;
using System.Collections.Generic;

public class DialogBox : MonoBehaviour
{
    public GameObject choiceButtonPrefab;

    public static DialogBox instance;

    void Awake() {
        instance = this;
    }

    [SerializeField] private InputAction advanceDialog;

    private GameObject textPanel;
    private GameObject textBox;
    private GameObject buttonPanel;

    private List<GameObject> buttons;

    // Start is called before the first frame update
    void Start() {
        textPanel = transform.Find("TextPanel").gameObject;
        textBox = textPanel.transform.Find("TextBox").gameObject;
        buttonPanel = textPanel.transform.Find("ButtonPanel").gameObject;
        buttons = new List<GameObject>();
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
    private DialogLine[] currentLines;
    private int dialogPosition;

    private void ShowDialogLine() {
        textPanel.SetActive(true);
        var text = textBox.GetComponent<TextMeshProUGUI>();
        var line = currentLines[dialogPosition];
        text.SetText(line.ToString());

        // Transition
        if (line.transition is string transition) {
            CharacterManager.instance.TriggerTransition(transition);
        }
    }

    private void ShowChoices(Choice[] choices) {
        var text = textBox.GetComponent<TextMeshProUGUI>();
        text.SetText("");
        foreach (var choice in choices) {
            if(!CheckInStates(choice.inStates)) continue;

            // Create new button
            var button = Instantiate(choiceButtonPrefab, new Vector3(0, 0, 0), Quaternion.identity);
            button.transform.SetParent(buttonPanel.transform);
            button.transform.localScale = new Vector3(2, 2, 2);

            // Set its text
            var bText = button.transform.Find("Text").GetComponent<TextMeshProUGUI>();
            bText.text = choice.answer;

            // Create a listener
            button.GetComponent<Button>().onClick.AddListener(() => MakeChoice(choice.lines));

            // Add it
            buttons.Add(button);
        }
    }

    private void HideChoices() {
        // Delete buttons
        foreach(var button in buttons) {
            Destroy(button);
        }
        buttons = new List<GameObject>();
        advanceDialog.Enable();
    }

    private void MakeChoice(DialogLine[] lines) {
        HideChoices();
        currentLines = lines;
        dialogPosition = -1;
        NextLine();
    }

    private void HideDialog() {
        textPanel.SetActive(false);
        currentChara.GetAnimator().SetBool("Talking", false);
    }

    private Character currentChara;

    public void ShowNewDialog(Dialog dialog, Character chara) {
        currentDialog = dialog;
        currentLines = dialog.lines;
        dialogPosition = -1;
        currentChara = chara;
        NextLine();
    }

    private bool CheckInStates(string[] states) {
        if (states == null) return true;
        foreach (var s in states) {
            if(!(currentChara.GetMachine().CheckState(s))) return false;
        }
        return true;
    }

    private void NextLine() {
        // Maybe terminate
        if(dialogPosition >= 0 && currentLines[dialogPosition].terminal) {
            HideDialog();
            return;
        }

        // Advance
        if(dialogPosition < currentLines.Length - 1) {

            dialogPosition++;

            // Maybe skip the line
            if (!(CheckInStates(currentLines[dialogPosition].inStates))) {
                NextLine();
                return;
            }

            ShowDialogLine();
        }

        // Show choices
        else {
            if(currentDialog.choices.Length > 0) {
                advanceDialog.Disable();
                ShowChoices(currentDialog.choices);
            } else {
                HideDialog();
            }
        }
    }

    public void AdvanceDialog(InputAction.CallbackContext _) {
        NextLine();
    }
}
