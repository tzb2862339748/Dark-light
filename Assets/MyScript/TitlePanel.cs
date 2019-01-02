using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TinyTeam.UI;
using UnityEngine.UI;
using DG.Tweening;
using UnityEngine.SceneManagement;

public class TitlePanel : TTUIPage {

    public Image imageTilte;
    public Image imageAnyKey;
    public Image imageBG;
    public static Button buttonNew;
    public static Button Load;


    public TitlePanel() : base(UIType.Normal, UIMode.DoNothing, UICollider.None)
    {
        uiPath = "TitlePanel";
    }

    public override void Awake(GameObject go)
    {
        imageAnyKey = transform.Find("ImageAnyKey").GetComponent<Image>();
        imageTilte = transform.Find("ImageTitle").GetComponent<Image>();
        imageBG = transform.Find("ImageBG").GetComponent<Image>();
        buttonNew = transform.Find("Create").GetComponent<Button>();
        Load  = transform.Find("Loade").GetComponent<Button>();
        
        imageTilte.color = new Color(1,1,1,0);
        imageAnyKey.gameObject.SetActive(false);
        imageBG.DOFade(0,1f).SetDelay(0.5f);
        imageTilte.DOFade(1,1).SetDelay(4);
        imageAnyKey.DOFade(0, 1).SetLoops(-1).SetDelay(4).OnStart(()=>imageAnyKey.gameObject.SetActive(true));

        buttonNew.onClick.AddListener(() =>
        {
            Tools.LoadSceneByLoading("My Character Creation");
            //GameCtrl.Instance.nextScenceName = "My Character Creation";
        });
        Load.onClick.AddListener(() =>
        {
            Tools.LoadSceneByLoading("Dreamdev Village");
            //GameCtrl.Instance.nextScenceName = "Dreamdev Village";
            //SceneManager.LoadScene("Loading");
            //GameCtrl.Instance.nextScenceName = "Dreamdev Village";

        });
        imageAnyKey.DOFade(0, 1).SetLoops(-1).SetDelay(4);
        //判断是否有存档
        if (!PlayerPrefs.HasKey("SaveData"))
        {
            Load.interactable = false;
        }
    }

    public static void OnKashi()
    {
        buttonNew.gameObject.SetActive(true);
        Load.gameObject.SetActive(true);
    }


}


//public  class Tools
//{
//    /// <summary>
//    /// 先经过Loading界面，然后加载指定场景
//    /// </summary>
//    /// <param name="scene"></param>
//    /// <param name="sceneName"></param>
//    public static void LoadSceneByLoading(string sceneName)
//    {
//        GameCtrl.Instance.nextScenceName =sceneName;
//        SceneManager.LoadScene("Loading");
//    }
//}
