using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HidingTrigger : MonoBehaviour
{

    public void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("HidingSpot"))
        {
            PlayerManager.instance.IsHiding = true;
        }
    }

    public void OnTriggerExit(Collider other)
    {
        if (other.CompareTag("HidingSpot"))
        {
            PlayerManager.instance.IsHiding = false;
        }
    }
}
