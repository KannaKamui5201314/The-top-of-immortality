using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UIElements;

public class PlayerController : MonoBehaviour
{
    Rigidbody2D player;
    Animator a_player;
    public Joystick moveJoystick;
    // Start is called before the first frame update
    void Start()
    {
        player = GetComponent<Rigidbody2D>();
        a_player = GetComponent<Animator>();
    }

    // Update is called once per frame
    void Update()
    {
        Move();
    }
    void Move()
    {
        if (moveJoystick.Horizontal == 0)
        {
            a_player.Play("idle");
        }
        else
        {
            transform.right = new Vector3(moveJoystick.Horizontal, 0, 0);
            a_player.Play("run");
        }
        player.velocity = (Global.UFOSpeed * new Vector2(moveJoystick.Horizontal,0f).normalized);
    }
}
