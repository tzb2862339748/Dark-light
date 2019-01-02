using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CamerFllow : MonoBehaviour {
    Vector3 offset;
    public float distance;
    Transform player;
	// Use this for initialization
	void Start () {
        offset = transform.forward*distance;
        player = GameObject.FindWithTag("Player").transform;
	}
	
	// Update is called once per frame
	void Update () {
        transform.position =player.position - offset;
	}
}
