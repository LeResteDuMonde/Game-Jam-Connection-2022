using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.InputSystem.LowLevel;
using UnityEngine.SceneManagement;

public class MouseControls : MonoBehaviour
{
	[SerializeField] private Camera mainCamera;
	[SerializeField] private InputAction mouseClick;

	[SerializeField] private GameObject hoveredItem;
	[SerializeField] private GameObject clickedItem;

	[SerializeField] private float hotSpotXEnumarator = 2;
	[SerializeField] private float hotSpotYEnumarator = 6;

	private bool isHovering;

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
		return SceneManager.GetActiveScene().name == "StartingScene" ||
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
		ClickHoverCheck();
		HoverCheck();
	}

	private void ClickHoverCheck()
	{
		Ray ray = mainCamera.ScreenPointToRay(Mouse.current.position.ReadValue());
		RaycastHit2D hit2D = Physics2D.GetRayIntersection(ray);

		if (hit2D.collider != null)
		{
			GameObject go = hit2D.collider.gameObject;
			if (IsActive() && go.TryGetComponent(out IClicked clicked))
			{
				ChangeCursor(cursorHover);
			}
			else
			{
				if(hoveredItem == null) ChangeCursor(cursor);
			}
		}
		else
		{
			if (hoveredItem == null) ChangeCursor(cursor);
		}
	}
	private void HoverCheck()
	{
		Ray ray = mainCamera.ScreenPointToRay(Mouse.current.position.ReadValue());
		RaycastHit2D hit2D = Physics2D.GetRayIntersection(ray);

		if (hit2D.collider != null)
		{
			GameObject go = hit2D.collider.gameObject;
			if (IsActive() && go.TryGetComponent(out IHovered hover))
			{
				if (hoveredItem != go)
				{
					hover.onHover();
					UnHoverOldItem();
					hoveredItem = go;
				}
			}
			else
			{
				UnHoverOldItem();
			}
		}
		else
		{
			UnHoverOldItem();
		}
	}
	private void UnHoverOldItem()
	{
		if (hoveredItem != null && hoveredItem.TryGetComponent(out IHovered oldHover)) {
			oldHover.onUnhover();
		};
		if (hoveredItem != null) //ChangeCursor(cursor);
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
	public void CursorHoverAnimation(bool hover)
	{
		//Debug.Log(hover ? "hover" : "unhover");
		ChangeCursor(hover ? cursorHover : cursor);
		isHovering = hover;
	}

	private void ChangeCursor(Texture2D cursorType)
	{
		if (!isHovering)
		{
			//Debug.Log(cursorType.name);
			Vector2 hotspot = new Vector2(cursorType.width / hotSpotXEnumarator, cursorType.height / hotSpotYEnumarator);
			Cursor.SetCursor(cursorType, hotspot, CursorMode.Auto);
		}
	}
}
