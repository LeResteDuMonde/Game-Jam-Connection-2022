using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.SceneManagement;

public class InputController : MonoBehaviour
{
	[SerializeField] private InputAction openBulletinBoard;
	[SerializeField] private InputAction closeBulletinBoard;

	private MapManager mM;

	private void Start()
	{
		mM = MapManager.instance;
	}

	private void OnEnable()
	{
		openBulletinBoard.Enable();
		openBulletinBoard.performed += OpenBulletinBoard;

		closeBulletinBoard.Enable();
		closeBulletinBoard.performed += CloseBulletinBoard;
	}

	private void OnDisable()
	{
		openBulletinBoard.performed -= OpenBulletinBoard;
		openBulletinBoard.Disable();

		closeBulletinBoard.performed -= CloseBulletinBoard;
		closeBulletinBoard.Disable();
	}

	private void OpenBulletinBoard(InputAction.CallbackContext context)
	{
		SceneManager.LoadScene("BulletinBoard", LoadSceneMode.Additive);
	}

	private void CloseBulletinBoard(InputAction.CallbackContext context)
	{
		SceneManager.UnloadSceneAsync("BulletinBoard");
	}
}
