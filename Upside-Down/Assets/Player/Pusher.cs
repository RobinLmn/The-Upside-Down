using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TheFirstPerson;

public class Pusher : MonoBehaviour
{
    // this script pushes all rigidbodies that the character touches
    public float pushPower = 2.0f;
    //FPSController FC;
    CharacterController cc;
    public float upward;

    private void Start()
    {
        //FC = this.GetComponent<FPSController>();
        cc = GetComponent<CharacterController>();
    }
    void OnControllerColliderHit(ControllerColliderHit hit)
    {
        Rigidbody body = hit.collider.attachedRigidbody;

        // no rigidbody
        if (body == null || body.isKinematic)
        {
            return;
        }

        // We dont want to push objects below us
        //if (hit.moveDirection.y < -0.3 || hit.moveDirection.y > -0.3)
        //{
        //   Debug.Log("cringe");
        //    return;
        //}

        // Calculate push direction from move direction,
        // we only push objects to the sides never up and down
        Vector3 pushDir = new Vector3(hit.moveDirection.x, upward, hit.moveDirection.z);

        // If you know how fast your character is trying to move,
        // then you can also multiply the push velocity by that.

        // Apply the push
        Debug.Log("based");
        body.velocity = pushDir * pushPower *cc.velocity.magnitude * cc.velocity.magnitude;
    }
}
