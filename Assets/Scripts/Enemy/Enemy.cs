using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : MonoBehaviour
{
    [Header("»ù´¡²ÎÊý")]
    public float normalSpeed;
    public float currentSpeed;
    public float pursueSpeed;
    public Vector3 facedir;
    public Vector2 offset;
    public float checkLenght;
    public LayerMask wallLayer;
    public float timerDuration;
    public float timerCount;


    public bool isWall;

    protected Rigidbody2D rb;
    protected Animator anim;
    protected SpriteRenderer sr;


    private void Awake()
    {
        rb = GetComponent<Rigidbody2D>();
        anim = GetComponent<Animator>();
        sr = GetComponent<SpriteRenderer>();

        currentSpeed = normalSpeed;
    }

    private void Start()
    {
        facedir = new Vector3(-this.transform.localScale.x, 0, 0);
        timerCount = timerDuration;
    }

    private void Update()
    {
        if (isWall) timerCount -= Time.deltaTime;
        CheckWall();
    }

    public virtual void Move()
    {
        if (isWall)
        {
            rb.velocity = Vector2.zero;
            anim.SetBool("walk", false);
            return;
        }
        rb.velocity = new Vector2(currentSpeed * facedir.x * Time.deltaTime,rb.velocity.y);
    }

    public void CheckWall()
    {
        if (Physics2D.Raycast((Vector2)this.transform.position + offset, Vector2.left, checkLenght, wallLayer))
        {
            isWall = true;
            if (timerCount <= 0.0f)
            {
                facedir = -facedir;
                FlipCharacter();
                offset.x = facedir.x > 0 ? Mathf.Abs(offset.x) : -offset.x;
                isWall = false;
                timerCount = timerDuration;
            }
        }
    }

    private void FlipCharacter()
    {
        if (facedir.x > 0.0f) sr.flipX = true;
        else if (facedir.x < 0.0f) sr.flipX = false;
    }

    private void OnDrawGizmosSelected()
    {
        Vector2 startPos = (Vector2)this.transform.position + offset;
        Gizmos.DrawLine(startPos, startPos + Vector2.left * checkLenght);
    }
}
