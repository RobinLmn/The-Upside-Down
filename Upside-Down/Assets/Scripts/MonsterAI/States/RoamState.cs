using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RoamState : State
{
    MonsterAI myMonster;
    PlayerController myPlayer;

    bool myPlayerIsTooSlow = false;
    bool myPlayerIsTooFast = false;

    SpeedCheck myMinSpeed;
    SpeedCheck myMaxSpeed;

    public RoamState(MonsterAI aMonster, PlayerController aPlayer, SpeedCheck minSpeed, SpeedCheck maxSpeed)
    {
        myPlayer = aPlayer;
        myMonster = aMonster;

        myMinSpeed = minSpeed;
        myMaxSpeed = maxSpeed;
    }

    // Runs in Update
    public override IEnumerator Do()
    {
        // Monster is roaming around the map
        /** TODO : Feedback **/

        // Checks if player is too slow
        bool playerIsTooSlow = myPlayer.GetCurrentSpeed() <= myMinSpeed.mySpeed;
        myPlayerIsTooSlow = Timer(myMinSpeed.myTimeLimit, playerIsTooSlow);

        // Checks if player is too fast / too loud
        bool playerIsTooFast = myPlayer.GetCurrentSpeed() >= myMaxSpeed.mySpeed;
        myPlayerIsTooFast = Timer2(myMaxSpeed.myTimeLimit, playerIsTooFast);

        return base.Do();
    }

    public override Type GetTransition()
    {
        if (myPlayerIsTooSlow || myPlayerIsTooFast)
        {
            Debug.Log("Transitioning from RoamState to SearchState");
            return typeof(SearchState);
        }
        else return typeof(RoamState);
    }
}
