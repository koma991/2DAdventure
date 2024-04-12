using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BoarlChaseState : BaseState
{

    public override void OnEnter(Enemy enemy)
    {
        currentEnemy = enemy;
        currentEnemy.currentSpeed = currentEnemy.pursueSpeed;
        currentEnemy.anim.SetBool("run", true);
    }

    public override void LogicUpdate()
    {
        if (currentEnemy.LostCurrentTime <= 0)
            currentEnemy.SwitchState(NPCState.Patrol);

        if (!currentEnemy.physicCheck.isGround || (currentEnemy.physicCheck.isWallLeft && currentEnemy.faceDir.x < 0) || (currentEnemy.physicCheck.isWallRight && currentEnemy.faceDir.x > 0))
        {
            currentEnemy.transform.localScale = new Vector3(currentEnemy.faceDir.x, 1, 1);
        }
    }

    public override void PhysicUpdate()
    {

    }

    public override void OnEnd()
    {
        currentEnemy.anim.SetBool("run", false);
    }
}
