using System;
using System.Collections.Generic;
using UnityEngine;
using TheFirstPerson;

public class MonsterWorldObject : MonoBehaviour
{
    MonsterAI myMonsterAI;
    [SerializeField] private FPSController myPlayer;

    [SerializeField] public float monsterSpeedInRoam;
    [SerializeField] public float monsterSpeedInSearch;
    [SerializeField] public float monsterSpeedInAttack;

    [SerializeField] private float minSpeedInRoam;
    [SerializeField] private float minSpeedAggroTimeInRoam;

    [SerializeField] private float maxSpeedInRoam;
    [SerializeField] private float maxSpeedAggroTimeInRoam;

    [SerializeField] private float minSpeedInSearch;
    [SerializeField] private float minSpeedAggroTimeInSearch;

    [SerializeField] private float maxSearchTime;

    private void Awake()
    {
        SpeedCheck minSpeed_Roam = new SpeedCheck(minSpeedInRoam, minSpeedAggroTimeInRoam);
        SpeedCheck maxSpeed_Roam = new SpeedCheck(maxSpeedInRoam, maxSpeedAggroTimeInRoam);

        SpeedCheck minSpeed_Search = new SpeedCheck(minSpeedInSearch, minSpeedAggroTimeInSearch);

        myMonsterAI = new MonsterAI(this, myPlayer, minSpeed_Roam, maxSpeed_Roam, minSpeed_Search, maxSearchTime);
    }

    void DebugState()
    {
        Debug.Log(myMonsterAI.GetState());
    }

    void Update()
    {
        myMonsterAI.UpdateState();
    }

    public State GetMonsterState()
    {
        return myMonsterAI.GetState();
    }

    public Vector3 GetVectorToPlayer()
    {
        return this.transform.position - myPlayer.transform.position;
	}
}
