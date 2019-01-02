using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TinyTeam.UI;
using UnityEngine.UI;
using DG.Tweening;
using UnityEngine.SceneManagement;

public class Character : TTUIPage
{
    private int count=0;
    private Button leftBtn;
    private Button rightBtn;
    private Button OKBtn;
    private Button buttonRandom;//随机名字
    private InputField inputName;//输入名字
    private string[] xings = { "司马","诸葛","上官","端木","斗破"};
    public string[] mings = { "赵四","王五","苍穹"};

    public GameObject[] hero;  //your hero
    //public GameObject buttonNext, buttonPrev; //button prev and button next

    [HideInInspector]
    public int indexHero = 0;  //index select hero

    private GameObject[] heroInstance; //use to keep hero gameobject when Instantiate


    public Character() : base(UIType.Normal, UIMode.DoNothing, UICollider.None)
    {
        uiPath = "MyCharacterCreationPanel";
    }

    /// <summary>
    /// 初始化可交互的UI
    /// </summary>
    /// <param name="go"></param>
    public override void Awake(GameObject go)
    {
        leftBtn = transform.Find("LeftBtn").GetComponent<Button>();
        rightBtn = transform.Find("RightBtn").GetComponent<Button>();
        OKBtn = transform.Find("OKBtn").GetComponent<Button>();
        buttonRandom = transform.Find("ButtonRandom").GetComponent<Button>();
        inputName = transform.Find("InputField").GetComponent<InputField>();
        OKBtn.onClick.AddListener(OnOk);


        hero = Resources.LoadAll<GameObject>("Player");//加载指定路径下的所用GameObject
        heroInstance = new GameObject[hero.Length]; //add array size equal hero size
        indexHero = 0; //set default selected hero
        SpawnHero(); //spawn hero to display current selected
        buttonRandom.onClick.AddListener(()=> {

            GetRandomName();
            count++;
            buttonRandom.transform.DORotate(Vector3.forward * 120*(count%2), 0.5f);
        });

        rightBtn.onClick.AddListener(()=> 
        {
            indexHero++;
            {
                if (indexHero>=heroInstance.Length)
                {
                    indexHero = 0;
                }
                UpdateHero(indexHero);
            }
        });

        leftBtn.onClick.AddListener(() =>
        {
            indexHero--;
            {
                if (indexHero <0)
                {
                    indexHero = heroInstance.Length - 1;
                }
                UpdateHero(indexHero);
            }
        });

        //check if hero is less than 1 , button next and prev will disappear
        if (hero.Length <= 1)
        {
            rightBtn.gameObject.SetActive(false);
            leftBtn.gameObject.SetActive(false);
        }
    }
    bool isTrue = true;
    public  void OnOk()
    {
        PlayerPrefs.SetString("pName", inputName.text);
        PlayerPrefs.SetInt("pSelect", indexHero);
       
        Tools.LoadSceneByLoading("My Dreamdev Village");
    }

   


    public void UpdateHero(int _indexHero)
    {
        for (int i = 0; i < hero.Length; i++)
        {
            //Show only select character
            if (i == _indexHero)
            {
                heroInstance[i].SetActive(true);
            }
            else
            {
                //Hide Other Character
                heroInstance[i].SetActive(false);
            }
        }
    }

    //Spawn all hero
    public void SpawnHero()
    {
        for (int i = 0; i < hero.Length; i++)
        {
            heroInstance[i] = (GameObject)GameObject.Instantiate(hero[i], Vector3.zero, Quaternion.identity);
        }

        UpdateHero(indexHero);

    }

    public void GetRandomName()
    {
        string x = xings[Random.Range(0,xings.Length)];
        string m = mings[Random.Range(0,mings.Length)];
        inputName.text = x + m;
    }
}
