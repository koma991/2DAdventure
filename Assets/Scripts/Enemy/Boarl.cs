using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Boarl : Enemy
{

    public override void Move()
    {
        base.Move();
        anim.SetBool("walk", true);
    }


}
