using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AttackState : State
{
    MonsterAI myMonsterAi;
    bool isGameOver = false;

    public AttackState(MonsterAI aMonster)
    {
        myMonsterAi = aMonster;
    }

    public override void StartState()
    {
        myMonsterAi.SetSpeedZero();
        Debug.Log("Player Dies");
        isGameOver = true;
    }

    public override IEnumerator Do()
    {
        /** TODO : Respawn Mechanic **/
        yield break;
    }

    public override Type GetTransition()
    {
        if (isGameOver)
        {
            Debug.Log("Transitioning into RoamState");
            return typeof(RoamState);
        }
        return typeof(AttackState);
    }
}
