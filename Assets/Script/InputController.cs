using UnityEngine;
using UnityEngine.InputSystem;

public class InputController : MonoBehaviour
{

	public static InputController instance;

	void Awake() {
		instance = this;
	}

	[SerializeField] private InputAction toggleBulletinBoard;
	[SerializeField] private InputAction closeLocation;
	[SerializeField] private InputAction toggleInventory;
	[SerializeField] private InputAction toggleHelp;
	[SerializeField] private InputAction swapConnectionType;

	private MapManager mM;
	private Inventory inventory;

	private void Start()
	{
		mM = MapManager.instance;
		inventory = Inventory.instance;
	}

	public void OnEnable()
	{
		toggleBulletinBoard.Enable();
		toggleBulletinBoard.performed += ToggleBulletinBoard;

		closeLocation.Enable();
		closeLocation.performed += CloseLocation;

		toggleInventory.Enable();
		toggleInventory.performed += ToggleInventory;

		toggleHelp.Enable();
		toggleHelp.performed += ToggleHelp;

		swapConnectionType.Enable();
		swapConnectionType.performed += SwapConncetion;
	}

	public void OnDisable()
	{
		toggleBulletinBoard.performed -= ToggleBulletinBoard;
		toggleBulletinBoard.Disable();

		closeLocation.Disable();
		closeLocation.performed -= CloseLocation;

		toggleInventory.performed -= ToggleInventory;
		toggleInventory.Disable();

		toggleHelp.Disable();
		toggleHelp.performed -= ToggleHelp;

		swapConnectionType.Disable();
		swapConnectionType.performed -= SwapConncetion;
	}

	private void ToggleBulletinBoard(InputAction.CallbackContext context)
	{
		if(!DialogBox.instance.IsOpen()
		   && !Inventory.instance.IsOpen())
		mM.ToggleBulletinBoard();
	}

	private void ToggleInventory(InputAction.CallbackContext context)
	{
		if(!MapManager.instance.IsBulletinBoardOpen())
			inventory.ToggleInventory();
	}

	private void ToggleHelp(InputAction.CallbackContext context)
	{
		HelpBox.instance.ToggleHelp();
	}

	public bool CanCloseLocation() {
		return !DialogBox.instance.IsOpen()
			&& !Inventory.instance.IsOpen()
			&& !MapManager.instance.IsBulletinBoardOpen();
	}

	private void CloseLocation(InputAction.CallbackContext _) {
		if (CanCloseLocation()) mM.CloseLocation();
	}

	private void SwapConncetion(InputAction.CallbackContext context)
	{
		BulletinBoardManager.instance?.SwapConnectionType();
	}
}
