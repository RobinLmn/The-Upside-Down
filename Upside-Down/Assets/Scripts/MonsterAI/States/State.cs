using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class State
{
    float timer = 0;

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

    public bool PlayerIsTooStill(float miniTime, float miniSpeed, PlayerController myPlayer)
    {
        Vector2 playerSpeed = new Vector2(myPlayer.myVelocity.x, myPlayer.myVelocity.z);

        if (playerSpeed.magnitude <= miniSpeed)
        {
            if (timer < miniTime)
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
}
