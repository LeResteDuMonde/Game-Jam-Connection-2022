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
	private bool wentInCave = false;
	public void onClicked()
	{
		if (!wentInCave && (data.locationName == "Maryse House") && CharacterManager.instance.getCharacter("Zephyr").GetMachine().CheckState("rejectedByZephyr"))
		{
			Debug.Log("in cave");
			wentInCave = true;
			CharacterManager.instance.TriggerTransition("goInCave");
			CaveCinematic.instance.StartCinematic();
		}
		else
		{
			mM.SetLocation(data);
			mM.LoadLocation("Location");
		}
		
	}
}
