using UnityEngine;
using UnityEngine.InputSystem;

public class InputController : MonoBehaviour
{

	public static InputController instance;

	void Awake() {
		instance = this;
	}

	[SerializeField] private InputAction toggleBulletinBoard;
	[SerializeField] private InputAction toggleInventory;
	[SerializeField] private InputAction toggleHelp;
	[SerializeField] private InputAction swapConnectionType;
	[SerializeField] private InputAction quit;
	[SerializeField] private InputAction confirm;

	private MapManager mM;
	private Inventory inventory;

	private void Start()
	{
		mM = MapManager.instance;
		inventory = Inventory.instance;
	}

	public void OnEnable()
	{
		EnableInputs();
	}

	public void OnDisable()
	{
		DisableInputs();
	}

	public void EnableInputs()
	{
		quit.Enable();
		quit.performed += Quit;

		confirm.Enable();
		confirm.performed += Confirm;

		toggleBulletinBoard.Enable();
		toggleBulletinBoard.performed += ToggleBulletinBoard;

		toggleInventory.Enable();
		toggleInventory.performed += ToggleInventory;

		toggleHelp.Enable();
		toggleHelp.performed += ToggleHelp;

		swapConnectionType.Enable();
		swapConnectionType.performed += SwapConncetion;
	}
	public void DisableInputs()
	{
		confirm.performed -= Confirm;
		confirm.Disable();

		quit.performed -= Quit;
		quit.Disable();

		toggleBulletinBoard.performed -= ToggleBulletinBoard;
		toggleBulletinBoard.Disable();

		toggleInventory.performed -= ToggleInventory;
		toggleInventory.Disable();

		toggleHelp.Disable();
		toggleHelp.performed -= ToggleHelp;

		swapConnectionType.Disable();
		swapConnectionType.performed -= SwapConncetion;
	}
	private void Quit(InputAction.CallbackContext context)
	{
		if (DialogBox.instance.IsOpen()) { DialogBox.instance?.CloseDialog(); }
		else if (Inventory.instance.IsOpen()) { inventory.ToggleInventory(); }
		else if (mM.IsBulletinBoardOpen()) { mM.ToggleBulletinBoard(); }
		else if (HelpBox.instance.IsOpen()) { HelpBox.instance.ToggleHelp(); }
		else if (mM.IsLocationOpen()) { mM.CloseLocation(); }
		else { Quitting.instance.TogglePanel(); }
	}

	private void Confirm(InputAction.CallbackContext context)
	{
		Quitting.instance.QuitGame();
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

	private void SwapConncetion(InputAction.CallbackContext context)
	{
		BulletinBoardManager.instance?.SwapConnectionType();
	}
}
