using System.Collections;
using System.Collections.Generic;
using TTSDK;
using UnityEngine;

public class GameController : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        //…Ë÷√µÿΩÁ
        if (TT.PlayerPrefs.GetInt("Boundary") == 0)
        {
            TT.PlayerPrefs.SetInt("Boundary", 1);
        }
        Global.Boundary = TT.PlayerPrefs.GetInt("Boundary");
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
