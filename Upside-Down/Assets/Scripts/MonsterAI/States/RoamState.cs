using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RoamState : State
{
    MonsterAI myMonster;
    PlayerController myPlayer;
    float myPlayerMinimSpeed;
    float timer = 0f;

    bool myPlayerIsTooStill = false;

    public RoamState(MonsterAI aMonster, PlayerController aPlayer, float aPlayerMinimSpeed)
    {
        myPlayer = aPlayer;
        myPlayerMinimSpeed = aPlayerMinimSpeed;
        myMonster = aMonster;
    }

    // Runs in Update
    public override IEnumerator Do()
    {
        // Monster is roaming around the map

        // Checks if player is too still
        myPlayerIsTooStill = PlayerIsTooStill(5f, myPlayerMinimSpeed, myPlayer);

        return base.Do();
    }

    public override Type GetTransition()
    {
        if (myPlayerIsTooStill)
        {
            Debug.Log("Transitioning from RoamState to SearchState");
            return typeof(SearchState);
        }
        else return typeof(RoamState);
    }
}
