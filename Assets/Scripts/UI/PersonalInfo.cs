using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using TTSDK;
using UnityEngine;
using UnityEngine.UI;

public class PersonalInfo : MonoBehaviour
{
    TextMeshProUGUI info;
    Button personalInfoQuit;
    void Start()
    {
        info = transform.Find("Info").GetComponent<TextMeshProUGUI>();
        personalInfoQuit = transform.parent.GetComponent<Button>();
        personalInfoQuit.onClick.AddListener(OnQuitClick);
        info.text = "���磺" + Realm.info[Player.Realm - 1] + "\n" +
                            "Ѫ����" + TT.PlayerPrefs.GetInt("Health") + "\n" +
                            "������" + TT.PlayerPrefs.GetInt("Strength") + "\n" +
                            "������" + TT.PlayerPrefs.GetInt("Defense") + "\n";


    }

    private void OnQuitClick()
    {
        personalInfoQuit.gameObject.SetActive(false);
        Time.timeScale = 1;
    }

    void Update()
    {
        
    }
}
