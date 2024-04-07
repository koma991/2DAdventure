using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerAnimation : MonoBehaviour
{

    private Animator anim;
    private Rigidbody2D rb;
    private PhysicCheck physicCheck;
    private PlayerController playerController;

    private void Awake()
    {
        anim = GetComponent<Animator>();
        rb = GetComponent<Rigidbody2D>();
        physicCheck = GetComponent<PhysicCheck>();
        playerController = GetComponent<PlayerController>();
    }

    // Update is called once per frame
    void Update()
    {
        SetAnim();
    }

    private void SetAnim()
    {
        anim.SetFloat("velocityX", Mathf.Abs(rb.velocity.x));
        anim.SetFloat("velocityY", rb.velocity.y);
        anim.SetBool("isGround", physicCheck.GroundCheck());
        anim.SetBool("isCrouch", playerController.isCrouch);
        anim.SetBool("isOverhead",physicCheck.CheckOverhead());
        anim.SetBool("isJump",playerController.isJump);
        anim.SetBool("isDead", playerController.isDead);
    }

    public void SetHurtAnim()
    {
        anim.SetTrigger("Hurt");
    }
}
