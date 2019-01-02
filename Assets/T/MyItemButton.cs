using UnityEngine;
using System.Collections;
using UnityEngine.UI;
/// <summary>
/// 挂载在预设物上  
/// </summary>
public class MyItemButton : MonoBehaviour
{
    //当前物品的图片
    public Sprite Sprite;
    //当前物品
    public GoodsModel CurrentGoods;
    //选中物品的Id
    public static int CurrentGoodsId;
    //物品信息显示框
    public Transform GoodsInfo;
    // Use this for initialization
    void Start()
    {
        Sprite = GetComponent<Image>().sprite;
        //EventDelegate dl = new EventDelegate(this, "Show");
        GetComponent<Button>().onClick.AddListener(Show);
        GoodsInfo = transform.parent.parent.parent.parent.parent.parent.Find("ShowInfo");

        //获取预设物图片的名字
        //for (int i = 0; i < Save.SaveGoods.GoodsList.Count; i++)
        //{
        //    //if (Sprite.name == Save.SaveGoods.GoodsList[i].Nature)
        //    {
        //        CurrentGoods = Save.SaveGoods.GoodsList[i];
        //        break;
        //    }
        //}
    }
    public void Show()
    {
     
        GoodsInfo.gameObject.SetActive(true);
        
        //GoodsInfo.GetChild(0).GetComponent<Sprite>().name = CurrentGoods.Nature;
        //GoodsInfo.GetChild(1).GetComponent<Text>().text = CurrentGoods.Name;
        //GoodsInfo.GetChild(2).GetComponent<Text>().text = CurrentGoods.Function;

        CurrentGoodsId = CurrentGoods.Id;
    }
}
