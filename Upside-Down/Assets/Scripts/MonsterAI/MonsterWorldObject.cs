using System;
using System.Collections.Generic;
using UnityEngine;

public class MonsterWorldObject : MonoBehaviour
{
    MonsterAI myMonsterAI;
    [SerializeField] private PlayerController myPlayer;
    [SerializeField] private float myMinimumSearchSpeed;
    [SerializeField] private float myMinimumRoamSpeed;

    private void Awake()
    { 
        myMonsterAI = new MonsterAI(myPlayer, myMinimumRoamSpeed, myMinimumSearchSpeed);
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
