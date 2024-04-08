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

    public Transform attack;
    [Header("计时器")]
    public bool wait;
    public float waitTime;
    public float waitCount;

    protected Rigidbody2D rb;
    protected Animator anim;
    protected PhysicCheck physicCheck;




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
        Move();
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

    public void TakeHurt(Transform attackTf)
    {
        attack = attackTf.transform;
        if((transform.position.x - attack.position.x) > 0) transform.localScale = new Vector3(1, 1, 1);
        if ((transform.position.x - attack.position.x) < 0) transform.localScale = new Vector3(-1, 1, 1);
        Vector2 dir = new Vector2(this.transform.position.x - attack.position.x, 0).normalized;
        rb.AddForce(dir * hurtForce,ForceMode2D.Impulse);
        anim.SetTrigger("hurt");
    }
}
