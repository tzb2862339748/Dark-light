using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TinyTeam.UI;
using UnityEngine.UI;

public class DZ : TTUIPage {

    private GameObject wpPrefab;
    private Button dzBtn;
    Transform trs;//记录BG
    private Button Closebtn;
    private List<Formula> formulaList=new List<Formula>();
    public DZ() : base(UIType.Normal,UIMode.DoNothing,UICollider.None)
    {
        uiPath = "duanzao";
    }
    public override void Awake(GameObject go)
    {
        base.Awake(go);
        GetPeiFang();
        wpPrefab = Resources.Load<GameObject>("MyGoods1");
        trs = transform.Find("BG");
        dzBtn = trs.Find("Button").GetComponent<Button>();
        dzBtn.onClick.AddListener(GetNeedDZ);
        Closebtn = trs.Find("CloseBtn").GetComponent<Button>();
        Closebtn.onClick.AddListener(()=> {
            Hide();
            Save.Clear();
        });
        ShowItem();
    }
    public void GetPeiFang()
    {
        foreach (var item in Save.PeiFangList)
        {
            int item1ID = item.Item1ID;
            int item1Num = item.Item1Amount;
            int item2ID = item.Item2ID;
            int item2Num = item.Item2Amount;
            int resID = item.ResID;
            Formula formula = new Formula(item1ID, item1Num, item2ID, item2Num, resID);//配方
            formulaList.Add(formula);
        }
    }
    public override void Refresh()
    {
        base.Refresh();
        ShowItem();
    }
    /// <summary>
    /// 得到要锻造的物品
    /// </summary>
    public void GetNeedDZ()
    {
        //判断两个锻造槽是否都有东西
        if (Save.Currformulas.Count>=2)
        {
            List<int> CaiLia = new List<int>();
            int a = 0;//所有锻造材料的总和
            //foreach (GoodsModel item in Save.Currformulas)
            //{
            //    a += item.Num;
            //}
            for (int i = 0; i < Save.Currformulas.Count; i++)
            {
                a += Save.Currformulas[i].Num;
            }
            //得到当前所有的锻造材料
            for (int i = 0; i < a; i++)
            {
                if (i + 1 <= Save.Currformulas[0].Num)
                {
                    CaiLia.Add(Save.Currformulas[0].Id);
                }
                else
                {
                    CaiLia.Add(Save.Currformulas[1].Id);
                }
            }

            Formula matchedFormula = null;
            foreach (Formula formula in formulaList)
            {
                bool isMatch = formula.Match(CaiLia);
                //Debug.Log(isMatch);
                if (isMatch)
                {
                    matchedFormula = formula;
                    break;
                }
            }
            if (matchedFormula!=null)
            {
       
                Save.BuyItem(Read.GedInstance().GetItemId( matchedFormula.ResID),false);
                Save.SuccessDZ(formulaList.Find(x=>x.ResID==matchedFormula.ResID));
                TTUIPage.ShowPage<BagPanel>();
                Clear();
            }
        }
        ShowItem();
    }

    public void ShowItem()
    {
        Clear();

        for (int i = 0; i < Save.Currformulas.Count; i++)
        {
            GameObject g = GameObject.Instantiate(wpPrefab);
            g.transform.SetParent(trs.GetChild(i));
            g.transform.position = trs.GetChild(i).position;
            g.transform.localScale = new Vector3(1, 1, 1);
            if (Save.Currformulas!=null)
            {
                g.transform.parent.name = Save.Currformulas[i].Id.ToString();
                g.transform.GetChild(0).GetComponent<Text>().text = Save.Currformulas[i].Num.ToString();
                g.transform.GetComponent<Image>().sprite = Resources.Load<Sprite>("Icon/" + Save.Currformulas[i].Id.ToString());
            }
           
        }
    }

    private void Clear()
    {
        Transform bg = transform.Find("BG");
        for (int i = 0; i < bg.childCount; i++)
        {
            if (bg.GetChild(i).childCount != 0)
            {
                bg.GetChild(i).DestroyChildren();
            }
        }
    }
}
