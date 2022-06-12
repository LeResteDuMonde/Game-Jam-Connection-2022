using System.Collections.Generic;
using UnityEngine;



public class Bulletin : MonoBehaviour, IClicked, IHovered
{
	[SerializeField] private CharacterData data;
	[SerializeField] public List<Connection> connections;
	private int id;

	private BulletinBoardManager bbM;

	private void Start()
	{
		connections = new List<Connection>();
		bbM = BulletinBoardManager.instance;
	}

	public int GetId()
	{
		return id;
	}

    public CharacterData GetData() {
        return data;
    }

	public void onClicked()
	{
		bbM.CreateString(gameObject);
	}

	public void onCancelClicked()
	{
		bbM.CancelString();
	}
	public Rope rope;
	
	public void AddConnection(Connection newConnection)
	{
		
		foreach (var connection in connections)
		{	
			//test si la connection existe déjà
			if(connection.bulletin == newConnection.bulletin) { 
				Debug.Log("meme connection");
				//si oui, on la supprime
				Destroy(connection.rope.gameObject);
				connections.Remove(connection); 
				//si c'est le meme type, on s'arrete
				if (connection.type == newConnection.type) {
					Debug.Log("suppression d'une connection");
					Destroy(newConnection.rope.gameObject);
					return;	
				}
				break;
			}
		}
		this.rope = rope;
		//on check si c'est une deuxieme connection : 
		Bulletin aConnecter = newConnection.bulletin.GetComponent<Bulletin>();
		Debug.Log("ids : ");
		Debug.Log(aConnecter.id);
		Debug.Log(id);
		foreach (var connection in aConnecter.connections){
			if (connection.bulletin.GetComponent<Bulletin>() == this){
				Debug.Log("secondary Connection");
				newConnection.rope.setSecondRope();
				break;
			}
		}
		newConnection.rope.forceLength(newConnection.bulletin.gameObject.transform.position);
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
