using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TinyTeam.UI;
using UnityEngine.UI;

public class MainGameScenceCtrl : MonoBehaviour {
    public Button bagBtn;//
    public Button EquipBtn;
    public Button button;//任务按钮
	// Use this for initialization
	void Start () {
        TTUIPage.ShowPage<MainPanelButton>();
        //bagBtn.onClick.AddListener(()=> { TTUIPage.ShowPage<BagPanel>();

        //});
        //EquipBtn.onClick.AddListener(()=> { TTUIPage.ShowPage<EquiPanel>(); });

        button.onClick.AddListener(()=> { TTUIPage.ShowPage<TaskPanel>();});
        TTUIPage.ShowPage<Nature>();
        TTUIPage.ClosePage<Nature>();
	}
	
	// Update is called once per frame
	void Update () {
		
	}
}
