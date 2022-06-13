using System.Collections;
using UnityEngine;

public class CaveCinematic : MonoBehaviour
{
	[SerializeField] private float caveTime = 5f;
	[SerializeField] private GameObject caveBackground;

	#region instance

	public static CaveCinematic instance;

	void Awake()
	{
		instance = this;
	}

	#endregion

	public void StartCinematic()
	{
		StartCoroutine(EnterCave());
	}

	private IEnumerator EnterCave()
	{
		caveBackground.SetActive(true);
		CameraManager.instance.MoveCamera(caveBackground.transform.position);
		yield return new WaitForSeconds(caveTime);
		CameraManager.instance.ResetCamera();
		caveBackground.SetActive(false);
	}
}
