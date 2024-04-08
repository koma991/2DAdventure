using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Boarl : Enemy
{

    private void FixedUpdate()
    {
        Move();
    }

    public override void Move()
    {
        base.Move();
        anim.SetBool("walk", true);
    }
}
