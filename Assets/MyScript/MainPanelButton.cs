using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TinyTeam.UI;
using UnityEngine.UI;

public class MainPanelButton : TTUIPage {

    private Button buttonStatus, btnEquip, btnBag, btnSkill, btnTishi;
    public MainPanelButton() : base(UIType.Normal, UIMode.DoNothing, UICollider.None)
    {
        uiPath = "Buttons";
    }
    public override void Awake(GameObject go)
    {
        buttonStatus = transform.Find("PeopleMessageBtn").GetComponent<Button>();
        btnEquip = transform.Find("WeaponBtn").GetComponent<Button>();
        btnBag = transform.Find("BagBtn").GetComponent<Button>();
        btnTishi = transform.Find("Tanhao").GetComponent<Button>();
        btnBag.onClick.AddListener(()=> { TTUIPage.ShowPage<BagPanel>(); });
        btnEquip.onClick.AddListener(()=> { TTUIPage.ShowPage<EquiPanel>(); });
        buttonStatus.onClick.AddListener(()=> { TTUIPage.ShowPage<Nature>(); });

        ShopItemlist.OnNpcTrigger+=ShowTiShi;
        btnTishi.gameObject.SetActive(false);
    }
    public void ShowTiShi(bool isShow,List<int> _item)
    {
        btnTishi.gameObject.SetActive(isShow);//默认隐藏提示按钮
        if (isShow)
        {
            btnTishi.onClick.AddListener(() => TTUIPage.ShowPage<ShopPanel>(_item));//传出所有的商品信息
        }
        if (!isShow)
        { 
            if (TTUIPage.allPages.ContainsKey("ShopPanel"))
            {
                ShopPanel.Close();
                TTUIPage.ClosePage<ShopPanel>();
                btnTishi.onClick.RemoveAllListeners();
            }
            
        }
    }
}
