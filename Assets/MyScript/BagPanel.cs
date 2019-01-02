using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TinyTeam.UI;
using UnityEngine.UI;
using System;

public class BagPanel : TTUIPage {
    //private List<GameObject> BagList=new List<GameObject>();//背包个字
    public GameObject gamePrefab;//物品模板
    private Transform bag;//背包
    public static List<GoodsModel> goods;//解析物品存入

    private Button closebtn;
    public BagPanel() : base(UIType.Normal, UIMode.HideOther, UICollider.None)
    {
        uiPath = "Inventory";
    }
    public override void Awake(GameObject go)
    {
        bag = GameObject.Find("Bag").transform;
        gamePrefab = Resources.Load<GameObject>("MyGoods");
        closebtn = transform.Find("Image/Closbtn").GetComponent<Button>();
        closebtn.onClick.AddListener(() => { Hide(); });
        
    }


    public override void Refresh()
    {
        if (bag.GetChild(0).childCount != 0)
        {
            ClearBag();
        }
        ShowPag();

        //base.Refresh(); 
    }


    public void ClearBag()
    {
        //删除之前创建物品的预设物
        if (goods == null)
        {
            return;
        }
        for (int i = 0; i < goods.Count+1; i++)
        {
            if (bag.GetChild(i).childCount != 0)
            {
                GameObject.Destroy((bag.GetChild(i).GetChild(0).gameObject));
            }
        }
    }
    private void ShowPag()
    {
        ClearBag();
        goods = Save.GoodList;
        //for (int i = 0; i < bag.childCount; i++)
        //{
        //    BagList.Add(bag.GetChild(i).gameObject);
        //}
        if (goods==null)
        {
            return;
        }
        for (int i = 0; i < goods.Count; i++)
        {
            if (goods[i] != null)
            {
                GameObject g = GameObject.Instantiate(gamePrefab);
                g.transform.SetParent(bag.GetChild(i));
                g.transform.position = bag.GetChild(i).transform.position;
                g.transform.localScale = new Vector3(1, 1, 1);
                g.transform.parent.name = goods[i].Id.ToString();
                g.transform.GetChild(0).GetComponent<Text>().text = goods[i].Num.ToString();
                g.transform.GetComponent<Image>().sprite = Resources.Load<Sprite>("Icon/" + goods[i].Id.ToString());
            }
            else
            {
                GameObject.Destroy(bag.GetChild(i).GetChild(0));
            }
            

        }
    }

    //public void TakeEquip(int _id)
    //{
    //    Item item = Read.GedInstance().GetItemId(_id);//装备栏中的物体
    //    Save.BuyItem(item,false);
        
        
    //}
}
