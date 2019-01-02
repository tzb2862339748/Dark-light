using UnityEngine;
using System.Collections;
using TinyTeam.UI;
using UnityEngine.UI;
/// <summary>
/// 属性
/// </summary>
public class Nature : TTUIPage {
    //public static Nature Instance = new Nature();
    private static Text hptext,maxHpText,atkText,spdText;
    private static int Hp, MaxHp, Attack, Speed;
    private Button closeBtn;
    public Nature() : base(UIType.Normal, UIMode.HideOther, UICollider.None)
    {
        uiPath = "Status";
    }
    public override void Awake(GameObject go)
    {
        base.Awake(go);
        hptext = transform.Find("BG/HP/Label").GetComponent<Text>();
        maxHpText = transform.Find("BG/MaxHp/Label").GetComponent<Text>();
        atkText = transform.Find("BG/Atk/Label").GetComponent<Text>();
        spdText = transform.Find("BG/Speed/Label").GetComponent<Text>();
        closeBtn = transform.Find("CloseBtn").GetComponent<Button>();
        closeBtn.onClick.AddListener(Hide);

    }

    public override void Refresh()
    {
        base.Refresh();
        ShowStatus();
    }
    /// <summary>
    /// 属性初始化
    /// </summary>
    static void AssigNature()
    {
            Hp = 100;
            MaxHp = 100;
            Attack = 0;
            Speed = 0;
      
    }
    /// <summary>
    /// 吃药的方法
    /// 使用物品后 属性改变
    /// </summary>

    public static void ShowStatus()
    {
        AssigNature();
        foreach (var equip in Save.Equiplist)
        {
            Item item = Read.GedInstance().GetItemId(equip.Id);
            Hp += item.hp;
            MaxHp += item.hp;
            Attack += item.atk;
            Speed += item.spd;
        }

        hptext.text = Hp.ToString();
        maxHpText.text = MaxHp.ToString();
        atkText.text= Attack.ToString();
        spdText.text=Speed.ToString();
    }
}
