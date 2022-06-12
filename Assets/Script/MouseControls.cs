using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.InputSystem.LowLevel;

public class MouseControls : MonoBehaviour
{
	[SerializeField] private Camera mainCamera;
	[SerializeField] private InputAction mouseClick;

	[SerializeField] private GameObject hoveredItem;
	[SerializeField] private GameObject clickedItem;

	[SerializeField]
	private Texture2D cursor;
	[SerializeField]
	private Texture2D cursorHover;

	#region instance

	public static MouseControls instance;

	void Awake()
	{
		instance = this;
	}

	#endregion

	private void Start()
	{
		ChangeCursor(cursor);
	}

	private void OnEnable()
	{
		mouseClick.Enable();
		mouseClick.performed += MousePressed;
		mouseClick.canceled += MouseCancel;
	}

	private void OnDisable()
	{
		mouseClick.performed -= MousePressed;
		mouseClick.canceled -= MouseCancel;
		mouseClick.Disable();
	}

    private bool IsActive() {
        return
            !Inventory.instance.IsOpen()
            && !DialogBox.instance.IsOpen();
    }

	public Vector3 MousePosition()
	{
		Vector3 mousePosition = Mouse.current.position.ReadValue();
		Vector3 correctedPosition = mainCamera.ScreenToWorldPoint(mousePosition);

		return new Vector3(correctedPosition.x, correctedPosition.y,0);
	}

	private void FixedUpdate()
	{
        HoverCheck();
	}

	private void HoverCheck()
	{
		Ray ray = mainCamera.ScreenPointToRay(Mouse.current.position.ReadValue());
		RaycastHit2D hit2D = Physics2D.GetRayIntersection(ray);

        if (hit2D.collider != null) {
			GameObject go = hit2D.collider.gameObject;
			if (IsActive() && go.TryGetComponent<IClicked>(out IClicked hover)) {
                if(hoveredItem == null) ChangeCursor(cursorHover);
                hoveredItem = go;
			} else {
                UnHoverOldItem();
            }
		} else {
            UnHoverOldItem();
        }
	}

	private void UnHoverOldItem()
	{
		if (hoveredItem != null && hoveredItem.TryGetComponent<IHovered>(out IHovered oldHover)) {
            oldHover.onUnhover();
        };
		if (hoveredItem != null) ChangeCursor(cursor);
		hoveredItem = null;
	}

	private void MousePressed(InputAction.CallbackContext context)
	{
        if(!IsActive()) return;

		Ray ray = mainCamera.ScreenPointToRay(Mouse.current.position.ReadValue());
		RaycastHit2D hit2D = Physics2D.GetRayIntersection(ray);

		if (hit2D.collider != null)
		{
			clickedItem = hit2D.collider.gameObject;

			clickedItem.TryGetComponent<IClicked>(out var iClickedComponent);
			iClickedComponent?.onClicked();
		}
	}

	private void MouseCancel(InputAction.CallbackContext context)
	{
        if(!IsActive()) return;

		if(clickedItem != null) {
			clickedItem.TryGetComponent<IClicked>(out var iClickedComponent);
			iClickedComponent?.onCancelClicked();
		}
		clickedItem = null;
	}

	public GameObject GetHoveredItem()
	{
		return hoveredItem;
	}

	private void ChangeCursor(Texture2D cursorType)
	{
		Vector2 hotspot = new Vector2(cursorType.width / 2 + cursorType.width/8, cursorType.height / 6);
		Cursor.SetCursor(cursorType, hotspot, CursorMode.Auto);
	}
}
