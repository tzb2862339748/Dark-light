/// <summary>
/// Npc shop.
/// This script use to create a shop to sell item
/// </summary>

using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using UnityEngine.UI;
using System;
using TinyTeam.UI;

public class ShopItemlist : MonoBehaviour {

    public static event Action<bool,List<int>> OnNpcTrigger;
	public List<int> itemID = new List<int>();//商品列表
	
	void Start()
	{
		if(this.gameObject.tag == "Untagged")
			this.gameObject.tag = "Npc_Shop";
	}
    private void OnTriggerEnter(Collider other)
    {
        //Debug.Log(other.name);
        if (other.gameObject.CompareTag("Player"))
        {
            OnNpcTrigger(true,itemID);
        }
        if (this.gameObject.name== "Quest_NPC")
        {
            TTUIPage.ShowPage<DZ>();
        }
    }
    private void OnTriggerExit(Collider other)
    { 
        if (other.gameObject.CompareTag("Player"))
        {
            OnNpcTrigger(false,itemID);
            TTUIPage.ClosePage<TshiPanel>();
        }
    }
}


