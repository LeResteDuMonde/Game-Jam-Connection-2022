using System.Collections;
using UnityEngine;

public class Location : MonoBehaviour, IClicked
{
	[SerializeField] private LocationData data;
	
	private MapManager mM;

	private void Start()
	{
		mM = MapManager.instance;
	}

	public void onCancelClicked()
	{
	}

	public void onClicked()
	{
		if ((data.name == "Maryse Home") && CharacterManager.instance.GetCurrentCharacter()
				 .GetComponent<Character>().GetMachine().CheckState("RejectedByZephyr"))
		{
			CaveCinematic.instance.StartCinematic();
		}
		else
		{
			mM.SetLocation(data);
			mM.LoadLocation("Location");
		}
		
	}
}
