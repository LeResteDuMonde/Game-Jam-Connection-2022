using UnityEngine;

public class Connection
{
	public GameObject bulletin;
	public ConnectionType type;
}

public enum ConnectionType
{
	Love,
	Hate,
	Shit
}

public class BulletinBoardManager : MonoBehaviour
{
	[SerializeField] private GameObject stringCursor;

	#region instance

	public static BulletinBoardManager instance;

	void Awake()
	{
		instance = this;
	}
	#endregion

	public void CreateString(GameObject bulletin)
	{
		stringCursor.SetActive(true);
	}

	public void CancelString()
	{
		stringCursor.SetActive(false);
	}
}
