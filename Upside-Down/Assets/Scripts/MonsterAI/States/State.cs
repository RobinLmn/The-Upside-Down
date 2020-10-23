using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class State
{
    public virtual void StartState()
    {
        return;
    }
    public virtual IEnumerator Do()
    {
        yield break;
    }
    public virtual void ExitState()
    {
        return;
    }
    public virtual Type GetTransition()
    {
        return null;
    }
}
