using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class HomeButton : MonoBehaviour
{
    private Button homeButton;
    public GameObject home;//FindObjectsWithTag�޷���ȡʧ������
    // Start is called before the first frame update
    void Start()
    {
        homeButton = GetComponent<Button>();
        homeButton.onClick.AddListener(HomeButtonOnClick);
    }

    private void HomeButtonOnClick()
    {
        Time.timeScale = 0;
        home.SetActive(true);
        Global.IsGo = false;
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
