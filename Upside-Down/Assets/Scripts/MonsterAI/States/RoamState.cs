using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TheFirstPerson;

public class RoamState : State
{
    MonsterAI myMonster;
    FPSController myPlayer;

    bool myPlayerIsTooSlow = false;
    bool myPlayerIsTooFast = false;

    SpeedCheck myMinSpeed;
    SpeedCheck myMaxSpeed;

    public RoamState(MonsterAI aMonster, FPSController aPlayer, SpeedCheck minSpeed, SpeedCheck maxSpeed)
    {
        myPlayer = aPlayer;
        myMonster = aMonster;

        myMinSpeed = minSpeed;
        myMaxSpeed = maxSpeed;
    }

    // Runs in Update
    public override IEnumerator Do()
    {
        // Monster moves towards the player. 
        /** TODO : Add randomization to roaming? Right now, the monster just searches slower. Add navmesh agent for pathfinding? **/
		//Vector3 tempVel = Vector3.Lerp(myMonster.monsterRb.velocity, myMonster.monsterObject.monsterSpeedInRoam * myMonster.monsterObject.GetVectorToPlayer(), 0.5f);
  //      tempVel.y = 0f;
  //      myMonster.monsterRb.velocity = tempVel;

        // Monster is roaming around the map
        /** TODO : Feedback **/
        // Checks if player is too slow
        bool playerIsTooSlow = PlayerManager.instance.GetCurrentSpeed() <= myMinSpeed.mySpeed;
        myPlayerIsTooSlow = Timer(myMinSpeed.myTimeLimit, playerIsTooSlow);

        // Checks if player is too fast / too loud
        bool playerIsTooFast = PlayerManager.instance.GetCurrentSpeed() >= myMaxSpeed.mySpeed;
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
