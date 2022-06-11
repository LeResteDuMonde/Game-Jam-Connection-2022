using UnityEngine;

public class Character : MonoBehaviour, IClicked
{
	public CharacterData data;
	[SerializeField] private Animator animator;
	[SerializeField] private AnimatorOverrideController overrideController;

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
