using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PhysicCheck : MonoBehaviour
{

    public bool isGround;
    public bool isOverhead;

    public Transform checkPoint;
    public float checkRadius;

    public LayerMask groundLayer;
    public float checkLenght;
    public Vector2 bottomOffset;
    public Vector2 startPos;


    private void Awake()
    {
        checkPoint = GameObject.Find("CheckPoint").transform;
    }


    // Update is called once per frame
    void Update()
    {
        GroundCheck();
    }

    public bool GroundCheck()
    {
        isGround = Physics2D.Raycast((Vector2)this.transform.position + bottomOffset, Vector2.down, checkLenght, groundLayer);
        return isGround;
    }

    public bool CheckOverhead()
    {
        isOverhead = Physics2D.OverlapCircle(checkPoint.position, checkRadius, groundLayer);
        return isOverhead;
    }


    private void OnDrawGizmosSelected()
    {
        startPos = (Vector2)this.transform.position + bottomOffset;
        Gizmos.DrawLine(startPos, startPos + Vector2.down * checkLenght);
        Gizmos.DrawSphere(checkPoint.position + (Vector3)bottomOffset, checkRadius);
    }
}
