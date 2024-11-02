using System.Collections;
using System.Collections.Generic;
using TTSDK;
using UnityEngine;

public class GameController : MonoBehaviour
{
    private void Awake()
    {
        Init();
    }
    void Start()
    {
    }

    void Update()
    {
        
    }

    void Init()
    {
        TT.InitSDK();
        //所处地界
        if (TT.PlayerPrefs.GetInt("Boundary") == 0)
        {
            TT.PlayerPrefs.SetInt("Boundary", 1);
        }
        //境界
        if (TT.PlayerPrefs.GetInt("Realm") == 0)
        {
            TT.PlayerPrefs.SetInt("Realm", 1);
        }
        //最大血量
        if (TT.PlayerPrefs.GetInt("Health") == 0)
        {
            TT.PlayerPrefs.SetInt("Health", 100);
        }
        //最大力量
        if (TT.PlayerPrefs.GetInt("Strength") == 0)
        {
            TT.PlayerPrefs.SetInt("Strength", 100);
        }
        //最大防御
        if (TT.PlayerPrefs.GetInt("Defense") == 0)
        {
            TT.PlayerPrefs.SetInt("Defense", 50);
        }
        Global.Boundary = TT.PlayerPrefs.GetInt("Boundary");
        new Player(TT.PlayerPrefs.GetInt("Realm"), TT.PlayerPrefs.GetInt("Health"), TT.PlayerPrefs.GetInt("Strength"), TT.PlayerPrefs.GetInt("Defense"));
    }
}
