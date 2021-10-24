using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UIConroller : MonoBehaviour
{
    public static UIConroller instance;
    public Text liveTxt, enemyTxt, timeTxt;
    
    private void Awake()
    {
        instance = this;
    }

    //update UI texts
    public void UIUpdate(int live, int enemy, int time)
    {
        liveTxt.text = "LIVE: " + live.ToString();
        enemyTxt.text = "ENEMY: " + enemy.ToString();
        timeTxt.text = "TIME: " + time.ToString();
    }
}
