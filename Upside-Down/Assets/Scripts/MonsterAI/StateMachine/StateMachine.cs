using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public abstract class StateMachine
{
    protected State myState;
    protected Dictionary<Type, State> allStates = new Dictionary<Type, State>();

    public void SetState(State aState)
    {
        myState = aState;
    }

    public void AddState(Type aType, State aStateInstance)
    {
        allStates.Add(aType, aStateInstance);
    }

    public State GetState()
    {
        return myState;
    }

    public void UpdateState()
    {
        myState.Do();

        // Change state if applicable
        State prevState = myState;
        Type newStateType = myState.GetTransition();
        myState = allStates[newStateType];

        if (myState  != prevState) 
        {
            prevState.ExitState();
            myState.StartState(); 
        }
    }
}
