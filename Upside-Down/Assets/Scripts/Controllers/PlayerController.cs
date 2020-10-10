using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PlayerController : MonoBehaviour
{
    private CharacterController myCharController;
    [SerializeField] private float mySpeed;

    public Vector3 myVelocity;
    [SerializeField] private float myGravityScaler;
    private const float gravity = -9.81f;

    [SerializeField] private Transform myGroundCheck;
    [SerializeField] private float myGroundDistance;
    [SerializeField] private LayerMask myGroundMask;

    public bool isGrounded;
    private bool isFlying = false;

    [SerializeField] private Transform myCameraTransform;

    void Start()
    {
        myCharController = GetComponent<CharacterController>();
    }

    void Update()
    {
        Movement();
    }

    void FixedUpdate()
    {
        GravityChange();
    }

    private void Movement()
    {
        isGrounded = Physics.CheckSphere(myGroundCheck.position, myGroundDistance, myGroundMask);

        float x = Input.GetAxis("Horizontal");
        float z = Input.GetAxis("Vertical");

        Vector3 move = transform.right * x + transform.forward * z;

        myCharController.Move(move * mySpeed * Time.deltaTime);

        if (isGrounded && myVelocity.y < 0)
        {
            myVelocity.y = -2f * myGravityScaler;
        }

        myVelocity.y += gravity * Time.deltaTime * myGravityScaler;
        myCharController.Move(myVelocity * Time.deltaTime);
       
    }

    private void GravityChange()
    {
        if (Input.GetKey(KeyCode.K))
        {
            AudioManager.instance.Play("GravityReverse");
            myGravityScaler *= -1;
            transform.Rotate(180, 0, 0);
            isGrounded = false;
        }
    }

    public float GetCurrentSpeed()
    {
        Vector2 currVelocity = new Vector2(myVelocity.x, myVelocity.y);
        return currVelocity.magnitude;
    }
}