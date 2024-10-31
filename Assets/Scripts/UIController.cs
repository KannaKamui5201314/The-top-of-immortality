using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class UIController : MonoBehaviour
{
    private Button C;
    private Transform Loading;
    private TextMeshProUGUI LoadingText;
    private bool isLoaded;
    float LoadingSum;

    private Transform floors;

    void Start()
    {
        C = transform.Find("C").GetComponent<Button>();
        C.onClick.AddListener(COnclick);
        Loading = transform.Find("Loading");
        LoadingText = Loading.GetComponentInChildren<TextMeshProUGUI>();
        Loading.gameObject.SetActive(true);

        floors = GameObject.FindGameObjectWithTag("floors").transform;
    }

    void Update()
    {
        _Loading();

        if (Input.GetKeyDown(KeyCode.Space))
        {
            Global.IsJumpStart = true;
        }
    }

    //¼ÓÔØ½çÃæ
    void _Loading()
    {
        if (!isLoaded)
        {
            if (Global.InitialFloorCount > floors.childCount)
            {
                LoadingSum = 100f * floors.childCount / Global.InitialFloorCount;
                Debug.Log(LoadingSum);
                LoadingText.text = LoadingSum.ToString() + "%";
            }
            else
            {
                isLoaded = true;
                Loading.gameObject.SetActive(false);
            }
        }
    }

    private void COnclick()
    {
        Global.IsJumpStart = true;
    }
}
