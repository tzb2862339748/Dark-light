using UnityEngine;
using System.Collections;
using UnityEngine.UI;
using System;
using TinyTeam.UI;
/// <summary>
/// 挂载在预设物上  
/// </summary>
public class BagItems : MonoBehaviour
{
    private Button button;
    //选中物品的Id
    public static int CurrentGoodsId;
    //物品信息显示框
    public Transform GoodsInfo;
    private static Text nameText;
    private static Text desText;
    private Button usebtn;
    private Button cancelbtn;
    private Button DZBtn;



    void Start()
    {
        GoodsInfo = transform.parent.parent.parent.parent.Find("wupin");
        GoodsInfo.gameObject.SetActive(false);
        button = transform.GetComponent<Button>();
        button.onClick.AddListener(Show);
        nameText = GoodsInfo.transform.Find("Image/nameText").GetComponent<Text>();
        desText = GoodsInfo.Find("Image/desText").GetComponent<Text>();
        usebtn = GoodsInfo.Find("UseBtn").GetComponent<Button>();
        cancelbtn = GoodsInfo.Find("Cancel").GetComponent<Button>();
        cancelbtn.onClick.AddListener(()=> { GoodsInfo.gameObject.SetActive(false); });
        DZBtn = GoodsInfo.Find("Button").GetComponent<Button>();

    }
    public void Show()
    {
        Vector3 worldPos;   
        CurrentGoodsId = int.Parse(transform.parent.name);
        GoodsInfo.gameObject.SetActive(true);//物品信息显示框设为显示
        nameText.text = Read.GedInstance().GetItemId(CurrentGoodsId).item_Name;
        desText.text = Read.GedInstance().GetItemId(CurrentGoodsId).description;
        //点击使用按钮执行的方法
        usebtn.onClick.AddListener(() => {
            GoodsInfo.gameObject.SetActive(false);//物品信息显示框设为隐藏
           //判断装备类型是不是药品，不是就存起来
            if (Read.GedInstance().GetItemId(CurrentGoodsId).equipment_Type.ToString()!="Null")
            {
                Save.UseItem(Read.GedInstance().GetItemId(CurrentGoodsId),false); 
                //Save.GoodList.Add();
            }
            //根据传入的值来判断是买东西存入，还是使用减少数组元素
            Save.BuyItem(Read.GedInstance().GetItemId(CurrentGoodsId),true);
           //刷新背包页面
            TTUIPage.ShowPage<BagPanel>();
            if (!TTUIPage.allPages.ContainsKey("EquipPanel"))
            {
                TTUIPage.ShowPage<EquiPanel>();
            }

        });
        //锻造按钮
        DZBtn.onClick.AddListener(()=> {
            //Save.BuyItem(Read.GedInstance().GetItemId(CurrentGoodsId), true);
            TTUIPage.ShowPage<DZ>();
            Save.CurrCaiLiao(Read.GedInstance().GetItemId(CurrentGoodsId));
        });

        if (RectTransformUtility.ScreenPointToWorldPointInRectangle(
            TTUIRoot.Instance.root.transform as RectTransform,
             Input.mousePosition, TTUIRoot.Instance.uiCamera, out worldPos
            ))
        {
            GoodsInfo.transform.position = worldPos;
        }
    }

    //public void Use(Item _item)
    //{
    //    //GoodsModel goods = Save.GoodList.Find(x => x.Id == _item.item_ID);
    //    //if (goods.Num != 0)
    //    //{
    //    //    goods.Num--;
    //    //}
    //    //else
    //    //{
    //    //    Save.GoodList.Remove(goods);
    //    //}


    //}
}
