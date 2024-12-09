using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MainCameraController : MonoBehaviour
{
    private Camera MainCamera;
    private Transform t_Player;
    public int Smoothvalue = 2;
    public float PosY = 1;

    Transform SpriteMask;
    float cun;
    void Start()
    {
        MainCamera = GetComponent<Camera>();
        t_Player = GameObject.FindGameObjectWithTag("Player").GetComponent<Transform>();
        SetSize();
        SpriteMask = this.transform.Find("SpriteMask");
        cun = Mathf.Sqrt(Screen.width * Screen.width + Screen.height * Screen.height)/100f;
        //动态修改遮罩大小
        //Debug.Log(cun);
        SpriteMask.localScale = new(cun + 1f, cun + 1f, 1f);
    }

    void Update()
    {
        Move();
    }

    //设置相机大小和屏幕大小一致  set  Camera & Screen Size same
    void SetSize()
    {
        if (MainCamera.orthographic)
        {
            //Debug.Log("Screen.height=" + Screen.height);
            MainCamera.orthographicSize = Screen.height / 2 / 100f;
        }
    }

    //follow player
    void Move()
    {
        Vector3 Targetpos = new(0, t_Player.position.y + PosY, -10f);
        transform.position = Vector3.Lerp(transform.position, Targetpos, Time.deltaTime * Smoothvalue);
    }
}
