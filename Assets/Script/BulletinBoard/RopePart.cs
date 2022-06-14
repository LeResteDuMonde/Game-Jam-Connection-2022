using UnityEngine;
using System;
public class RopePart : MonoBehaviour
{
	SpriteRenderer sp;

	private void Awake()
	{
		sp = GetComponent<SpriteRenderer>();
	}
	void Start()
	{
		//SetColor();
		Vector3 p = transform.position;
		transform.position = new Vector3(p.x,p.y,0);
	}

	private void Update()
	{
		//SetColor();
	}
	float offset = 0;
	public void setCoord(Vector3 start, Vector3 end){
		Vector3 dir = end-start;
		dir.z=0;
		float angle;
		//angle  = (float)Math.Asin(dir.y/Math.Sqrt(dir.x*dir.x,dir.y*dir.y));
		//angle *= 180f/3.14f;
		//Debug.Log(angle);
		//Debug.Log(dir);
		angle = Vector3.Angle(dir,new Vector3(1,0,0));
		if (dir.y<0){
			angle=-angle;
		}
//	    Debug.Log(angle);
//	    Debug.Log(dir);
	
		Vector3 mid = new Vector3((start.x+end.x)/2,(start.y+end.y)/2);
		mid.z = 0;
		gameObject.transform.position = mid;

		sp = GetComponent<SpriteRenderer>();
		sp.material.mainTextureOffset = new Vector2(offset,0);
		offset+=0.00f;
		gameObject.transform.rotation = Quaternion.Euler(0,0,0);
		Vector2 size = sp.bounds.size;
		float coef = size.x/dir.magnitude;
		gameObject.transform.rotation = Quaternion.Euler(0,0,angle);
		
		sp.size = new Vector2(Math.Max(sp.size.x/coef,0.1f),sp.size.y);


	}
	public void setOffset(float offset){

		float w = sp.sprite.rect.width;
		this.offset =offset*2;
		sp.material.mainTextureOffset = new Vector2(this.offset,0);
	}
	public float getLength(){

		return sp.bounds.size.x;
	}
	
	public void SetColor()
	{
		sp.color = BulletinBoardManager.instance.GetColor();
	}
}
