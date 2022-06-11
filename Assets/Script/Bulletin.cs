using System.Collections.Generic;
using UnityEngine;



public class Bulletin : MonoBehaviour, IClicked
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
}
