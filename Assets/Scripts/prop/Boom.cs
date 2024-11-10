using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Boom : MonoBehaviour
{
    private Transform t_Player;

    private float moveDirection;

    private long Strength;

    float destroyTimer;

    private void Start()
    {
        t_Player = GameObject.FindGameObjectWithTag("Player").transform;
        moveDirection = Mathf.Sign(t_Player.localScale.x);
        Strength = Player.Strength;
    }

    private void Update()
    {
        DestroySelf();
        transform.Translate(new(moveDirection * Global.BoomSpeed * Time.deltaTime, 0));//�ƶ�
    }
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("Enemy"))
        {
            Debug.Log("�����˵���");
            collision.gameObject.GetComponent<EnemyController>().SetHealth(Strength);
            Destroy(gameObject);
        }
    }

    //�Ա�
    void DestroySelf()
    {
        destroyTimer += Time.deltaTime;
        if (destroyTimer>1f)
        {
            Destroy(gameObject);
        }
    }
}
