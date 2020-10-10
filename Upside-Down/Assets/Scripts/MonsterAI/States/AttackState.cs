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

    public override IEnumerator Do()
    {
        if (!isGameOver)
        {
            Debug.Log("Player Dies");
            isGameOver = true;
        }
        yield break;
    }

    public override Type GetTransition()
    {
        if (isGameOver)
        {
            Debug.Log("Transitioning in RoamState");
            return typeof(RoamState);
        }
        return typeof(AttackState);
    }
}
