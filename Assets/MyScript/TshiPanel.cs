using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TinyTeam.UI;
using UnityEngine.UI;

public class TshiPanel : TTUIPage {
    private Text text;
    public TshiPanel() : base(UIType.PopUp,UIMode.DoNothing,UICollider.Normal)
    {
        uiPath = "Ti";
    }

    public override void Awake(GameObject go)
    {
        base.Awake(go);
        text = transform.Find("DisText").GetComponent<Text>();

    }
    public override void Refresh()
    {
        base.Refresh();
        text.text = data.ToString();
    }
}
