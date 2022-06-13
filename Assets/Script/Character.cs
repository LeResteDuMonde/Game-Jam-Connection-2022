using System;
using UnityEngine;

public class Character : MonoBehaviour, IClicked
{
    public CharacterData data;
    [SerializeField] private Animator animator;
    [SerializeField] private Transform locationBackgroundPosition;
    private StateMachine machine;
    private Dialog dialog;

    private bool encountered;

    public CharacterData GetData() {
        return data;
    }

    public StateMachine GetMachine() {
        return machine;
    }

    public Animator GetAnimator() {
        return animator;
    }

    public bool WasEncountered() {
        return encountered;
    }

    public void Initialize() {
        encountered = false;

        dialog = LocalizationManager.instance.RetrieveDialog(data.stateMachine);

        try {
            var smJson = Resources.Load<TextAsset>("StateMachines/" + data.stateMachine);
            machine = JsonUtility.FromJson<StateMachine>(smJson.text);
            machine.Start();
        } catch(Exception e) {
            Debug.LogError("Couldn't load state machine for " + data.stateMachine);
            Debug.LogException(e, this);
        }
    }

    void Start() {
        animator.runtimeAnimatorController = data.animatorController;
        encountered = true;

	//TEST
	string name = CharacterManager.instance.getCharacter(GetData().name)?.GetData().name;
	Debug.Log(name);

    }

    public void onCancelClicked()
    {
    }

    public void onClicked()
    {
        animator.SetBool("Talking",true);
        Interact();
    }

    // Run each behaviour which condition is satisfied
    public void LoadInLocation(string location) {
        foreach (var s in machine.GetCurrentStates()) {
            if (s.location is string loc) {
                if (loc == location) {
                    gameObject.SetActive(true);
                    gameObject.transform.SetParent(locationBackgroundPosition);
                    gameObject.transform.localPosition = new Vector2(0, gameObject.transform.position.y);
                    if (s.position is string pos) {
                        if (pos == "left") {
                            gameObject.transform.localPosition = new Vector2(-5, gameObject.transform.position.y);
                        } else if (pos == "right") {
                            gameObject.transform.localPosition = new Vector2(5, gameObject.transform.position.y);
                        }
                    }
                    break;
                }
            }

        }
        // foreach (GameObject g in behaviours) {
        //     ActorBehaviour b = g.GetComponent<ActorBehaviour>();
        //     if (b.Condition(em)) b.UpdateActor(em);
        // }
    }

    void Interact() {
        CharacterManager.instance.SetCurrentCharacter(gameObject);
        AudioManager.instance.ChangeMusic(data.music);
        Debug.Log(machine.ToString());
        DialogBox.instance.ShowNewDialog(dialog, this);
    }

    public void TriggerTransition(string transition) {
        if(machine != null) {
            machine.TriggerTransition(transition);
        } else {
            Debug.Log(machine);
        }
    }
}
