using UnityEngine;
using TheFirstPerson;

public class MonsterAI : StateMachine
{
    // Shouldn't have these here, but for a gamejam we can let best practice slip (Hansen)
    public MonsterWorldObject monsterObject;
    public Rigidbody monsterRb;

    public MonsterAI(MonsterWorldObject aMonster, FPSController aPlayer, SpeedCheck minSpeed_Roam, SpeedCheck maxSpeed_Roam, SpeedCheck minSpeed_Search, float maxSearchTime)
    {
        monsterObject = aMonster;
        monsterRb = monsterObject.GetComponent<Rigidbody>();

        AddState(typeof(SearchState), new SearchState(this, aPlayer, minSpeed_Search, maxSearchTime));
        AddState(typeof(AttackState), new AttackState(this));
        AddState(typeof(RoamState), new RoamState(this, aPlayer, minSpeed_Roam, maxSpeed_Roam));

        myState = allStates[typeof(RoamState)]; // Starts in Roam state
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