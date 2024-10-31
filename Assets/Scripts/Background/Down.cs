using System.Collections;
using System.Collections.Generic;
using TTSDK;
using UnityEngine;

public class Down : MonoBehaviour
{
    public Transform Up;
    public Transform Camera;
    public Animator a_Player;

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("Player"))
        {
            Global.Boundary -= 1;
            if (Global.Boundary == 0)
            {
                collision.transform.position = new(collision.transform.position.x, 2f);
                Camera.position = new(0, 2f, Camera.position.z);
                Global.Boundary = 1;
                //ตนตุ
                a_Player.Play("knockdown");
                return;
            }
            TT.PlayerPrefs.SetInt("Boundary", Global.Boundary);
            collision.transform.position = new(collision.transform.position.x,500f);
            Camera.position = new(0, 500f, Camera.position.z);
        }
    }
}
