using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Tools
{
    /// <summary>
    /// 先经过Loading界面，然后加载指定场景
    /// </summary>
    /// <param name="scene"></param>
    /// <param name="sceneName"></param>
    public static void LoadSceneByLoading(string sceneName)
    {
        GameCtrl.Instance.nextScenceName = sceneName;
        SceneManager.LoadScene("Loading");
    }

    //public static T FindInChildn<T>()
    //{

    //}
}
