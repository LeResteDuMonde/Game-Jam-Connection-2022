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
		mM.SetLocation(data);
		mM.LoadLocation("Location");
	}
}
