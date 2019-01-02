using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TinyTeam.UI;
using DG.Tweening;

public class Success : TTUIPage {
    public Text text;
    public GameObject go;
    CanvasGroup cg;//可以让UI渐变
    public Success() : base(UIType.PopUp, UIMode.DoNothing, UICollider.None)
    {
        uiPath = "success";

    }
    public override void Awake(GameObject go)
    {
        base.Awake(go);
        go = this.gameObject;
        //text = transform.Find("Text").GetComponent<Text>();
        //go.transform.GetComponent<Image>().DOFade(0,1);
        //text.ta
        //查找UI组件
        cg = go.transform.GetComponent<CanvasGroup>();
       
    }
    public override void Refresh()
    {
        base.Refresh();
        cg.alpha = 1;
        cg.DOFade(0, 0.5f).SetDelay(0.5f).OnComplete(() => ClosePage()); ;
    }
}
