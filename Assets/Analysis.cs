using UnityEngine;
using System.Collections;
using LitJson;
using Newtonsoft.Json;
using System.Collections.Generic;
/// <summary>
/// 解析数据
/// </summary>
public class Analysis : MonoBehaviour {

	void Awake () {
        // 用户数据解析
        UserAnalysis();
        // 物品数据解析
        GoodsAnalysis();
        //装备解析
        EquipItem();
        //配方解析
        PeiFang();
        //材料解析
        Cailiao();

        //AllTask();

        CurrentTask();
    }
    /// <summary>
    /// 用户数据解析
    /// </summary>
	void UserAnalysis()
    {
        TextAsset u = Resources.Load("Setting/UserJson") as TextAsset;
        if (!u)
        {
            return;
        }
        Save.UserList = JsonConvert.DeserializeObject<List<Nature>>(u.text);

    }

    /// <summary>
    /// 物品数据解析
    /// </summary>
    void GoodsAnalysis()
    {
        TextAsset g = Resources.Load("Setting/GoodList") as TextAsset;
        if (!g)
        {
            Debug.Log("要加载的文件不存在");
            return;
        }
        Save.GoodList = JsonConvert.DeserializeObject<List<GoodsModel>>(g.text);
        //print(g.text);
    }
    //装备数据解析
    void EquipItem()
    {
        TextAsset g = Resources.Load("Setting/TestA") as TextAsset;
        if (!g)
        {
            Debug.Log("要加载的文件不存在");
            return;
        }
        Save.Equiplist = JsonConvert.DeserializeObject<List<GoodsModel>>(g.text);
 
    }

    void PeiFang()
    {
        TextAsset g = Resources.Load("Setting/Formulas") as TextAsset;
        if (!g)
        {
            Debug.Log("要加载的文件不存在");
            return;
        }
        Save.PeiFangList = JsonConvert.DeserializeObject<List<Formula>>(g.text);

    }

    void Cailiao()
    {
        TextAsset g = Resources.Load("Setting/CaiLiao") as TextAsset;
        if (!g)
        {
            Debug.Log("要加载的文件不存在");
            return;
        }
        Save.Currformulas = JsonConvert.DeserializeObject<List<GoodsModel>>(g.text);

    }
    /// <summary>
    /// 所有的任务
    /// </summary>

    /// <summary>
    /// 当前任务
    /// </summary>
    void CurrentTask()
    {
        TextAsset g = Resources.Load("Setting/TaskOver") as TextAsset;
        if (!g)
        {
            Debug.Log("要加载的文件不存在");
            return;
        }
        Save.CurrtasksList = JsonConvert.DeserializeObject<List<Task>>(g.text);

    }
}
