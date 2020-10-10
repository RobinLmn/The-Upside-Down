using UnityEngine;

public class MonsterAI : StateMachine
{

    public MonsterAI(PlayerController aPlayer, SpeedCheck minSpeed_Roam, SpeedCheck maxSpeed_Roam, SpeedCheck minSpeed_Search, float maxSearchTime)
    {
        AddState(typeof(SearchState), new SearchState(this, aPlayer, minSpeed_Search, maxSearchTime));
        AddState(typeof(AttackState), new AttackState(this));
        AddState(typeof(RoamState), new RoamState(this, aPlayer, minSpeed_Roam, maxSpeed_Roam));

        myState = allStates[typeof(RoamState)];
        Debug.Log("Starts in RoamState");
    }
}

public struct SpeedCheck
{
    public float mySpeed;
    public float myTimeLimit;

    public SpeedCheck(float aSpeed, float aTimeLimit)
    {
        mySpeed = aSpeed;
        myTimeLimit = aTimeLimit;
    }
}