using System.Collections;
using System.Collections.Generic;
using TMPro;
using TTSDK;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    public int Boundary;
    public int ply_Boundary;

    BoxCollider2D bobyCollider;
    Rigidbody2D r_Player;
    Animator a_Player;
    public Joystick moveJoystick;

    public float JumpUpForce = 13f;
    public float JumpDownForce = 10f;

    GameObject Boom;
    Transform Booms;
    Transform Launcher;

    RectTransform Canvas_Health;
    RectTransform Health;
    TextMeshProUGUI HealthText;

    private void Awake()
    {
        TT.InitSDK();
    }

    void Start()
    {
        bobyCollider = GetComponent<BoxCollider2D>();
        r_Player = GetComponent<Rigidbody2D>();
        a_Player = GetComponent<Animator>();

        Boom = Resources.Load<GameObject>("Prefabs/prop/Boom");
        Booms = GameObject.FindGameObjectWithTag("Booms").transform;
        Launcher = transform.Find("Launcher");

        Canvas_Health = (RectTransform)transform.Find("Canvas_Health");
        Health = (RectTransform)Canvas_Health.Find("Panel_Health").Find("Health");
        HealthText = Canvas_Health.Find("Panel_Health").Find("HealthText").GetComponent<TextMeshProUGUI>();

        // �޸�sizeDelta��xֵ���ı��ȣ�yֵ���ֲ���
        Vector2 sizeDelta = Health.sizeDelta;
        sizeDelta.x = 1.1f * Player.Health / long.Parse(TT.PlayerPrefs.GetString("Health"));
        Health.sizeDelta = sizeDelta;
        if (Player.Health > 99999999999)
        {
            HealthText.text = "???????????";
        }
        else
        {
            HealthText.text = Player.Health.ToString();
        }
        
    }

    void Update()
    {
        Boundary = Global.Boundary;
        ply_Boundary = TT.PlayerPrefs.GetInt("Boundary");
        Move();
        ChangeAnimation();
    }

    private void FixedUpdate()
    {
        Jump();
    }

    void ChangeAnimation()
    {
        if (a_Player.GetCurrentAnimatorStateInfo(0).IsName("knockdown"))
        {
            return;
        }

        Attack();
        if (a_Player.GetCurrentAnimatorStateInfo(0).IsName("attack"))
        {
            return;
        }

        if (Global.IsOnFloor)
        {
            if (moveJoystick.Horizontal == 0)
            {
                //�ڵذ��ϣ�ҡ��û��
                a_Player.Play("idle");
            }
            else
            {
                //�ڵذ��ϣ�ҡ�˶���
                a_Player.Play("run");
            }
        }

        if (Global.isJumpUp)
        {
            a_Player.Play("jump up");
        }
        if (r_Player.velocity.y < 0)
        {
            a_Player.Play("jump fall");
        }
    }

    void Move()
    {
        if (Global.IsOnFloor)
        {
            //�ڵذ��ϣ�ֻ��ˮƽ�ƶ�
            r_Player.velocity = new(Global.PlayerRunSpeed * moveJoystick.Horizontal, 0);
        }
        else
        {
            //����ˮƽ�����ƶ�
            if (moveJoystick.Horizontal != 0)
            {
                r_Player.velocity = new(Global.PlayerRunSpeed * Mathf.Sign(moveJoystick.Horizontal), r_Player.velocity.y);
            }
            else
            {
                r_Player.velocity = new(0, r_Player.velocity.y);
            }
        }
        
        if (moveJoystick.Horizontal != 0)
        {
            //player ����
            transform.localScale = new Vector3(Mathf.Abs(transform.localScale.x) * Mathf.Sign(moveJoystick.Horizontal), 
                                                            transform.localScale.y, transform.localScale.z);
            //Debug.Log(Canvas_Health.localScale);
            //��ɫ��ͨ�����Ÿı䳯�򣬸ı䳯��ʱҲ�ᵼ��Ѫ������ת���˴�ʱ����ҡ�˷���ʹѪ��������ת
            Canvas_Health.localScale = new(Mathf.Sign(moveJoystick.Horizontal)* Mathf.Abs(Canvas_Health.localScale.x), Canvas_Health.localScale.y);
        }

        //��������Ļ��
        if (transform.position.x >= Screen.width / 2f / 100f + 0.5f)
        {
            transform.position = new(-Screen.width / 2f / 100f, transform.position.y);
        }
        else if (transform.position.x <= -Screen.width / 2f / 100f - 0.5f)
        {
            transform.position = new(Screen.width / 2f / 100f, transform.position.y);
        }
    }

    void Attack()
    {
        if (Global.IsAttack)
        {
            Global.IsAttack = false;
            if (a_Player.GetCurrentAnimatorStateInfo(0).IsName("attack"))
            {
                return;
            }
            a_Player.Play("attack");
            GameObject newObject = Instantiate(Boom);
            newObject.name = "+Boom";
            newObject.transform.SetParent(Booms);
            newObject.transform.position = Launcher.position;
            Vector2 newScale = new(transform.localScale.x * 0.2f, transform.localScale.y * 0.2f);
            newObject.transform.localScale = newScale;
        }
    }
    void Jump()
    {
        if (Global.IsJumpStart)
        {
            Global.IsJumpStart = false;
            //������������isTriggerΪtrue
            bobyCollider.isTrigger = true;
            //-45��-  -135��  �·�90��ķ�Χ
            if (moveJoystick.Vertical < 0 && Mathf.Abs(moveJoystick.Vertical) > Mathf.Abs(moveJoystick.Horizontal) && Global.IsOnFloor)
            {
                JumpDown();//ֻ���ڵذ��ϲ���������������������
            }
            else
            {
                JumpUp();
            }
        }

        if (r_Player.velocity.y< 0)
        {
            //����Ϊ��ײ��
            Global.isJumpUp = false;
            //Global.isJumpDown = true;
            //if (!a_Player.GetCurrentAnimatorStateInfo(0).IsName("knockdown"))
            //{
            //    a_Player.Play("jump fall");
            //}

            if (!Global.isJumpDown)
            {
                //��������Ϊ�������������뿪�˵ذ��ٱ����ײ��
                bobyCollider.isTrigger = false;
            }
        }
        //����Ϊ������
        if (r_Player.velocity.y>0)
        {
            bobyCollider.isTrigger = true;
        }
    }

    //IEnumerator GroundCapsulleColliderTimmerFuc()
    //{
    //    yield return new WaitForSeconds(0.3f);
    //    Global.isJumpDown = false;
    //}

    void JumpUp()
    {
        //����
        //Debug.Log("����");
        //Debug.Log("isTrigger = " + bobyCollider.isTrigger);
        Global.IsOnFloor = false;
        Global.isJumpUp = true;
        //a_Player.Play("jump up");
        r_Player.velocity = Vector2.zero;
        r_Player.AddForce(Vector2.up * JumpUpForce, ForceMode2D.Impulse);
    }

    void JumpDown()
    {
        //����
        //Debug.Log("����");
        //Debug.Log("isTrigger = " + bobyCollider.isTrigger);
        Global.IsOnFloor = false;
        Global.isJumpDown = true;
        //a_Player.Play("jump fall");
        r_Player.AddForce(-Vector2.up * JumpDownForce);
        //StartCoroutine(GroundCapsulleColliderTimmerFuc());
    }

    //private void OnTriggerEnter2D(Collider2D collision)
    //{
    //    if (collision.gameObject.CompareTag("Player"))
    //    {
    //        bobyCollider.isTrigger = true;
    //    }
    //}
    private void OnTriggerStay2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("floor"))
        {
            bobyCollider.isTrigger = true;
            Global.isJumpDown = true;//��ֹ���������ײ��
        }
    }
    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("floor"))
        {
            bobyCollider.isTrigger = false;
            Global.isJumpDown = false;//��������
        }
    }
}
