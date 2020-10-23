using System.Collections;
using System;
using UnityEngine;
using TheFirstPerson;

public class SearchState : State
{
    MonsterAI myMonsterAi;
    FPSController myPlayer;
    ParticleSystem searchStateFog;
    AudioManager audioManager;
    
    bool isPlayerTooSlow;

    float myCooldownTimer = 0;
    float myCooldown;

    SpeedCheck myMinSpeed;

    public SearchState(MonsterAI aMonster, FPSController aPlayer, SpeedCheck minSpeed, float aCooldwon)
    {
        myPlayer = aPlayer;
        myMonsterAi = aMonster;

        myCooldown = aCooldwon;
        myMinSpeed = minSpeed;
    }

    public override void StartState()
    {
        myCooldownTimer = 0;
        searchStateFog = GameObject.Find("SearchStateFog").GetComponent<ParticleSystem>();
        searchStateFog.Play();
        GameObject.Find("AudioManager").GetComponent<AudioManager>().Play("MonsterCry");
    }

    public override IEnumerator Do()
    {
        Vector3 tempVel = Vector3.Lerp(myMonsterAi.monsterRb.velocity, myMonsterAi.monsterObject.monsterSpeedInSearch * myMonsterAi.monsterObject.GetVectorToPlayer(), 0.1f);
        tempVel.y = 0f;
        myMonsterAi.monsterRb.velocity = tempVel;

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
        if (isPlayerTooSlow || IsPlayerInAttackRange())
        {
            Debug.Log("Transitioning from SearchState to AttackState");
            return typeof(AttackState);
        }
        else if (PlayerManager.instance.IsHiding || myCooldownTimer > myCooldown)
        {
            Debug.Log("Transitioning from SearchState to RoamState");
            return typeof(RoamState);
        }
        else
        {
            return typeof(SearchState);
        }
    }


    private bool IsPlayerInAttackRange()
    {
        if ((myPlayer.transform.position - myMonsterAi.monsterObject.transform.position).magnitude < myMonsterAi.monsterObject.monsterAttackRange)
            return true;
        return false;
    }
}
