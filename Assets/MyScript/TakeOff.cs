using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TinyTeam.UI;

public class TakeOff : MonoBehaviour {
    public Button button;//自身的Button
    public Button OkBtn;
    public Button CancelBtn;
    Transform info;
    int ID;
	void Start () {
        info = transform.parent.parent.parent.Find("TakeOff");
        info.gameObject.SetActive(false);
        button = GetComponent<Button>();
        button.onClick.AddListener(() =>
        {
            if (gameObject.name=="Image(Clone)")
            {
                return;
            }
            Show();
        });    
	}

    void Show()
    { 
        info.gameObject.SetActive(true);
        Debug.Log(info.gameObject.name);
        OkBtn = info.Find("UseBtn").GetComponent<Button>();
        CancelBtn = info.Find("Cancel").GetComponent<Button>();
        CancelBtn.onClick.AddListener(() => { info.gameObject.SetActive(false); });
        OkBtn.onClick.AddListener(() => {
            if (gameObject.name != "Image(Clone)")
            {
                ID = int.Parse(gameObject.name);
                GoodsModel gm = Save.Equiplist.Find(x => x.Id == ID);
                Save.BuyItem(Read.GedInstance().GetItemId(ID), false);
                if (gm != null)
                {
                    Save.UseItem(Read.GedInstance().GetItemId(ID), true);
                }
                //GetComponent<Image>().sprite = Resources.Load<Sprite>("Icon/Slotlayout");
                //if (gameObject.name==ID.ToString())
                //{
                //    gameObject.name = "Image";
                //}
            }
            info.gameObject.SetActive(false);
            TTUIPage.ShowPage<EquiPanel>();
        });
    }
	void Update () {
		
	}
}
