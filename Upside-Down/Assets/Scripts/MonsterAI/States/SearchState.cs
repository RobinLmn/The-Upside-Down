using System.Collections;
using System;
using System.Collections.Generic;
using UnityEngine;

public class SearchState : State
{
    MonsterAI myMonster;
    PlayerController myPlayer;

    bool isPlayerTooStill;
    bool isPlayerHiding;
    bool cooldownReached;

    float myMinimPlayerSpeed;

    public SearchState(MonsterAI aMonster, PlayerController aPlayer, float aMinimSpeed)
    {
        myMinimPlayerSpeed = aMinimSpeed;
        myPlayer = aPlayer;
        myMonster = aMonster;
    }

    public override IEnumerator Do()
    {
        // Monster searches

        // Checks if player too still
        isPlayerTooStill = PlayerIsTooStill(3, myMinimPlayerSpeed, myPlayer);

        return base.Do();
    }

    public override Type GetTransition()
    {
        if (isPlayerTooStill)
        {
            Debug.Log("Transitionning in AttackState");
            return typeof(AttackState);
        }
        else if (isPlayerHiding || cooldownReached)
        {
            Debug.Log("Transitionning in RoamState");
            return typeof(RoamState);
        }
        else
        {
            return typeof(SearchState);
        }
    }
}
