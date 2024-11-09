using System.Collections;
using System.Collections.Generic;
using TMPro;
using TTSDK;
using UnityEngine;

public class EnemyController : MonoBehaviour
{
    Transform t_Player;

    Animator a_Enemy;
    Rigidbody2D r_Enemy;
    Perception p_Enemy;

    int action = 1;
    float actionTimer;

    public bool isHit;

    RectTransform Canvas_Health;
    RectTransform Health;
    TextMeshProUGUI HealthText;

    void Start()
    {
        t_Player = GameObject.FindGameObjectWithTag("Player").transform;

        a_Enemy = GetComponent<Animator>();
        r_Enemy = GetComponent<Rigidbody2D>();
        p_Enemy = GetComponentInChildren<Perception>();

        Canvas_Health = (RectTransform)transform.Find("Canvas_Health");
        Health = (RectTransform)Canvas_Health.Find("Panel_Health").Find("Health");
        HealthText = Canvas_Health.Find("Panel_Health").Find("HealthText").GetComponent<TextMeshProUGUI>();

        // 修改sizeDelta的x值来改变宽度，y值保持不变
        Vector2 sizeDelta = Health.sizeDelta;
        sizeDelta.x = 1.5f * Player.Health / long.Parse(TT.PlayerPrefs.GetString("Health"));
        Health.sizeDelta = sizeDelta;
        //HealthText.text = Player.Health.ToString();
    }

    void Update()
    {
        Action();
        Move();
        ChangeAnimation();;
    }

    private void OnDisable()
    {
        r_Enemy.velocity = Vector2.zero;
    }

    void Move()
    {
        r_Enemy.velocity = new(action * 3f,0);

        //朝向
        if (action != 0)
        {
            transform.localScale = new Vector3(Mathf.Abs(transform.localScale.x) * Mathf.Sign(action), 
                                                            transform.localScale.y, transform.localScale.z);
            //Debug.Log(Canvas_Health.localScale);
            //敌人是通过缩放改变朝向，改变朝向时也会导致血量条翻转，此处时根据摇杆方向使血量条不翻转
            Canvas_Health.localScale = new(Mathf.Sign(action) * Mathf.Abs(Canvas_Health.localScale.x), Canvas_Health.localScale.y);
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
            if (IsAttack())
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
        //Debug.Log(p_Enemy.IsPerception() + "   " + isAttack);
        if (p_Enemy.IsPerception())
        {
            if (IsAttack())
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

    bool IsAttack()
    {
        if (Mathf.Abs(PlayerDirection_X()) <= 4f)
        {
            return true;
        }
        else return false;
    }

    //private void OnCollisionEnter2D(Collision2D collision)
    //{
    //    if (collision.gameObject.CompareTag("Player"))
    //    {
    //        Debug.Log("进来了");
    //        isAttack = true;
    //    }
    //}

    //private void OnCollisionExit2D(Collision2D collision)
    //{
    //    if (collision.gameObject.CompareTag("Player"))
    //    {
    //        Debug.Log("离开了");
    //        isAttack = false;
    //    }
    //}
}
