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
    public float waitTime;
    public float waitCount;

    [Header("基础判断")]
    public bool wait;

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
}
