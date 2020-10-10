using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HidingTrigger : MonoBehaviour
{
    float triggerEnterTime = 0;
    bool isHiding = false;

    [SerializeField] private float hidingTime;

    public void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("HidingSpot"))
        {
            isHiding = true;
            triggerEnterTime = 0;
        }
    }

    public void Update()
    {
        if (isHiding)
        {
            if (triggerEnterTime > hidingTime)
            {
                PlayerManager.instance.IsHiding = true;
            }
            else
            {
                triggerEnterTime += Time.deltaTime;
            }
        }
    }

    public void OnTriggerExit(Collider other)
    {
        if (other.CompareTag("HidingSpot"))
        {
            PlayerManager.instance.IsHiding = false;
            isHiding = false;
            triggerEnterTime = 0;
        }
    }
}
