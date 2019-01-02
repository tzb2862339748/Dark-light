using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class MyCamera : MonoBehaviour {
    public Camera mycamera;

    public Button buttonjia;
    public Button buttonjian;
	// Use this for initialization
	void Start () {
        buttonjia.onClick.AddListener(()=> {
            if (mycamera.orthographicSize<=0)
            {
                return;
            }
            mycamera.orthographicSize -= 1; });
        buttonjian.onClick.AddListener(() => mycamera.orthographicSize += 1);
    }
	
	// Update is called once per frame
	void Update () {
 
	}
}
