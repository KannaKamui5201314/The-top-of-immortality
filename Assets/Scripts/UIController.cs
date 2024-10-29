using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UIController : MonoBehaviour
{
    private Button C;

    void Start()
    {
        C = transform.Find("C").GetComponent<Button>();
        C.onClick.AddListener(COnclick);
    }

    private void COnclick()
    {
        Global.IsJumpStart = true;
    }

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Space))
        {
            Global.IsJumpStart = true;
        }
    }
}
