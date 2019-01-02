using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerCtrl : MonoBehaviour {
    public RaycastHit hit;
    GameObject go;
    CharacterController m_CharacterController;
    float distance;
    // Use this for initialization
    void Start () {
        m_CharacterController = GetComponent<CharacterController>();
    }
    bool isWalk=false;
	// Update is called once per frame
	void Update () {
        if (Input.GetMouseButton(0))
        {
            isWalk = true;
            Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);//得到一条射线
                                                                        //Physics.Raycast(ray,out hit);
                                                                        //判断是否碰撞到物体
            if (Physics.Raycast(ray, out hit, 200, LayerMask.GetMask("Ground")))//最后一个参数是层的参数(不能直接填Int类型的参数，需要用方法获取)
            {
                //得到碰撞点
                //Vector3 vector = hit.point;//属性

                Instantiate(GameSetting.Instance.mousefxNormal, hit.point, Quaternion.identity);
                 transform.LookAt(new Vector3(hit.point.x,transform.position.y,hit.point.z));
            }
        }
        distance = Vector3.Distance(transform.position,hit.point);
        if (distance > 1 && isWalk)
        {
            m_CharacterController.SimpleMove(transform.forward * 2);
        }


    }


    //private void MyClickToMove()
    //{
    //    Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);//得到一条射线
    //    //Physics.Raycast(ray,out hit);
    //    //判断是否碰撞到物体
    //    if (Physics.Raycast(ray, out hit, 200, LayerMask.GetMask("Ground")))//最后一个参数是层的参数(不能直接填Int类型的参数，需要用方法获取)
    //    {
    //        //得到碰撞点
    //        //Vector3 vector = hit.point;//属性

    //        GameObject o = Instantiate(GameSetting.Instance.mousefxNormal,hit.point,Quaternion.identity);
    //    }
    //}
}
