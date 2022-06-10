using UnityEngine;
using UnityEngine.InputSystem;

public class MouseControls : MonoBehaviour
{
	[SerializeField] private Camera mainCamera;
	[SerializeField] private InputAction mouseClick;

	[SerializeField] private GameObject hoveredItem;

	/*
	#region instance

	public static MouseControls instance;

	void Awake()
	{
		instance = this;
	}

	#endregion*/

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
			GameObject go = hit2D.collider.gameObject;

			go.TryGetComponent<IClicked>(out var iClickedComponent);
			iClickedComponent?.onClicked();
		}
	}

	private void MouseCancel(InputAction.CallbackContext context)
	{
		Ray ray = mainCamera.ScreenPointToRay(Mouse.current.position.ReadValue());
		RaycastHit2D hit2D = Physics2D.GetRayIntersection(ray);

		if (hit2D.collider != null)
		{
			GameObject go = hit2D.collider.gameObject;

			go.TryGetComponent<IClicked>(out var iClickedComponent);
			iClickedComponent?.onCancelClicked();
		}
	}
}
