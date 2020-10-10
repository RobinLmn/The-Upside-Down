using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AttackState : State
{
    MonsterAI myMonster;
    bool isGameOver = false;

    public AttackState(MonsterAI aMonster)
    {
        myMonster = aMonster;
    }

    public override void StartState()
    {
        isGameOver = true;
    }

    public override IEnumerator Do()
    {
        /** TODO : Respawn Mechanic **/
        Debug.Log("Player Dies"); 
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
