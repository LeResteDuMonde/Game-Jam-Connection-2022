using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class bulletinParent : MonoBehaviour
{
	// Start is called before the first frame update
	void Start()
	{
		MapManager.instance.bulletinParent = gameObject;
		gameObject.active = false;
	}

	// Update is called once per frame
	void Update()
	{ 
	}
}
