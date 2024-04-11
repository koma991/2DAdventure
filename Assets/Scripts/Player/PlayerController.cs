using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerController : MonoBehaviour
{
    private PlayerInputControl inputControl;
    private Rigidbody2D rb;
    private PhysicCheck physicCheck;
    private CapsuleCollider2D capsuleCollider;
    private PlayerAnimation playerAnimation;

    [Header("基本参数")]
    private Vector2 colliderSize;
    private Vector2 colliderOffset;
    private Vector2 Direction;
    public float moveSpeed;
    public float jumpForce;
    public float fullMultiplier;
/*    public float accelerateValue;*/       ///run and walk 切换值
    public PhysicsMaterial2D nomal;
    public PhysicsMaterial2D wall;

    [Header("条件参数")]
    public bool isCrouch;
    public bool isJump;
    public bool isDead;
    public bool isHurt;
    public bool isAttack;

    public float hurtForce;

    private void Awake()
    {

        Application.targetFrameRate = 50;

        inputControl = new PlayerInputControl();
        rb = this.GetComponent<Rigidbody2D>();
        physicCheck = this.GetComponent<PhysicCheck>();
        capsuleCollider = this.GetComponent<CapsuleCollider2D>();
        playerAnimation = this.GetComponent<PlayerAnimation>();
        Physics2D.gravity = new Vector2(Physics2D.gravity.x, Physics2D.gravity.y * 4);

        colliderSize = capsuleCollider.size;
        colliderOffset = capsuleCollider.offset;
/*        accelerateValue = 0.5f;*/
    }

    // Start is called before the first frame update
    void Start()
    {
        inputControl.GamePlay.Jump.started += StartJump;
        inputControl.GamePlay.Jump.canceled += CamcelJump;
        inputControl.GamePlay.Attack.started += PlayerAttack;
        
/*   run and walk 切换     inputControl.GamePlay.Shift.started += Accelerate;
        inputControl.GamePlay.Shift.canceled += Accelerate;*/
    }

    

    private void OnEnable()
    {
        inputControl.Enable();
    }



    // Update is called once per frame
    void Update()
    {
        ReadInput();
        Crouch();
        CheckMaterial();
    }

    private void FixedUpdate()
    {
        if (!isHurt && !isAttack)
        {
            Move();
            FlipCharacter();
        }

        if (rb.velocity.y < 0)
            rb.velocity += Vector2.up * Physics2D.gravity.y * (fullMultiplier - 1) * Time.deltaTime;
    }
    private void ReadInput()
    {
        Direction = inputControl.GamePlay.Move.ReadValue<Vector2>();

    }

    private void Crouch()
    {
        isCrouch = Direction.y < 0;
        if (isCrouch && physicCheck.isGround)
        {
            capsuleCollider.size = new Vector2(colliderSize.x, colliderSize.y * 0.5f);
            capsuleCollider.offset = new Vector2(colliderOffset.x, colliderOffset.y * 0.5f);
        }
        else
        {
            capsuleCollider.size = colliderSize;
            capsuleCollider.offset = colliderOffset;
        }
    }

    private void Move()
    {
        rb.velocity = new Vector2(Direction.x * moveSpeed * Time.deltaTime /** accelerateValue*/, rb.velocity.y);
    }

    private void StartJump(InputAction.CallbackContext context)
    {
        isJump = true;
        if (physicCheck.isGround) rb.AddForce(transform.up * jumpForce,ForceMode2D.Impulse);
    }

    private void CamcelJump(InputAction.CallbackContext context)
    {
        isJump = false;
    }

    private void FlipCharacter()
    {
        if (rb.velocity.x < 0.0f) transform.localScale = new Vector3(-1, 1,1);
        else if (rb.velocity.x > 0.0f) transform.localScale = new Vector3(1,1,1);
    }

    public void GetHurt(Transform attack)
    {
        isHurt = true;
        rb.velocity = Vector2.zero;
        Vector2 dir = new Vector2((this.transform.position.x - attack.position.x), 0).normalized;   //归一化
        rb.AddForce(dir * hurtForce, ForceMode2D.Impulse);
    }

    public void GetDead()
    {
        isDead = true;
        inputControl.GamePlay.Disable();
    }

    private void PlayerAttack(InputAction.CallbackContext context)
    {
        isAttack = true;
        playerAnimation.SetAttackAnim();
    }

    public void CheckMaterial()
    {
        capsuleCollider.sharedMaterial = physicCheck.isGround ? nomal : wall;
    }
    /// <summary>
    /// run and walk 切换
    /// </summary>
/*    private void Accelerate(InputAction.CallbackContext context)
    {
        if(context.started)
        {
            accelerateValue *= 2.0f;
        }
        if (context.canceled)
        {
            accelerateValue = 0.5f;
        }
    }*/

    private void OnDisable()
    {
        inputControl.Disable();
    }
}
