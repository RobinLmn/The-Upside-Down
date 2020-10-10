using System;
using System.Collections.Generic;
using UnityEngine;

public class MonsterWorldObject : MonoBehaviour
{
    MonsterAI myMonsterAI;
    [SerializeField] private PlayerController myPlayer;

    [SerializeField] private float minSpeedInRoam;
    [SerializeField] private float minTimeSpeedInRoam;

    [SerializeField] private float maxSpeedInRoam;
    [SerializeField] private float maxTimeSpeedInRoam;

    [SerializeField] private float minSpeedInSearch;
    [SerializeField] private float minTimeSpeedInSearch;

    [SerializeField] private float maxSearchTime;

    private void Awake()
    {
        SpeedCheck minSpeed_Roam = new SpeedCheck(minSpeedInRoam, minTimeSpeedInRoam);
        SpeedCheck maxSpeed_Roam = new SpeedCheck(maxSpeedInRoam, maxTimeSpeedInRoam);

        SpeedCheck minSpeed_Search = new SpeedCheck(minSpeedInSearch, minTimeSpeedInSearch);

        myMonsterAI = new MonsterAI(myPlayer, minSpeed_Roam, maxSpeed_Roam, minSpeed_Search, maxSearchTime);
    }

    void DebugState()
    {
        Debug.Log(myMonsterAI.GetState());
    }

    void Update()
    {
        myMonsterAI.UpdateState();
    }
}
