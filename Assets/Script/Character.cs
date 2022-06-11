using UnityEngine;

public class Character : MonoBehaviour, IClicked
{
	public CharacterData data;
	[SerializeField] private Animator animator;
	[SerializeField] private AnimatorOverrideController overrideController;

	const string IDLE = "Idle";
	const string TALKING = "TALKING";

	private void Start()
	{
		animator.runtimeAnimatorController = data.animatorController;
	}

	public void onCancelClicked()
	{
	}

	public void onClicked()
	{
		animator.SetBool("Talking",true);
	}
}
