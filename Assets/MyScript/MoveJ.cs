using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;

public class MoveJ : MonoBehaviour {
    float moveX;
    float moveY;
    Vector3 speed;
    CharacterController m_CharacterController;
    Animator myAnimator;
    public GameObject effect;


    // Use this for initialization
    void Start () {
        m_CharacterController = GetComponent<CharacterController>();
        myAnimator = GetComponent<Animator>();
    }

    // Update is called once per frame
    void Update() {
        moveX = Input.GetAxisRaw("Horizontal");
        moveY = Input.GetAxisRaw("Vertical");
        //speed = new Vector3(-moveX, 0, -moveY);
        if (moveX != 0 || moveY != 0)
        {
            myAnimator.SetBool("isWalk", true);
            Vector3 move = new Vector3(moveX, 0, moveY);
            Quaternion rotation = Quaternion.Euler(0, Camera.main.transform.eulerAngles.y, 0);
            Vector3 dir = rotation * move;
            m_CharacterController.SimpleMove(dir * Time.deltaTime * 100);
            if (Mathf.Abs(moveX) == 1 || Mathf.Abs(moveY) == 1)
            {
                transform.rotation = Quaternion.Lerp(transform.rotation, Quaternion.LookRotation(dir), 0.5f);
            }
        }
        else
        {
            myAnimator.SetBool("isWalk", false);
        }
        if (Input.GetKeyDown(KeyCode.Z))
        {
            
            myAnimator.SetTrigger("skill1");
           
        }
       

    }
    void MySkill1()
    {
        Instantiate(effect, transform.position, Quaternion.identity);
        Camera.main.DOShakePosition(1);

    }

  
}
