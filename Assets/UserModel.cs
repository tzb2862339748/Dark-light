using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using Newtonsoft.Json;
using System.IO;
using UnityEditor;
using TinyTeam.UI;

public class UserModel {
    /*  {"UserList":[{"Hp":80,"MaxHp":120,"Attack":35,"Speed":25}]}  */
    public int Hp;
    public int MaxHp;
    public int Attack;
    public int Speed;
}
public  class UserModelList
{
    public List<UserModel> UserList = new List<UserModel>();
}

public class GoodsModel //商品信息
{
    public int Id;
    //public string Name;
    //public string Nature;//图片种类(图片名)
    //public string Function;
    //public int Value;//值
    public int Num;//数量
}
public class GoodsModelList
{
    //public List<GoodsModel> GoodsList;
}

public  class Save
{
    public static UserModelList SaveUser;
    public static GoodsModelList SaveGoods;
    private static List<GoodsModel> goodList;//所有装备列表
    private static List<GoodsModel> equiplist;//身上穿的装备的列表
    private static List<Nature> userlist;//用户属性列表
    private static List<Formula> peiFangList;//配方列表
    private static List<GoodsModel> currformulas;//存当前锻造材料
    private static List<Task> alltasksList;//所有的任务
    private static List<Task> currtasksList;//已接受的任务
    //private static List<>
    /// <summary>
    /// 字段封装成的属性
    /// </summary>
    public static List<GoodsModel> GoodList
    {
        get
        {
            return goodList;
        }

        set
        {
            goodList = value;
        }
    }

    public static List<GoodsModel> Equiplist
    {
        get
        {
            return equiplist;
        }

        set
        {
            equiplist = value;
        }
    }

    public static List<Nature> UserList
    {
        get
        {
            return userlist;
        }

        set
        {
            userlist = value;
        }
    }

    public static List<Formula> PeiFangList
    {
        get
        {
            return peiFangList;
        }

        set
        {
            peiFangList = value;
        }
    }

    public static List<GoodsModel> Currformulas
    {
        get
        {
            return currformulas;
        }

        set
        {
            currformulas = value;
        }
    }

    public static List<Task> AlltasksList
    {
        get
        {
            return alltasksList;
        }
        set
        {
            alltasksList = value;
        }

    }

    public static List<Task> CurrtasksList
    {
        get
        {
            return currtasksList;
        }

        set
        {
            currtasksList = value;
        }
    }

