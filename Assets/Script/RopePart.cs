using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
public class RopePart : MonoBehaviour
{
	SpriteRenderer sp;
    // Start is called before the first frame update
    void Start()
    {
	    sp = GetComponent<SpriteRenderer>();
    }
    float offset = 0;
	public void setCoord(Vector3 start, Vector3 end){
	    Vector3 dir = end-start;
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
	


    // Update is called once per frame
    void Update()
    {

    }
}
