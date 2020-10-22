using UnityEngine;
using TheFirstPerson;

public class MonsterAI : StateMachine
{
    public MonsterAI(MonsterScript monsterScript, FPSController player)
    {
        AddState(typeof(RoamState), new RoamState(monsterScript, player));
        AddState(typeof(ChaseState), new ChaseState(monsterScript, player));
        AddState(typeof(AttackState), new AttackState(monsterScript, player));

        myState = allStates[typeof(RoamState)]; // Starts in Roam state
    }
}