    /// <summary>
    /// 购买物品时添加物品，使用时移除
    /// </summary>
    /// <param name="_item"></param>
    public static void BuyItem(Item _item,bool isuse)
    {
        if (isuse == false)
        {
            if (goodList == null)
            {
                goodList = new List<GoodsModel>();
            }
            GoodsModel gm = goodList.Find(x => x.Id == _item.item_ID);
            if (gm != null)
            {
                gm.Num += 1;
            }
            else
            {
                goodList.Add(new GoodsModel() { Id = _item.item_ID, Num = 1 });
            }
            SaveGoodss();
            TTUIPage.ShowPage<Success>();
        }
        else
        {
            if (goodList == null)
            {
                return;
            }
            GoodsModel gm = goodList.Find(x => x.Id == _item.item_ID);
            if (gm != null&& gm.Num > 0)
            {
                gm.Num -= 1;
                
            }
            
            SaveGoodss();
        }

    }
    /// <summary>
    ///使用物品是存入装备数组，脱装备的时候从装备数组移除
    /// </summary>
    /// <param name="_item"></param>
    /// <param name="istakeoff"></param>
    public static void UseItem(Item _item,bool istakeoff)
    {
        if (istakeoff == false)
        {
            if (equiplist == null)
            {
                equiplist = new List<GoodsModel>();
            }
            GoodsModel gm = equiplist.Find(x => x.Id == _item.item_ID);
            if (gm != null)
            {
                BuyItem(_item, false);
            }
            else
            {
                equiplist.Add(new GoodsModel() { Id = _item.item_ID, Num = 1 });
            }
        }

        else
        {
            GoodsModel gm = equiplist.Find(x => x.Id == _item.item_ID);
            equiplist.Remove(gm);
        }
        
        SaveUse();
        //TTUIPage.ShowPage<Success>();

    }
    /// <summary>
    /// 再点击取消按钮的时候，清空材料
    /// </summary>
    public static void Clear()
    {
        for (int i = 0; i < 2; i++)
        {
            if (currformulas[0]!= null)
            {
            currformulas.Remove(currformulas[0]);
            }
            
        }
        //foreach (var item in currformulas)
        //{
        //    currformulas.Remove(item);
        //}
        Save.SaveCaiLiao();
    }
    /// <summary>
    /// 锻造的时候添加材料
    /// </summary>
    /// <param name="_item"></param>
    public static void CurrCaiLiao(Item _item)
    {

        if (currformulas == null)
        {
            currformulas = new List<GoodsModel>();
        }
        if (currformulas.Count <= 2)
        {
            GoodsModel gm = currformulas.Find(x => x.Id == _item.item_ID);
            if (gm != null)
            {
                gm.Num += 1;
            }
            else
            {
                currformulas.Add(new GoodsModel() { Id = _item.item_ID, Num = 1 });
            }
        }
        else
        {
            //GoodsModel gm = currformulas.Find(x => x.Id == _item.item_ID);
            //if (gm != null)
            //{
                currformulas.RemoveAt(0);
                ////currformulas.Add(gm);
            //}
        }

        SaveCaiLiao();
        //TTUIPage.ShowPage<Success>();
    }
    /// <summary>
    /// 锻造成功减少材料
    /// </summary>
    /// <param name="formula"></param>
    public static void SuccessDZ(Formula formula)
    {
        if (currformulas == null)
        {
            currformulas = new List<GoodsModel>();
        }
        GoodsModel gm = currformulas.Find(x => x.Id == formula.Item1ID);
        GoodsModel gm1= currformulas.Find(x => x.Id == formula.Item2ID);
        GoodsModel gm2 = goodList.Find(x => x.Id == formula.Item1ID);//修改背包数据
        GoodsModel gm3 = goodList.Find(x => x.Id == formula.Item2ID);
        if (gm != null&&gm1!=null&&gm.Num>0&&gm1.Num>0)
        {
            gm.Num -= formula.Item2Amount;
            gm1.Num -= formula.Item2Amount;
            gm2.Num -= formula.Item1Amount;
            gm3.Num -= formula.Item2Amount;
            //Save.BuyItem();
            foreach (var item in currformulas)
            {
                if (item.Num<=0)
                {
                    currformulas.Remove(item);
                }
            }
        }
        Save.SaveCaiLiao();
        Save.SaveGoodss();
    }

    public static void AcceptTask(Task task)
    {

        if (currtasksList == null)
        {
            currtasksList = new List<Task>();
            currtasksList.Add(task);
        }
        else
        {
            currtasksList.Add(task);
        }
        SaveTask();
    }

    public static void MakeTask(Task task)
    {

        Task gm = currtasksList.Find(x => x.Name == task.Name);
        if (gm != null)
        {
            gm.curretNum += 1;
        }
        SaveTask();
    }

    public static void WanCheng(Task task)
    {
        if (currtasksList.Count!=0)
        {
            currtasksList.RemoveAt(0);
        }

        SaveTask();
    }

 

    //保存数据
    private static void  SaveGoodss()
    {
        foreach (var item in goodList)
        {
            if (item.Num <= 0)
            {
                goodList.Remove(item);
            }
        }
        string path1 = Application.dataPath + @"/Resources/Setting/GoodList.json";
        using (StreamWriter sw1=new StreamWriter(path1))
        {
            string json = JsonConvert.SerializeObject(goodList);
            sw1.Write(json);
        }
    }
    private static void SaveUse()
    {
        string path1 = Application.dataPath + @"/Resources/Setting/TestA.json";
        using (StreamWriter sw1 = new StreamWriter(path1))
        {
            string json = JsonConvert.SerializeObject(equiplist);
            sw1.Write(json);
        }
    }
    /// <summary>
    /// 保存材料
    /// </summary>
    private static void SaveCaiLiao()
    {
        string path1 = Application.dataPath + @"/Resources/Setting/CaiLiao.json";
        using (StreamWriter sw1 = new StreamWriter(path1))
        {
            string json = JsonConvert.SerializeObject(currformulas);
            sw1.Write(json);
        }
    }

    private static void SaveTask()
    {
        string path1 = Application.dataPath + @"/Resources/Setting/TaskOver.json";
        using (StreamWriter sw1 = new StreamWriter(path1))
        {
            string json = JsonConvert.SerializeObject(currtasksList);
            sw1.Write(json);
        }
    }

}

