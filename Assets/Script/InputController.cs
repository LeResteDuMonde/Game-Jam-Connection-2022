using UnityEngine;
using UnityEngine.InputSystem;

public class InputController : MonoBehaviour
{
	[SerializeField] private InputAction openBulletinBoard;
	[SerializeField] private InputAction closeBulletinBoard;
	[SerializeField] private InputAction openInventory;

	private MapManager mM;
	private Inventory inventory;

	private void Start()
	{
		mM = MapManager.instance;
		inventory = Inventory.instance;
	}

	private void OnEnable()
	{
		openBulletinBoard.Enable();
		openBulletinBoard.performed += OpenBulletinBoard;

		closeBulletinBoard.Enable();
		closeBulletinBoard.performed += CloseBulletinBoard;

		openInventory.Enable();
		openInventory.performed += OpenInventory;
	}

	private void OnDisable()
	{
		openBulletinBoard.performed -= OpenBulletinBoard;
		openBulletinBoard.Disable();

		closeBulletinBoard.performed -= CloseBulletinBoard;
		closeBulletinBoard.Disable();

		openInventory.performed -= OpenInventory;
		openInventory.Disable();
	}

	private void OpenBulletinBoard(InputAction.CallbackContext context)
	{
		mM.OpenBulletinBoard();
		inventory.CloseIventory();
	}

	private void CloseBulletinBoard(InputAction.CallbackContext context)
	{
		mM.CloseLocation();
		mM.CloseBulletinBoard();
		inventory.CloseIventory();
	}

	private void OpenInventory(InputAction.CallbackContext context)
	{
		inventory.OpenIventory();
	}
}
