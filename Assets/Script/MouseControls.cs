using UnityEngine;
using UnityEngine.InputSystem;

public class MouseControls : MonoBehaviour
{
	[SerializeField] private Camera mainCamera;
	[SerializeField] private InputAction mouseClick;

	[SerializeField] private GameObject hoveredItem;
	[SerializeField] private GameObject clickedItem;

	#region instance

	public static MouseControls instance;

	void Awake()
	{
		instance = this;
	}

	#endregion

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

	private void Update()
	{
		HoverCheck();
	}

	private void HoverCheck()
	{
		Ray ray = mainCamera.ScreenPointToRay(Mouse.current.position.ReadValue());
		RaycastHit2D hit2D = Physics2D.GetRayIntersection(ray);

		if (hit2D.collider != null)
		{
			GameObject go = hit2D.collider.gameObject;
			if (go.TryGetComponent<IHovered>(out IHovered hover))
			{
				if (hoveredItem != go) 
				{
					UnHoverOldItem();
					hoveredItem = go;
					hover.onHover();
				}
			}
			else { UnHoverOldItem(); }
		}
		else { UnHoverOldItem(); }
	}

	public Vector3 MousePosition()
	{
		Vector3 mousePosition = Mouse.current.position.ReadValue();
		Vector3 correctedPosition = mainCamera.ScreenToWorldPoint(mousePosition);

		return new Vector3(correctedPosition.x, correctedPosition.y,0);
	}

	private void UnHoverOldItem()
	{
		if (hoveredItem != null && hoveredItem.TryGetComponent<IHovered>(out IHovered oldHover)) { oldHover.onUnhover(); };
		hoveredItem = null;
	}
	private void MousePressed(InputAction.CallbackContext context)
	{
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
}
