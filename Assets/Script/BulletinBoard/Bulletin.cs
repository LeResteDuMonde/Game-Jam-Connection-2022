using System.Collections.Generic;
using UnityEngine;



public class Bulletin : MonoBehaviour, IClicked, IHovered
{
	[SerializeField] private CharacterData data;
	[SerializeField] public List<Connection> connections;
	public Rope rope;

	private BulletinBoardManager bbM;

	private void Start()
	{
		connections = new List<Connection>();
		bbM = BulletinBoardManager.instance;
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
	
	
	public void AddConnection(Connection newConnection)
	{
		
		foreach (var connection in connections)
		{	
			//test si la connection existe déjà
			if(connection.bulletin == newConnection.bulletin) { 
				//si oui, on la supprime
				Destroy(connection.rope.gameObject);
				connections.Remove(connection); 
				//si c'est le meme type, on s'arrete
				if (connection.type == newConnection.type) {
					Destroy(newConnection.rope.gameObject);
					return;	
				}
				break;
			}
		}
		this.rope = rope;
		//on check si c'est une deuxieme connection : 
		Bulletin aConnecter = newConnection.bulletin.GetComponent<Bulletin>();
		
		foreach (var connection in aConnecter.connections){
			if (connection.bulletin.GetComponent<Bulletin>() == this){
			//	Debug.Log("secondary Connection");
				newConnection.rope.setSecondRope();
				break;
			}
		}
		newConnection.rope.forceLength(newConnection.bulletin.gameObject.transform.position);
		Debug.Log(newConnection.type + " de " + data.characterName + " à " + aConnecter.data.characterName);
		connections.Add(newConnection);
		//Debug.Log("there is : " + connections.Count + " connections");
		
	}

	public void onHover()
	{
	}

	public void onUnhover()
	{
	}
}
