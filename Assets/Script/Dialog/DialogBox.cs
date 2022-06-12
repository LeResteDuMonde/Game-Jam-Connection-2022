using UnityEngine;
using TMPro;
using UnityEngine.InputSystem;
using UnityEngine.UI;
using System.Collections.Generic;
using System.Collections;

public class DialogBox : MonoBehaviour
{
	public GameObject choiceButtonPrefab;

	public static DialogBox instance;

	[SerializeField] private float typeWriterDelay = 0;
	private bool typing = false;
	private bool endTypeWriterEffect;
	private 
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
		OpenDialog();
		var text = textBox.GetComponent<TextMeshProUGUI>();
		var line = currentLines[dialogPosition];

		//text.SetText(line.ToString());
		StartCoroutine(TypeWriter(text, line.ToString()));

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

	private void OpenDialog() {
		textPanel.SetActive(true);
		currentChara.GetAnimator().SetBool("Talking", true);
		//MouseControls.instance.OnDisable();
	}

	public void CloseDialog() {
		HideChoices();
		textPanel.SetActive(false);
		currentChara.GetAnimator().SetBool("Talking", false);
		//MouseControls.instance.OnEnable();
	}

	public bool IsOpen() {
		return textPanel.activeSelf;
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

		if (typing) { endTypeWriterEffect = true; } //TypeWriter Effect Unfinished
		else
		{
			// Maybe terminate
			if (dialogPosition >= 0 && currentLines[dialogPosition].terminal)
			{
				CloseDialog();
				return;
			}

			// Advance
			if (dialogPosition < currentLines.Length - 1)
			{

				dialogPosition++;

				// Maybe skip the line
				if (!(CheckInStates(currentLines[dialogPosition].inStates)))
				{
					NextLine();
					return;
				}

				ShowDialogLine();
			}

			// Show choices
			else
			{
				if (currentDialog.choices.Length > 0)
				{
					advanceDialog.Disable();
					ShowChoices(currentDialog.choices);
				}
				else
				{
					CloseDialog();
				}
			}
		}
		
	}

	private IEnumerator TypeWriter(TextMeshProUGUI textMesh, string text)
	{
		typing = true;

		for (int i = 0; i < text.Length; i++)
		{
			if (endTypeWriterEffect){
				textMesh.SetText(text); break;
			}

			textMesh.SetText(text.Substring(0,i+1));
			yield return new WaitForSeconds(typeWriterDelay);
		}
		endTypeWriterEffect = false;
		typing = false;
	}

	public void AdvanceDialog(InputAction.CallbackContext _) {
		if(IsOpen()) NextLine();
	}
}
