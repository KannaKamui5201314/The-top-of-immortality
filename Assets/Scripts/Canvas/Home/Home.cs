using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using TTSDK;
using UnityEngine;
using UnityEngine.UI;

public class Home : MonoBehaviour
{
    private Button Go;
    private UIController uiController;
    TextMeshProUGUI Gold_Home_Text;

    private void Awake()
    {
        Gold_Home_Text = transform.Find("Gold_Home").GetComponentInChildren<TextMeshProUGUI>();
    }

    void OnEnable()
    {
        Gold_Home_Text.text = Global.Gold.ToString();
    }

    void Start()
    {
        Go = transform.Find("Go").GetComponent<Button>();
        
        uiController = transform.parent.GetComponent<UIController>();
        Go.onClick.AddListener(GoOnclick);
        Gold_Home_Text.text = Global.Gold.ToString();
        Time.timeScale = 0;
    }

    private void GoOnclick()
    {
        Global.IsGo = true;
        if (!uiController.isLoaded)
        {
            transform.parent.Find("Loading").gameObject.SetActive(true);
        }
        if (!transform.parent.Find("Loading").gameObject.activeSelf)
        {
            Time.timeScale = 1;
        }
        this.gameObject.SetActive(false);
        
    }

    // Update is called once per frame
    
}
