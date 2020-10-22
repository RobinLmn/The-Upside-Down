using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TheFirstPerson;

public class RoamState : State // Monster is roaming around the map
{
    MonsterScript _monsterScript;
    FPSController _player;

    float roamTimer = 0f;

    public RoamState(MonsterScript monsterScript, FPSController player)
    {
        _player = player;
        _monsterScript = monsterScript;
    }

	public override void StartState()
	{
        roamTimer = 0f;
        _monsterScript.SetSpeedZero();
    }

	// Runs in Update
	public override IEnumerator Do()
    {
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

    bool IsPlayerTooClose() // If player is in aggro range, transition to search
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
