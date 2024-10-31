using System.Collections;
using System.Collections.Generic;
using TTSDK;
using UnityEngine;

public class Up : MonoBehaviour
{
    public Transform Down;
    public Transform Camera;
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("Player"))
        {
            Global.Boundary += 1;
            TT.PlayerPrefs.SetInt("Boundary", Global.Boundary);
            collision.transform.position = new(collision.transform.position.x, 2f);
            Camera.position = new(0,2f, Camera.position.z);
        }
    }
}
