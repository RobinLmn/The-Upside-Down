using System.Collections;
using System;
using UnityEngine;
using TheFirstPerson;

public class SearchState : State
{
    MonsterAI myMonster;
    FPSController myPlayer;

    bool isPlayerTooSlow;

    float myCooldownTimer = 0;
    float myCooldown;

    SpeedCheck myMinSpeed;

    public SearchState(MonsterAI aMonster, FPSController aPlayer, SpeedCheck minSpeed, float aCooldwon)
    {
        myPlayer = aPlayer;
        myMonster = aMonster;

        myCooldown = aCooldwon;
        myMinSpeed = minSpeed;
    }

    public override void StartState()
    {
        myCooldownTimer = 0;
    }

    public override IEnumerator Do()
    {
        Vector3 tempVel = Vector3.Lerp(myMonster.monsterRb.velocity, myMonster.monsterObject.monsterSpeedInSearch * myMonster.monsterObject.GetVectorToPlayer(), 0.1f);
        tempVel.y = 0f;
        myMonster.monsterRb.velocity = tempVel;

        // Monster searches
        /** TODO : Feedback **/

        // Checks if player too slow
        bool minimPlayerSpeed = myMinSpeed.mySpeed >= PlayerManager.instance.GetCurrentSpeed();
        isPlayerTooSlow = Timer(myMinSpeed.myTimeLimit, minimPlayerSpeed);

        // Update cooldown
        myCooldownTimer += Time.deltaTime;

        return base.Do();
    }

    public override Type GetTransition()
    {
        if (isPlayerTooSlow)
        {
            Debug.Log("Transitioning from SearchState to AttackState");
            myMonster.SetSpeedZero();
            return typeof(AttackState);
        }
        else if (PlayerManager.instance.IsHiding || myCooldownTimer > myCooldown)
        {
            Debug.Log("Transitioning from SearchState to RoamState");
            myMonster.SetSpeedZero();
            return typeof(RoamState);
        }
        else
        {
            return typeof(SearchState);
        }
    }
}
