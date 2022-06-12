using UnityEngine;

public class Connection
{
	public GameObject bulletin;
	public Rope rope;
	public ConnectionType type;
	
	public Connection(GameObject newBulletin, ConnectionType newType, Rope rope)
	{
		bulletin = newBulletin;
		type = newType;
		this.rope = rope;

	}
}
public class ConnectionSerial{
	public string character;
	public ConnectionType type;
	public ConnectionSerial(Connection connection){
		//character = connection.bulletin.GetComponent<Bulletin>().charName;
		type = connection.type;
	}
	public void serialize(){

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

	[SerializeField] Animator noteBookAnimator;
	[SerializeField] GameObject enableAfterAnimation;

	private bool isDrawingString = false;
	public GameObject ropePrefab;
	private Rope rope;


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

	private void OnDisable()
	{
		enableAfterAnimation.SetActive(false);
	}

	public void CreateString(GameObject bulletin)
	{
		//Debug.Log("creation");
		currentBulletin = bulletin;
		stringCursor.SetActive(true);
		isDrawingString=true;
		rope = Instantiate(ropePrefab, gameObject.transform).GetComponent<Rope>();
		rope.setOrigine(bulletin.gameObject.transform.position);

	}

	public void CancelString()
	{
//		Debug.Log("destruction");
		isDrawingString=false;

		GameObject hoveredItem = mC.GetHoveredItem();

		if (hoveredItem != null && hoveredItem != currentBulletin)
		{
			currentBulletin.TryGetComponent(out Bulletin bulletin);
			bulletin?.DebugBulletin();
			Connection newConnection = new Connection(hoveredItem, currentConnectionType, rope);
			bulletin?.AddConnection(newConnection);
		}else{

			Destroy(rope?.gameObject);
		}

		stringCursor.SetActive(false);
	}
	public void Update(){
		if (noteBookAnimator.GetCurrentAnimatorStateInfo(0).normalizedTime > 1)
		{
			enableAfterAnimation.SetActive(true);
		}
		if (isDrawingString){
//			Debug.Log("update");
			rope?.setRope(MouseControls.instance.MousePosition());

		}
	}

	public void SwapConnectionType()
	{
		currentConnectionType++;
		switch (currentConnectionType)
		{
			case ConnectionType.Love:
				currentConnectionType = ConnectionType.Hate;
				break;
			case ConnectionType.Hate:
				currentConnectionType = ConnectionType.Shit;
				break;
			case ConnectionType.Shit:
				currentConnectionType = ConnectionType.Love;
				break;
			default:
				break;
		}
	}
}
