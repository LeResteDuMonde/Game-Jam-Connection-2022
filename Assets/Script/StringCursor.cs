using UnityEngine;

public class StringCursor : MonoBehaviour
{
	private MouseControls mC;

	private void Awake()
	{
		mC = MouseControls.instance;
	}
	private void Update()
	{
		UdpateCursorPosition();
	}
	void UdpateCursorPosition()
	{
		Vector3 mousePosition = mC.MousePosition();
		gameObject.transform.position = new Vector2(mousePosition.x, mousePosition.y);
	}
}
