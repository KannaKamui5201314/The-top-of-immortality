using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyController : MonoBehaviour
{
    Transform t_Player;

    Animator a_Enemy;
    Rigidbody2D r_Enemy;
    Perception p_Enemy;

    int action = 1;
    float actionTimer;

    bool isAttack;
    
    void Start()
    {
        t_Player = GameObject.FindGameObjectWithTag("Player").transform;

        a_Enemy = GetComponent<Animator>();
        r_Enemy = GetComponent<Rigidbody2D>();
        p_Enemy = GetComponentInChildren<Perception>();
    }

    void Update()
    {
        Action();
        Move();
        ChangeAnimation();;
    }

    void Move()
    {
        r_Enemy.velocity = new(action * 3f,0);

        //朝向
        if (action != 0)
        {
            transform.localScale = new Vector3(Mathf.Abs(transform.localScale.x) * Mathf.Sign(action), 
                                                            transform.localScale.y, transform.localScale.z);
        }
        
        //限制在屏幕内
        if (transform.position.x >= Screen.width / 2f / 100f + 0.5f)
        {
            transform.position = new(-Screen.width / 2f / 100f, transform.position.y);
        }
        else if (transform.position.x <= -Screen.width / 2f / 100f - 0.5f)
        {
            transform.position = new(Screen.width / 2f / 100f, transform.position.y);
        }
    }

    void Action()
    {
        //Debug.Log(action);
        //感知
        if (p_Enemy.IsPerception())
        {
            if (isAttack)
            {
                action = 0;
                return;
            }
            action = 2 * (int)Mathf.Sign(PlayerDirection_X());
            return;
        } 

        //3秒切换一次状态
        actionTimer += Time.deltaTime;
        if (actionTimer>3f)
        {
            actionTimer = 0f;
            action = Random.Range(-2,2);
        }
    }

    float PlayerDirection_X()
    {
        return t_Player.position.x - transform.position.x;
    }

    void ChangeAnimation()
    {
        if (p_Enemy.IsPerception())
        {
            if (isAttack)
            {
                a_Enemy.Play("attack");
                return;
            }
            a_Enemy.Play("run");
            return;
        }
        switch (action)
        {
            case -2:
                a_Enemy.Play("run");
                break;
            case -1:
                a_Enemy.Play("walk");
                break;
            case 0:
                a_Enemy.Play("idle");
                break;
            case 1:
                a_Enemy.Play("walk");
                break;
            case 2:
                a_Enemy.Play("run");
                break;
            default:
                break;
        }
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Player"))
        {
            isAttack = true;
        }
    }

    private void OnCollisionExit2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Player"))
        {
            isAttack = false;
        }
    }
}
