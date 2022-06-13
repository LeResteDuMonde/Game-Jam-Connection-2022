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
		defaultPosition = transform.position;
	}

	#endregion
	public void ResetCamera()
	{
		transform.position = defaultPosition;
	} 
}