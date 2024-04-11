using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Boarl : Enemy
{
    protected override void Awake()
    {
        base.Awake();
        patrolState = new BoarlPatrolState();
    }
}
