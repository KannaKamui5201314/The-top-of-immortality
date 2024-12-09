using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Home : MonoBehaviour
{
    private Button Go;
    private UIController uiController;
    // Start is called before the first frame update
    void Start()
    {
        Go = transform.Find("Go").GetComponent<Button>();
        uiController = transform.parent.GetComponent<UIController>();
        Go.onClick.AddListener(GoOnclick);
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
    void Update()
    {
        
    }
}
