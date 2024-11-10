using System.Collections;
using System.Collections.Generic;
using TMPro;
using TTSDK;
using UnityEngine;

public class EnemyController : MonoBehaviour
{
    private int _Boundary;
    private long _Health;
    private long _Strength;
    private long _Defense;

    private long oldHealth;

    Transform t_Player;
    PropController propController;

    Animator a_Enemy;
    Rigidbody2D r_Enemy;
    Perception p_Enemy;

    public int action;
    int moveAction;
    float actionTimer;

    RectTransform Canvas_Health;
    RectTransform Health;

    private void OnEnable()
    {
        action = Random.Range(-2, 2);
        _Boundary = Global.Boundary;
        _Health = (long)(100 * Mathf.Pow(_Boundary, 2));//y = x*x
        _Strength = (long)(100 * Mathf.Pow(_Boundary, 2));
        _Defense = (long)(50 * Mathf.Pow(_Boundary, 2));

        oldHealth = _Health;

        Canvas_Health = (RectTransform)transform.Find("Canvas_Health");
        Health = (RectTransform)Canvas_Health.Find("Panel_Health").Find("Health");

        SetHealthBar(_Health);
    }
    void Start()
    {
        t_Player = GameObject.FindGameObjectWithTag("Player").transform;
        propController = GetComponentInParent<Transform>().GetComponentInParent<PropController>();

        a_Enemy = GetComponent<Animator>();
        r_Enemy = GetComponent<Rigidbody2D>();
        p_Enemy = GetComponentInChildren<Perception>();
    }

    void Update()
    {
        CalculateAction();
        Move();
        ChangeAnimation();;
    }

    private void OnDisable()
    {
        r_Enemy.velocity = Vector2.zero;
    }

    void Move()
    {
        if (action == -555 || action == 555)
        {
            moveAction = 0;
        }
        else moveAction = action;

        //移动
        r_Enemy.velocity = new(moveAction * 3f, 0);

        if (action != 0)
        {
            //朝向
            transform.localScale = new Vector3(Mathf.Abs(transform.localScale.x) * Mathf.Sign(action), 
                                                            transform.localScale.y, transform.localScale.z);
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

    void CalculateAction()
    {
        if (a_Enemy.GetCurrentAnimatorStateInfo(0).IsName("knockdown") || a_Enemy.GetCurrentAnimatorStateInfo(0).IsName("hit heavy"))
        {
            action = 0;
            return;
        }

        //感知
        if (p_Enemy.IsPerception())
        {
            if (IsAttack())
            {
                action = 555 * (int)Mathf.Sign(PlayerDirection_X());
                return;
            }
            action = 2 * (int)Mathf.Sign(PlayerDirection_X());
            return;
        }
        else
        {
            if (Mathf.Abs(action) == 555)
            {
                action = Random.Range(-2, 2);
            }
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
        if (a_Enemy.GetCurrentAnimatorStateInfo(0).IsName("knockdown") 
            || a_Enemy.GetCurrentAnimatorStateInfo(0).IsName("hit heavy")
            )
        {
            return;
        }

        switch (action)
        {
            ////case -999:
            ////    a_Enemy.Play("knockdown");
            ////    break;
            case -555:
                a_Enemy.Play("attack");
                break;
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
            case 555:
                a_Enemy.Play("attack");
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

    public void SetHealth(long strength)
    {
        //力量大于防御才会掉血
        if (strength>_Defense)
        {
            _Health = _Health - (strength - _Defense);//扣血公式
            if (_Health > 0)
            {
                a_Enemy.Play("hit heavy");
            }
            else
            {
                a_Enemy.Play("knockdown");//死亡
                propController.EnemyObjectPool.ReturnObject(gameObject);//回收敌人
            }
            SetHealthBar(_Health);
        }
    }

    //修改血量条显示
    void SetHealthBar(long currentHealth)
    {
        // 修改sizeDelta的x值来改变宽度，y值保持不变
        Vector2 sizeDelta = Health.sizeDelta;
        sizeDelta.x = 1.5f * currentHealth / oldHealth;
        Health.sizeDelta = sizeDelta;
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
