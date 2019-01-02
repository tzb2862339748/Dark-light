using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ShowInfo : MonoBehaviour {

    private Button useBtn;
    private Button backBtn;
    private static Text nameText;
    private static Text messageText;

    void Start()
    {
        useBtn = transform.Find("UseButton").GetComponent<Button>();
        backBtn = transform.Find("BackButton").GetComponent<Button>();
        nameText = transform.Find("NameLabel").GetComponent<Text>();
        messageText = transform.Find("MessageLabel").GetComponent<Text>();

        useBtn.onClick.AddListener(()=> {
          
        });
        backBtn.onClick.AddListener(()=> {this.gameObject.SetActive(false); });

    }

    public static void UpdateUI(string name,string message)
    {
        nameText.text = name;
        messageText.text = message;
    }
}
