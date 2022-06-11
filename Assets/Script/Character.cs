using UnityEngine;

public class Character : MonoBehaviour, IClicked
{
	public CharacterData data;
	[SerializeField] private Animator animator;
	[SerializeField] private AnimatorOverrideController overrideController;

	private StateMachine machine;
    private Dialog dialog;

	private void Start()
	{
		animator.runtimeAnimatorController = data.animatorController;

        var smJson = Resources.Load<TextAsset>("StateMachines/" + data.stateMachine);
        machine = JsonUtility.FromJson<StateMachine>(smJson.text);
        machine.Start();

        dialog = LocalizationManager.instance.RetrieveDialog(data.stateMachine);
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
        DialogBox.instance.ShowNewDialog(dialog);
        // foreach (GameObject g in behaviours) {
        //     ActorBehaviour b = g.GetComponent<ActorBehaviour>();
        //     if (b.Condition(em)) {
        //         b.Interact(em);
        //     }
        // }
    }

    public void TriggerTransition(string transition) {
        machine.TriggerTransition(transition);
    }
}
