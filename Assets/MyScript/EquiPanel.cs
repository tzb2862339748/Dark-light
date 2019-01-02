using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TinyTeam.UI;
using UnityEngine.UI;

public class EquiPanel : TTUIPage {
    private Transform Head, Armor, rHand, lHand, Shoe, Accessory;
    private Button closeBtn;
    private Text infoName, InfoDes;
    private Transform infoParent;
    private GameObject game;//武器格子
    private Sprite sprite;//要替换的图片
    private GameObject gamePrefab;
    private List<GoodsModel> items = new List<GoodsModel>();//读取已穿戴的武器
    public EquiPanel() : base(UIType.Normal, UIMode.HideOther, UICollider.None)
    {
        uiPath = "Equip";
    }

    public override void Awake(GameObject go)
    {
        base.Awake(go);
        closeBtn = transform.Find("CloseBtn").GetComponent<Button>();
        closeBtn.onClick.AddListener(()=> { Hide(); });
        gamePrefab = Resources.Load<GameObject>("Image");

    }
    public override void Refresh()
    {
        base.Refresh();
        ShowEquip();
        items = Save.Equiplist;
        if (items==null)
        {
            return;
        }
        for (int i = 0; i < items.Count; i++)
        {
            Chuan(Read.GedInstance().GetItemId(items[i].Id));
        }
        Nature.ShowStatus();
    }
    public  void Chuan(Item _item)
    {
        
        game = transform.Find("BG/" + _item.equipment_Type.ToString()).GetChild(0).gameObject;
        sprite = Resources.Load<Sprite>("Icon/"+_item.item_ID.ToString());
   
        if (game.name != "Image(Clone)"&&game.name!=_item.item_ID.ToString())
        {
            GoodsModel gm = Save.Equiplist.Find(x => x.Id == int.Parse(game.name));
            Save.BuyItem(Read.GedInstance().GetItemId(int.Parse(game.name)), false);
            if(gm!=null)
            {
                Save.Equiplist.Remove(gm);
            }
            
        }
        switch (_item.equipment_Type)
        {
            case Equipment_Type.Null:
                Debug.Log("Null");
                break;
            case Equipment_Type.Head_Gear:
                Debug.Log("Head_Gear");
                game.name = _item.item_ID.ToString();
                game.transform.GetComponent<Image>().sprite = sprite;
                break;
            case Equipment_Type.Armor:
                Debug.Log("Armor");
                game.name = _item.item_ID.ToString();
                game.transform.GetComponent<Image>().sprite = sprite;
                break;
            case Equipment_Type.Shoes:
                Debug.Log("Shoes");
                game.name = _item.item_ID.ToString();
                game.transform.GetComponent<Image>().sprite = sprite;
                break;
            case Equipment_Type.Accessory:
                Debug.Log("Accessory");
                game.name = _item.item_ID.ToString();
                game.transform.GetComponent<Image>().sprite = sprite;
                break;
            case Equipment_Type.Left_Hand:
                Debug.Log("Left_Hand");
                game.name = _item.item_ID.ToString();
                game.transform.GetComponent<Image>().sprite = sprite;
                break;
            case Equipment_Type.Right_Hand:
                Debug.Log("Right_Hand");
                game.name = _item.item_ID.ToString();
                game.transform.GetComponent<Image>().sprite = sprite;
                break;
            case Equipment_Type.Two_Hand:
                Debug.Log("Two_Hand");
                game.name = _item.item_ID.ToString();
                game.transform.GetComponent<Image>().sprite = sprite;
                break;
            default:
                //Debug.Log("药品");
                //image.sprite = Resources.Load<Sprite>(_item.item_ID.ToString());
                break;
        }
    }

    void ShowEquip()
    {
        Clear();
        Transform num = transform.Find("BG");

        for (int i = 0; i < num.childCount; i++)
        {
            GameObject gm = GameObject.Instantiate(gamePrefab);
            gm.transform.SetParent(num.GetChild(i));
            gm.transform.position = num.GetChild(i).transform.position;
            gm.transform.localScale = new Vector3(1, 1, 1);   
        }
    }

    private void Clear()
    {
        Transform bg= transform.Find("BG");
        for (int i = 0; i < bg.childCount; i++)
        {
            if (bg.GetChild(i).childCount!=0)
            {
                bg.GetChild(i).DestroyChildren();
            }
        }
    }
}
