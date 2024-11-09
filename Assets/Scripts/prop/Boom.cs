using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Boom : MonoBehaviour
{
    private Transform t_Player;

    private float moveDirection;

    private void Start()
    {
        t_Player = GameObject.FindGameObjectWithTag("Player").transform;
        moveDirection = Mathf.Sign(t_Player.localScale.x);
    }

    private void Update()
    {
        transform.Translate(new(moveDirection * Global.BoomSpeed * Time.deltaTime, 0));
    }
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("Enemy"))
        {
            collision.gameObject.GetComponent<EnemyController>().isHit = true;
        }
    }
}
