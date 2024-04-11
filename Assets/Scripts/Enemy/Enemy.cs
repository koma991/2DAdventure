using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : MonoBehaviour
{
    [Header("基础参数")]
    [HideInInspector] public float normalSpeed;
    public float currentSpeed;
    public float pursueSpeed;
    [HideInInspector] public Vector3 faceDir;
    public float hurtForce;

    [HideInInspector] public Transform attack;
    [Header("计时器")]
    public bool wait;
    public float waitTime;
    public float waitCount;

    [HideInInspector] protected Rigidbody2D rb;
    [HideInInspector] public Animator anim;
    [HideInInspector] public PhysicCheck physicCheck;

    [Header("条件")]
    public bool isHurt;
    public bool isDie;

    protected BaseState currentState;
    protected BaseState patrolState;
    protected BaseState chaseState;

    private void OnEnable()
    {
        currentState = patrolState;
        currentState.OnEnter(this);
    }

    protected virtual void Awake()
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
        currentState.LogicUpdate();
        TimeCounter();
    }

    private void FixedUpdate()
    {
        if(!isHurt && !isDie &&!wait) Move();

        currentState.PhysicUpdate();
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
        Vector2 dir = new Vector2(this.transform.position.x - attackTf.position.x, 0).normalized;
        StartCoroutine(OnHurt(dir));
    }

    public IEnumerator OnHurt(Vector2 dir)
    {
        rb.AddForce(dir * hurtForce, ForceMode2D.Impulse);
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
