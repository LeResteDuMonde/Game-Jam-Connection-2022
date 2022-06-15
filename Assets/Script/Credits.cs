using System.Collections;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.SceneManagement;

public class Credits : MonoBehaviour
{
	[SerializeField] private InputAction confirm;

	private void OnEnable()
	{
		confirm.Enable();
		confirm.performed += Confirm;
	}

	private void OnDisable()
	{
		confirm.performed -= Confirm;
		confirm.Disable();
	}

	private void Start()
	{
		//StartCoroutine(CreditsRoll());
	}

	private IEnumerator CreditsRoll()
	{
		yield return new WaitForSeconds(5f);
		SceneManager.LoadScene("StartingScene");
	}

	private void Confirm(InputAction.CallbackContext context)
	{
		SceneManager.LoadScene("StartingScene");
	}
}
