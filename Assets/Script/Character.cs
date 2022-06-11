using UnityEngine;

public class Character : MonoBehaviour, IClicked
{
    public CharacterData data;
    [SerializeField] private Animator animator;
    [SerializeField] private AnimatorOverrideController overrideController;

    private StateMachine machine;
    private Dialog dialog;

    private void Start() {
        animator.runtimeAnimatorController = data.animatorController;

        dialog = LocalizationManager.instance.RetrieveDialog(data.stateMachine);

        machine = CharacterManager.instance.GetMachine(data.stateMachine);
        CharacterManager.instance.Register(this.gameObject);
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
    public void UpdateBehaviours() {
        // foreach (GameObject g in behaviours) {
        //     ActorBehaviour b = g.GetComponent<ActorBehaviour>();
        //     if (b.Condition(em)) b.UpdateActor(em);
        // }
    }

    void Interact() {
        Debug.Log(machine.ToString());
        DialogBox.instance.ShowNewDialog(dialog, machine);
    }

    public void TriggerTransition(string transition) {
        if(machine != null) {
            machine.TriggerTransition(transition);
        } else {
            Debug.Log(machine);
        }
    }
}
