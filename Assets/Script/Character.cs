using UnityEngine;

public class Character : MonoBehaviour, IClicked
{
    public CharacterData data;
    [SerializeField] private Animator animator;
    [SerializeField] private AnimatorOverrideController overrideController;

    private StateMachine machine;
    private Dialog dialog;

    public StateMachine GetMachine() {
        return machine;
    }

    public Animator GetAnimator() {
        return animator;
    }

    public void Initialize() {
        dialog = LocalizationManager.instance.RetrieveDialog(data.stateMachine);

        var smJson = Resources.Load<TextAsset>("StateMachines/" + name);
        machine = JsonUtility.FromJson<StateMachine>(smJson.text);
        machine.Start();
    }

    void Start() {
        animator.runtimeAnimatorController = data.animatorController;
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
