using UnityEngine;

public class CameraManager : MonoBehaviour
{
	#region instance

	private Vector3 defaultPosition;
	public static CameraManager instance;

	void Awake()
	{
		instance = this;
	}

	private void Start()
	{
		defaultPosition = new Vector3(0,0,-10);
	}

	#endregion
	public void ResetCamera()
	{
		transform.position = defaultPosition;
	}

	public void MoveCamera(Vector3 position)
	{
		transform.position = new Vector3 (position.x, position.y, transform.position.z);
	}
}
