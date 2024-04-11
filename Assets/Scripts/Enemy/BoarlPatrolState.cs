using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BoarlPatrolState : BaseState
{
    public Enemy currentEnemy;
    
    public override void OnEnter(Enemy enemy)
    {
        currentEnemy = enemy;
    }

    public override void LogicUpdate()
    {
        if (!currentEnemy.physicCheck.isGround || (currentEnemy.physicCheck.isWallLeft && currentEnemy.faceDir.x < 0) || (currentEnemy.physicCheck.isWallRight && currentEnemy.faceDir.x > 0))
        {
            currentEnemy.wait = true;
            currentEnemy.anim.SetBool("walk", false);
        }
        else
        {
            currentEnemy.anim.SetBool("walk", true);
        }
    }

    public override void PhysicUpdate()
    {
        
    }

    public override void OnEnd()
    {
        
    }
}
