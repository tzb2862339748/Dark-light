using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TinyTeam.UI;
using Newtonsoft.Json;

public class TaskPanel :TTUIPage  {

    private GameObject taskPrefab;
    //任务名称，目标1当前数量，目标2当前数量，目标1的需要数量，目标2的需要数量，奖励的名字
    private static Text nameT,  t1num, t2num, target1num, target2num,JLName,buttonText;
    //领奖励按钮，进行中按钮，接受按钮，攻击按钮一，攻击按钮二，放弃按钮
    private Button JLBtn, ingBtn, acceptBtn, attk1Btn,attk2Btn, putBtn;

    Transform t;//任务预置的父物体

    public TaskPanel(): base(UIType.Normal,UIMode.HideOther,UICollider.None)
    {
        uiPath = "Task/TaskPanel";
    }
    public override void Awake(GameObject go)
    {
        base.Awake(go);

        t = transform.Find("Scroll View/Viewport/Content");
        taskPrefab = Resources.Load<GameObject>("Task/Task");
        acceptBtn = transform.Find("AcceptTask").GetComponent<Button>();
        attk1Btn = transform.Find("AtkItem1").GetComponent<Button>();
        attk2Btn = transform.Find("AtkItem2").GetComponent<Button>();
        putBtn = transform.Find("Put").GetComponent<Button>();

        if (Save.CurrtasksList.Count > 0)
        {
            ShowTask();
        }
        acceptBtn.onClick.AddListener(()=> {
            AllTask();
            acceptBtn.interactable = false;
            Task task = Save.AlltasksList.Find(x=>x.Name=="Kill");
            Save.AcceptTask(task);
            ShowTask();
        });

        attk1Btn.onClick.AddListener( ()=> {
            Task task = Save.CurrtasksList.Find(x => x.Name == "Kill");
            Save.MakeTask(task);
            ShowTask();

        });
    }

    private  void ShowTask()
    {
        Clear();
        List<Task> tasks = Save.CurrtasksList;
        for (int i = 0; i < tasks.Count; i++)
        {
            GameObject g = GameObject.Instantiate(taskPrefab);
            g.transform.SetParent(t);
            g.transform.localScale = new Vector3(1,1,1);
            nameT = g.transform.Find("TaskName").GetComponent<Text>();
            t1num = g.transform.Find("Target1/Num1").GetComponent<Text>();
            //t2num = g.transform.Find("Target2/Num1").GetComponent<Text>();
            target1num = g.transform.Find("Target1/Num2").GetComponent<Text>();
            target2num = g.transform.Find("Target2/Num2").GetComponent<Text>();
            JLName = g.transform.Find("JLName").GetComponent<Text>();
            JLBtn = g.transform.Find("JL").GetComponent<Button>();
            ingBtn = g.transform.Find("Ing").GetComponent<Button>();
            buttonText = g.transform.Find("Ing").GetChild(0).GetComponent<Text>();
            ingBtn.interactable = false;

            nameT.text = tasks[i].Name;
            t1num.text = tasks[i].curretNum.ToString();
            //t2num.text = tasks[i].currentNum2.ToString();
            target1num.text = tasks[i].Num.ToString();
            target2num.text = tasks[i].Des;
            JLName.text = Read.GedInstance().GetItemId(tasks[i].JLID).item_Name;
            if (tasks[i].curretNum >= tasks[i].Num)
            {
                buttonText.text = "已完成";
                ingBtn.interactable = true;
                acceptBtn.interactable = true;
            }
            else
            {
                acceptBtn.interactable = false;
            }

            ingBtn.onClick.AddListener(()=> {
                Task task = Save.CurrtasksList.Find(x => x.Name == "Kill");
                Save.WanCheng(task);
                ShowTask();
                
            });
        }        
    }
    void Clear()
    {
        for (int i = 0; i < t.childCount; i++)
        {
            if (t.childCount != 0)
            {
                t.DestroyChildren();
            }
        }
    }

    void AllTask()
    {
        TextAsset g = Resources.Load("Setting/Task") as TextAsset;
        if (!g)
        {
            Debug.Log("要加载的文件不存在");
            return;
        }
        Save.AlltasksList = JsonConvert.DeserializeObject<List<Task>>(g.text);

    }
}

