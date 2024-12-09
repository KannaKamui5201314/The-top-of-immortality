using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class UIController : MonoBehaviour
{
    private Button C;
    private Button Attack;

    private Transform Loading;
    private TextMeshProUGUI LoadingText;
    [HideInInspector]
    public bool isLoaded;
    float LoadingSum;

    private Transform floors;
    private Transform Enemies;

    void Start()
    {
        C = transform.Find("C").GetComponent<Button>();
        C.onClick.AddListener(COnclick);
        Attack = transform.Find("Attack").GetComponent<Button>();
        Attack.onClick.AddListener(AttackOnclick);

        Loading = transform.Find("Loading");
        LoadingText = Loading.GetComponentInChildren<TextMeshProUGUI>();
        //Loading.gameObject.SetActive(true);

        floors = GameObject.FindGameObjectWithTag("floors").transform;
        Enemies = GameObject.FindGameObjectWithTag("Enemies").transform;

    }

    void Update()
    {
        if (Global.IsGo)
        {
            _Loading();
        }
        

        if (Input.GetKeyDown(KeyCode.Space))
        {
            Global.IsJumpStart = true;
        }
        else if (Input.GetKeyDown(KeyCode.A) && Application.isEditor)
        {
            Global.IsAttack = true;
        }
    }

    //加载界面
    void _Loading()
    {
        if (!isLoaded)
        {
            if (Global.InitialFloorCount + Global.InitialEnemyCount > floors.childCount + Enemies.childCount )
            {
                LoadingSum = 100f * (floors.childCount + Enemies.childCount) / (Global.InitialFloorCount + Global.InitialEnemyCount);//浮点数在前才能自动转换结果为浮点数
                //Debug.Log(LoadingSum);
                LoadingText.text = LoadingSum.ToString() + "%";
            }
            else
            {
                isLoaded = true;
                Loading.gameObject.SetActive(false);
                Time.timeScale = 1;
            }
        }
    }

    private void COnclick()
    {
        Global.IsJumpStart = true;
    }

    private void AttackOnclick()
    {
        Global.IsAttack = true;
    }
}
