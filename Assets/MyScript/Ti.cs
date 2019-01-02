using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;
using TinyTeam.UI;


public class Ti : MonoBehaviour,IPointerDownHandler,IPointerUpHandler {
    List<string> items = new List<string>();
    public Toggle toggle;
    public Button buy;
    public Item item;
    int a;
    public void OnPointerDown(PointerEventData eventData)
    {

         a = int.Parse(eventData.pointerCurrentRaycast.gameObject.name);
        TTUIPage.ShowPage<TshiPanel>(items[a]);
        toggle = transform.Find("Toggle").GetComponent<Toggle>();
        toggle.isOn = true;
        //Save.SaveGoodss();
 
    }

    public void OnPointerUp(PointerEventData eventData)
    {
        //TTUIPage.ClosePage<TshiPanel>();
    }

    void Start () {
        
        for (int i = 0; i < Read.GedInstance().itemList.Count; i++)
        {
            items.Add(Read.GedInstance().itemList[i].description);
        }
        buy = transform.parent.Find("ButtonBuy").GetComponent<Button>();
        buy.onClick.AddListener(() => { Save.BuyItem(item,false); });
    }
	
	// Update is called once per frame
	void Update () {
		
	}
    public void Init(Item _item)
    {
        item = _item;
    }
}
