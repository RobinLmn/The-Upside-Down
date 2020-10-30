using System.Collections;
using System;
using UnityEngine;
using TheFirstPerson;
using UnityEngine.AI;

public class ChaseState : State
{
    float chaseTimer = 0f;

    MonsterScript _monsterScript;
    Rigidbody _monsterRb;
    NavMeshAgent _agent;
    FPSController _player;

    ParticleSystem chaseStateFog;

    private RaycastHit hit;

    public ChaseState(MonsterScript monsterScript, FPSController player)
    {
        _player = player;
        _monsterScript = monsterScript;
        _monsterRb = _monsterScript.GetComponent<Rigidbody>();
        _agent = _monsterScript.GetComponent<NavMeshAgent>();
    }

    public override void StartState()
    {
        chaseStateFog = GameObject.Find("ChaseStateFog").GetComponent<ParticleSystem>();
        chaseStateFog.Play();
        GameObject.Find("AudioManager").GetComponent<AudioManager>().Play("MonsterCry");

        chaseTimer = 0f;
    }

    public override IEnumerator Do()
    {
        MoveTowardsPlayer();

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

    private void MoveTowardsPlayer() // Raycast player position to the ground. Move towards this position
    {
        int layerMask = LayerMask.GetMask("Ground");
        Physics.Raycast(_player.transform.position, Vector3.down, out hit, layerMask);



        _agent.SetDestination(hit.point);
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
