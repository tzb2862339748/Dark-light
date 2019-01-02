using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;
using TinyTeam.UI;
using UnityEngine.SceneManagement;

public class TitleScenceCtrl : MonoBehaviour {

    public Camera camera;//主摄像机
    public Transform targetPoint;//摄像机移动的目标点
   
	// Use this for initialization
	void Start () {
        camera.transform.DOMove(targetPoint.position,6);//摄像机移动
        TTUIPage.ShowPage<TitlePanel>();
	}
	
	// Update is called once per frame
	void Update () {

        if (Input.anyKeyDown&&Time.time>5)
        {
            TitlePanel.OnKashi();


        }
    }
}
