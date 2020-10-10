using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class State
{
    float timer;
    float timer2;

    public virtual void StartState()
    {
        return;
    }
    public virtual IEnumerator Do()
    {
        yield break;
    }

    public virtual Type GetTransition()
    {
        return null;
    }

    public bool Timer(float timeLimit, bool condition)
    {
        if (condition)
        {
            if (timer < timeLimit)
            {
                timer += Time.deltaTime;
            }
            else
            {
                timer = 0;
                return true;
            }
        }
        else
        {
            timer = 0;
        }

        return false;
    }

    public bool Timer2(float timeLimit, bool condition)
    {
        if (condition)
        {
            if (timer2 < timeLimit)
            {
                timer2+= Time.deltaTime;
            }
            else
            {
                timer2 = 0;
                return true;
            }
        }
        else
        {
            timer2 = 0;
        }

        return false;
    }
}
