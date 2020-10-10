using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MonsterAI : StateMachine
{

    public MonsterAI(PlayerController aPlayer, float aMinimRoamSpeed, float aMinimSearchSpeed)
    {
        AddState(typeof(SearchState), new SearchState(this, aPlayer, aMinimSearchSpeed));
        AddState(typeof(AttackState), new AttackState(this));
        AddState(typeof(RoamState), new RoamState(this, aPlayer, aMinimRoamSpeed));

        myState = allStates[typeof(RoamState)];
        Debug.Log("Starts in RoamState");
    }
}
