using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TinyTeam.UI;
using UnityEngine.UI;

public class ShopPanel : TTUIPage
{

    private GameObject itemPrefab;
    private List<int> items = new List<int>();
    private static Transform content;
    private  Item itemsAll;
    public static Text des;
    private Button buy;
    public ShopPanel() : base(UIType.Normal, UIMode.HideOther, UICollider.None)
    {
        uiPath = "Shop";
    }
    public override void Awake(GameObject go)
    {
        base.Awake(go);
    }
    public override void Refresh()
    {
  
        items = data as List<int>;
        itemPrefab = Resources.Load<GameObject>("ShopItem");
        //buy = itemPrefab.transform.Find("ButtonBuy").GetComponent<Button>();
        //buy.onClick.AddListener(() => { Save.BuyItem(itemsAll); });
        content = GameObject.Find("Content").transform;

        for (int i = 0; i < items.Count; i++)
        {
            itemsAll = Read.GedInstance().GetItemId(items[i]);
            GameObject o = GameObject.Instantiate(itemPrefab);
            o.transform.SetParent(content);
            o.transform.localScale = new Vector3(1, 1, 1);
            o.transform.Find("imageItem").GetChild(0).GetComponent<Image>().sprite = Resources.Load<Sprite>("Icon/" + items[i].ToString());          
            //if (items[0] < 2000)
            {
                o.transform.Find("NameText").GetComponent<Text>().text = itemsAll.item_Name;
                o.transform.Find("LText").GetComponent<Text>().text = itemsAll.item_Type;
                o.transform.Find("JText").GetComponent<Text>().text = itemsAll.price.ToString();
                o.transform.Find("imageItem").GetChild(0).name = i.ToString();
                o.transform.Find("imageItem").GetChild(1).GetComponent<Toggle>().group = content.GetComponent<ToggleGroup>();
                o.transform.Find("imageItem").GetComponent<Ti>().Init(itemsAll);
            }


        }

        
    }
    public static void Close()
    {
        content.DestroyChildren();
    }

    
}
