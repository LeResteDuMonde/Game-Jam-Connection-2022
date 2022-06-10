using UnityEngine;

public class Location : MonoBehaviour, IClicked
{
	[SerializeField] private string sceneToLoad;

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
		mM.LoadLocation(sceneToLoad);
	}
}
