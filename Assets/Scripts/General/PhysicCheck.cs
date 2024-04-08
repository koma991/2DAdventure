using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class PhysicCheck : MonoBehaviour
{

    private CapsuleCollider2D coll;

    [Header("基础条件")]
    public bool isGround;
    public bool isWallLeft;
    public bool isWallRight;
    public bool auto;

    [Header("基础参数")]
    public float checkRadius;
    public LayerMask groundLayer;
    public Vector2 bottomOffset;
    public Vector2 leftOffset;
    public Vector2 rightOffset;

    private void Awake()
    {
        coll = GetComponent<CapsuleCollider2D>();
        if (auto)
        {
            rightOffset = new Vector2((coll.bounds.size.x + coll.offset.x) / 2, coll.bounds.size.y / 2);
            leftOffset = new Vector2(-rightOffset.x, rightOffset.y);
        }
    }

    // Update is called once per frame
    void Update()
    {
        GroundCheck();
        WallCheck();
    }

    public void GroundCheck()
    {
        isGround = Physics2D.OverlapCircle((Vector2)transform.position + bottomOffset,checkRadius,groundLayer);
    }

    public void WallCheck()
    {
        isWallLeft = Physics2D.OverlapCircle((Vector2)transform.position + leftOffset, checkRadius, groundLayer);
        isWallRight = Physics2D.OverlapCircle((Vector2)transform.position + rightOffset, checkRadius, groundLayer);
    }

    private void OnDrawGizmosSelected()
    {
        Gizmos.DrawSphere((Vector2)transform.position + bottomOffset, checkRadius);
        Gizmos.DrawSphere((Vector2)transform.position + leftOffset, checkRadius);
        Gizmos.DrawSphere((Vector2)transform.position + rightOffset, checkRadius);
    }
}
