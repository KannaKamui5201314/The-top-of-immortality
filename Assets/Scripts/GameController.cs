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
        if (TT.PlayerPrefs.GetString("Health") == "")
        {
            TT.PlayerPrefs.SetString("Health", "100");
        }
        //最大力量
        if (TT.PlayerPrefs.GetString("Strength") == "")
        {
            TT.PlayerPrefs.SetString("Strength", "100");
        }
        //最大防御
        if (TT.PlayerPrefs.GetString("Defense") == "")
        {
            TT.PlayerPrefs.SetString("Defense", "50");
        }
        Global.Boundary = TT.PlayerPrefs.GetInt("Boundary");
        new Player(TT.PlayerPrefs.GetInt("Realm"), long.Parse(TT.PlayerPrefs.GetString("Health")), long.Parse(TT.PlayerPrefs.GetString("Strength")), long.Parse(TT.PlayerPrefs.GetString("Defense")));
    }
}
