using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TheFirstPerson;

public class AttackState : State
{
    MonsterScript _monsterScript;
    FPSController _player;

    public bool isGameOver = false;

    public AttackState(MonsterScript monsterScript, FPSController player)
    {
        _player = player;
        _monsterScript = monsterScript;
    }

    public override void StartState()
<<<<<<< HEAD
    {
        _monsterScript.SetSpeedZero();
        Debug.Log("Player Dies");

        // Gameover logic should live in a GameManager class. Modify later
=======
    {
        myMonsterAi.SetSpeedZero();
        Debug.Log("Player Dies");
        GameObject.Find("AudioManager").GetComponent<AudioManager>().Play("MonsterEating");
>>>>>>> main
        isGameOver = true;
    }

    public override IEnumerator Do()
    {
        /** TODO : Respawn Mechanic **/
        yield break;
    }

    public override Type GetTransition()
    {
        if (isGameOver)
        {
            Debug.Log("Transitioning from AttackState into RoamState");
            return typeof(RoamState);
        }
        return typeof(AttackState);
    }
}
