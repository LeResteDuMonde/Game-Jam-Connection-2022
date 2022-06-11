using UnityEngine;

public class Connection
{
	public GameObject bulletin;
	public ConnectionType type;

	public Connection(GameObject newBulletin, ConnectionType newType)
	{
		bulletin = newBulletin;
		type = newType;
	}
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
	[SerializeField] private GameObject currentBulletin;
	private ConnectionType currentConnectionType = ConnectionType.Love;
	private MouseControls mC;

	#region instance

	public static BulletinBoardManager instance;

	void Awake()
	{
		instance = this;
	}
	#endregion

	private void Start()
	{
		mC = MouseControls.instance;
	}

	public void CreateString(GameObject bulletin)
	{
		currentBulletin = bulletin;
		stringCursor.SetActive(true);
	}

	public void CancelString()
	{
		GameObject hoveredItem = mC.GetHoveredItem();

		if (hoveredItem != null && hoveredItem != currentBulletin)
		{
			currentBulletin.TryGetComponent(out Bulletin bulletin);
			bulletin?.DebugBulletin();
			Connection newConnection = new Connection(hoveredItem, currentConnectionType);
			bulletin?.AddConnection(newConnection);
		}

		stringCursor.SetActive(false);
	}
}
