using System.Collections;
using System;
using UnityEngine;
using TheFirstPerson;

public class ChaseState : State
{
    float chaseTimer = 0f;

    MonsterScript _monsterScript;
    Rigidbody _monsterRb;
    FPSController _player;

    public ChaseState(MonsterScript monsterScript, FPSController player)
    {
        _player = player;
        _monsterScript = monsterScript;
        _monsterRb = _monsterScript.GetComponent<Rigidbody>();
    }

    public override void StartState()
    {
        chaseTimer = 0f;
    }

    public override IEnumerator Do()
    {
        // Monster runs towards player
        Vector3 tempVel = Vector3.Lerp(_monsterRb.velocity, _monsterScript.chaseSpeed * _monsterScript.GetVectorToPlayer(), 0.1f);
        tempVel.y = 0f;
        _monsterRb.velocity = tempVel;

        // Update timer
        chaseTimer += Time.deltaTime;

        return base.Do();
    }

    public override Type GetTransition()
    {
        // If player is in range, attack
        if (IsPlayerInAttackRange())
        {
            Debug.Log("Transitioning from ChaseState to AttackState");
            return typeof(AttackState);
        }
        // If player is hiding, or the player is out of aggro range, or the chase timer has run out, then go back to roaming
        else if (PlayerManager.instance.IsHiding || IsPlayerOutOfAggroRange() || chaseTimer > _monsterScript.chaseDuration)
        {
            Debug.Log("Transitioning from ChaseState to RoamState");
            return typeof(RoamState);
        }
        else
        {
            return typeof(ChaseState);
        }
    }


    private bool IsPlayerInAttackRange() // If player is in attack range, return true
    {
        if ((_player.transform.position - _monsterScript.transform.position).magnitude < _monsterScript.attackRange)
            return true;
        return false;
    }

    private bool IsPlayerOutOfAggroRange() // If player is out of aggro range, return true
    {
        if ((_player.transform.position - _monsterScript.transform.position).magnitude > _monsterScript.chaseAggroRange)
            return true;
        return false;
    }
}
