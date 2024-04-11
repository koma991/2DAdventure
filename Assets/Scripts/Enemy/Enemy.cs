using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : MonoBehaviour
{
    [Header("基础参数")]
    public float normalSpeed;
    public float currentSpeed;
    public float pursueSpeed;
    public Vector3 faceDir;
    public float hurtForce;
    public Vector2 dir;

    public Transform attack;
    [Header("计时器")]
    public bool wait;
    public float waitTime;
    public float waitCount;

    protected Rigidbody2D rb;
    protected Animator anim;
    protected PhysicCheck physicCheck;

    [Header("条件")]
    public bool isHurt;
    public bool isDie;


    private void Awake()
    {
        rb = GetComponent<Rigidbody2D>();
        anim = GetComponent<Animator>();
        physicCheck = GetComponent<PhysicCheck>();

        currentSpeed = normalSpeed;
        waitCount = waitTime;
        wait = false;
    }

    private void Update()
    {
        faceDir = new Vector3(-this.transform.localScale.x, 0, 0);
        if((physicCheck.isWallLeft && faceDir.x < 0) || (physicCheck.isWallRight && faceDir.x > 0))
        {
            wait = true;
            anim.SetBool("walk", false);
        }
        TimeCounter();
    }

    private void FixedUpdate()
    {
        if(!isHurt & !isDie) Move();
    }

    public virtual void Move()
    {
        rb.velocity = new Vector2(currentSpeed * faceDir.x * Time.deltaTime,rb.velocity.y);
    }

    public void TimeCounter()
    {
        if (wait)
        {
            waitCount -= Time.deltaTime;
            if (waitCount <= 0)
            {
                wait = false;
                waitCount = waitTime;
                transform.localScale = new Vector3(faceDir.x, 1, 1);
            }
        }
    }

    public void OnTakeDamage(Transform attackTf)
    {
        attack = attackTf;
        //判断玩家在敌人的方向
        if((transform.position.x - attack.position.x) > 0) transform.localScale = new Vector3(1, 1, 1);
        if ((transform.position.x - attack.position.x) < 0) transform.localScale = new Vector3(-1, 1, 1);

        isHurt = true;
        anim.SetTrigger("hurt");
        dir = new Vector2(this.transform.position.x - attackTf.position.x, 0).normalized;
        StartCoroutine(OnHurt());
    }

    public IEnumerator OnHurt()
    {
        rb.AddForce(dir * hurtForce, ForceMode2D.Impulse);
        Debug.Log(rb.velocity.x);
        yield return new WaitForSeconds(0.5f);
        isHurt = false;
    }

    public void OnDead()
    {
        anim.SetBool("dead", true);
        this.gameObject.layer = 2;
        isDie = true;
    }

    public void DestoryAfterAnimation()
    {
        Destroy(this.gameObject);
    }
}
