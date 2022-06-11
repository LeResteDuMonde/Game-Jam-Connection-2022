using UnityEngine;
using UnityEngine.InputSystem;

public class InputController : MonoBehaviour
{
	[SerializeField] private InputAction toggleBulletinBoard;
	[SerializeField] private InputAction closeLocation;
	[SerializeField] private InputAction toggleInventory;

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
	}

	public void OnDisable()
	{
		toggleBulletinBoard.performed -= ToggleBulletinBoard;
		toggleBulletinBoard.Disable();

        closeLocation.Disable();
        closeLocation.performed -= CloseLocation;

		toggleInventory.performed -= ToggleInventory;
		toggleInventory.Disable();
	}

	private void ToggleBulletinBoard(InputAction.CallbackContext context)
	{
		mM.ToggleBulletinBoard();
	}

	private void ToggleInventory(InputAction.CallbackContext context)
	{
		inventory.ToggleInventory();
	}

    private void CloseLocation(InputAction.CallbackContext _) {
        if (!DialogBox.instance.IsOpen()
            && !Inventory.instance.IsOpen()
            && !MapManager.instance.IsBulletinBoardOpen())
            mM.CloseLocation();
    }
}
