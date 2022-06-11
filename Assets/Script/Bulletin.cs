using System.Collections.Generic;
using UnityEngine;



public class Bulletin : MonoBehaviour, IClicked, IHovered
{
	[SerializeField] private string characterName;
	[SerializeField] private List<Connection> connections;
	private static int idC;
	private int id;

	private BulletinBoardManager bbM;

	static Bulletin()
	{
		idC = 0;
	}
	public Bulletin()
	{
		id = idC;
		idC++;
	}

	private void Start()
	{
		connections = new List<Connection>();
		bbM = BulletinBoardManager.instance;
	}

	public int GetId()
	{
		return id;
	}
	public void onClicked()
	{
		bbM.CreateString(gameObject);
	}

	public void onCancelClicked()
	{
		bbM.CancelString();
	}

	public void update(){
		
	}
	public void AddConnection(Connection newConnection)
	{
		foreach (var connection in connections)
		{
			if(connection.bulletin == newConnection.bulletin) { connections.Remove(connection); break; }
		}

		connections.Add(newConnection);
		Debug.Log("there is : " + connections.Count + " connections");
		
	}

	public void DebugBulletin()
	{
		Debug.Log(id);
	}

	public void onHover()
	{
	}

	public void onUnhover()
	{
	}
}
