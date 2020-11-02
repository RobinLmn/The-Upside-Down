using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TheFirstPerson;
using UnityEngine.AI;

public class RoamState : State // Monster is roaming around the map
{
    MonsterScript _monsterScript;
    NavMeshAgent _agent;
    FPSController _player;

    ParticleSystem chaseStateFog;

    float roamTimer = 0f;

    private RaycastHit hit;

    public RoamState(MonsterScript monsterScript, FPSController player)
    {
        _player = player;
        _monsterScript = monsterScript;

        _agent = _monsterScript.GetComponent<NavMeshAgent>();
    }

	public override void StartState()
	{
        roamTimer = 0f;
        _monsterScript.SetSpeedZero();

        chaseStateFog = GameObject.Find("ChaseStateFog").GetComponent<ParticleSystem>();
        chaseStateFog.Stop();

        _agent.speed = _monsterScript.roamSpeed;
    }

	// Runs in Update
	public override IEnumerator Do()
    {
        MoveTowardsPlayer();
        // Make monster move in a random direction using navmesh

        roamTimer += Time.deltaTime;

        return base.Do();
    }

    public override Type GetTransition()
    {
        if (IsPlayerTooClose() && IsChaseCooldownReady())
        {
            Debug.Log("Transitioning from RoamState to ChaseState");
            return typeof(ChaseState);
        }
        else return typeof(RoamState);
    }

    private void MoveTowardsPlayer() // Raycast player position to the ground. Move towards this position
    {
        int layerMask = LayerMask.GetMask("Ground");
        Physics.Raycast(_player.transform.position, Vector3.down, out hit, layerMask);



        _agent.SetDestination(hit.point);
    }

    bool IsPlayerTooClose() // If player is in aggro range, transition to chase
    {
        // Make this calculate the path to player instead?
        float distToPlayer = Vector3.Distance(_player.transform.position, _monsterScript.transform.position);

        if (distToPlayer < _monsterScript.roamAggroRange)
            return true;
        else
            return false;
    }

    bool IsChaseCooldownReady()
    {
        // if the monster has roamed longer than the chase cooldown, return true
        return (roamTimer > _monsterScript.chaseCooldown);
    }
}
