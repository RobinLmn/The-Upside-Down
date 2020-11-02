using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TheFirstPerson;
using UnityEngine.SceneManagement;

public class AttackState : State
{
    MonsterScript _monsterScript;
    FPSController _player;
    ParticleSystem deathParticles;
    public bool isGameOver = false;

    public AttackState(MonsterScript monsterScript, FPSController player)
    {
        _player = player;
        _monsterScript = monsterScript;
    }

    public override void StartState()
    {
        _monsterScript.SetSpeedZero();
        Debug.Log("Player Dies");

        // Gameover logic should live in a GameManager class. Modify later
        isGameOver = true;
        GameObject.Find("AudioManager").GetComponent<AudioManager>().Play("MonsterEating");
        deathParticles = GameObject.Find("DeathParticles").GetComponent<ParticleSystem>();
        deathParticles.Play();
        GameObject.Find("GameManager").GetComponent<GameManager>().endGame();
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
