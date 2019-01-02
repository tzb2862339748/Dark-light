using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MoveG : MonoBehaviour {
    float moveX;
    float moveY;

	// Use this for initialization
	void Start () {
   
	}
	
	// Update is called once per frame
	void Update () {
        moveX = Input.GetAxis("Horizontal");
        moveY = Input.GetAxis("Vertical");

        //transform.Translate(new Vector3(-moveX,0,-moveY)*Time.deltaTime);
        GetComponent<Rigidbody>().velocity= (transform.forward*Time.deltaTime*100*moveY);
        //transform.Rotate(speed);
        //transform.LookAt(new Vector3( moveX, 0,0));
        transform.rotation *=Quaternion.Euler(0, moveX,0);
    }

}
