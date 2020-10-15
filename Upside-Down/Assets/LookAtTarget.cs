using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LookAtTarget : MonoBehaviour
{
    public Transform target;

    // Update is called once per frame
    void Update()
    {
        Vector3 relativePos = target.position - transform.position;
        Quaternion lookRotation = Quaternion.LookRotation(relativePos);
        transform.rotation = new Quaternion(0f, lookRotation.y, 0f, lookRotation.w);
    }
}
