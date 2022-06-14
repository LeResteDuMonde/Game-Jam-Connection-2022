using System.Collections.Generic;
using UnityEngine;
using System;
public class Rope : MonoBehaviour
{
	public GameObject ropepartFab;

	private List<Vector3> nodes;
	private List<Vector3> nodesOld;
	private List<RopePart> rp;
	private int nodeMini = 3;
	private int nbnode =0;
	public Sprite arrow;

	[Header("rigidity Settings")]
	private float maxDist = 0.1f;
	private float maxDistSecondRopeMultiplier = 1f;
	private float cstSecondRope = 4f;
	private float mag = 1f;
	private float magSecondRopeMultiplier = 1f;
	public Rope (){
		nodes  = new List<Vector3>();
		nodesOld  = new List<Vector3>();
		rp  = new List<RopePart>();
	}

	void addNode(){
	nbnode++;
	nodes.Add(nodes[nbnode-2]+Vector3.forward*maxDist*0.8f);
	nodesOld.Add(nodes[nbnode-2]+Vector3.forward*maxDist*0.8f);
	rp.Add(Instantiate(ropepartFab, gameObject.transform).GetComponent<RopePart>());
	}
	bool removeNode(){

		if (nbnode>3){

			nodes.RemoveAt(nbnode-1);
			nodesOld.RemoveAt(nbnode-1);
			Destroy(rp[nbnode-2].gameObject);
			rp.RemoveAt(nbnode-2);

			nbnode--;
			return true;
		}
		return false;
	}

	float vkill = 0f;
	Vector3 origine = Vector3.zero;
   public void setOrigine(Vector3 origine){
		nbnode=nodeMini;
	   this.origine = origine;
		//Debug.Log("awake");
	for (int i=0; i<nodeMini; i++){
		nodes.Add(origine);
		nodesOld.Add(origine);
	}
	for (int i=0;i<nbnode-1;i++){
		rp.Add(Instantiate(ropepartFab, this.gameObject.transform).GetComponent<RopePart>());
		
	}
   }
	private float cst = 1f; 
   public void forceLength(Vector3 m){
		m.z = 0;
		origine.z = 0;
		while (removeNode());
	//Debug.Log("magnitude");
	//Debug.Log(m.magnitude);
	while((m-origine).magnitude - 2.3f*nbnode*maxDist + cst > nodeMini*maxDist){
		addNode();
		}
	for (int i=0;i<100;i++){
		setRope(m);
	}
		//put the arrow
		AddArrow();
   }
   
   public void setSecondRope(){
	   mag *= magSecondRopeMultiplier; 
	   maxDist *= maxDistSecondRopeMultiplier;
	   cst *= cstSecondRope;
   }
	public void setRope(Vector3 m)
	{
		UpdateColor();
		m.z = 0;
		origine.z = 0;
	    //Debug.Log(m - origine);
	if ((m-origine).magnitude - 2.3f*nbnode*maxDist + cst> nodeMini*maxDist){
		addNode();
		vkill =0f;
	}
	if ((m-origine).magnitude - 1.2f*nbnode*maxDist < nodeMini*maxDist){
		removeNode();
	}
	for (int i=1;i<nbnode-1; i++){
		Vector3 vel = nodes[i]-nodesOld[i];
		nodesOld[i] = nodes[i];
		nodes[i] += vel * vkill - new Vector3(0,0.008f/(float)Math.Sqrt(m.magnitude)/mag,0);
		vkill = vkill + (0.95f - vkill)/10f;
	}
	for(int i=1; i<nbnode; i++){
		Vector3 diff = nodes[i]-nodes[i-1];
		float dist = (diff).magnitude;
		Vector3 changeDir = Vector3.zero;
		float error = Math.Abs(dist-maxDist);
		if (dist>maxDist){
			changeDir = diff.normalized;
		}else{

			changeDir = -diff.normalized;
		}
		Vector3 change = changeDir * error;
		if (i==1)
			nodes[i]-= change;
		else{
		nodes[i-1]+=change*0.5f;
		nodes[i] -= change*0.5f;
		}
		}
	nodes[nbnode-1] = m;
	for (int i=1;i<nbnode;i++){
		rp[i-1].setCoord(nodes[i-1],nodes[i]);
	}
	}
	public void OnDestroy(){
		for (int i=0;i<nbnode-1;i++){

		Destroy(rp[i].gameObject);
		}
	}

	private void UpdateColor()
	{
		foreach (RopePart ropePart in rp)
		{
			ropePart.SetColor();
		}
	}

	private void AddArrow()
	{
		/*
		SpriteRenderer sr = rp[(int)nbnode / 2].gameObject.GetComponent<SpriteRenderer>();
		sr.sprite = arrow;
		sr.drawMode = SpriteDrawMode.Simple;
		Vector3 s = sr.gameObject.transform.localScale;
		sr.gameObject.transform.localScale = new Vector3(s.x, s.x, s.z);*/

		SpriteRenderer sr = rp[(int)nbnode / 2].gameObject.transform.GetChild(0).GetComponent<SpriteRenderer>();
		sr.enabled = true;
		sr.drawMode = SpriteDrawMode.Simple;
		sr.color = BulletinBoardManager.instance.GetColor();
		Vector3 s = sr.gameObject.transform.localScale;
		sr.gameObject.transform.localScale = new Vector3(s.x, s.x, s.z);
	}

}
