using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using TTSDK;
using UnityEngine;
using UnityEngine.UI;

public class Panel : MonoBehaviour
{
    TextMeshProUGUI Boundary;
    private int oldBoundary;
    Button HeadPortrait;
    TextMeshProUGUI realm;
    RectTransform Health;
    TextMeshProUGUI HealthText;
    TextMeshProUGUI Gold_Play_Text;

    public GameObject personalInfoQuit;
    // Start is called before the first frame update
    void Start()
    {
        Boundary = transform.Find("Boundary").GetComponent<TextMeshProUGUI>();
        oldBoundary = Global.Boundary;
        HeadPortrait = transform.Find("HeadPortrait").GetComponent<Button>();
        realm = transform.Find("Realm").GetComponent<TextMeshProUGUI>();
        Health = (RectTransform)transform.Find("Panel_Health").Find("Health");
        HealthText = transform.Find("Panel_Health").GetComponentInChildren<TextMeshProUGUI>();
        Gold_Play_Text = transform.Find("Gold_Play").GetComponentInChildren<TextMeshProUGUI>();

        Boundary.text = "第" + oldBoundary.ToString() + "界";
        HeadPortrait.onClick.AddListener(OnHeadPortraitClick);
        realm.text = Realm.info[Player.Realm - 1];
        // 修改sizeDelta的x值来改变宽度，y值保持不变
        Vector2 sizeDelta = Health.sizeDelta;
        sizeDelta.x = 220f * Player.Health / long.Parse(TT.PlayerPrefs.GetString("Health"));
        Health.sizeDelta = sizeDelta;
        if (Player.Health > 99999999999)
        {
            HealthText.text = "???????????";
        }
        else
        {
            HealthText.text = Player.Health.ToString();
        }
        Gold_Play_Text.text = Global.Gold.ToString();
    }

    private void OnHeadPortraitClick()
    {
        Debug.Log("个人信息");
        Time.timeScale = 0;
        personalInfoQuit.SetActive(true);
    }

    // Update is called once per frame
    void Update()
    {
        if (oldBoundary != Global.Boundary)
        {
            oldBoundary = Global.Boundary;
            Boundary.text = "第" + oldBoundary.ToString() + "界";
        }

        if (Global.RefreshGoldUI)
        {
            Gold_Play_Text.text = Global.Gold.ToString();
            TT.PlayerPrefs.SetString("Gold", Global.Gold.ToString());
            Global.RefreshGoldUI = false;
        }
    }
}
