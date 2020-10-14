using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TheFirstPerson;

public class Inverter : MonoBehaviour
{
    public FPSController FC;
    public float turnTime;
    private float startTime;
    private float endTime = -1;
    private Vector3 goal;
    private Vector3 start;
    [SerializeField] bool inverted;
    public GameObject cam;
    public Transform invCam;
    public Transform normalCam;
    public Transform feet;
    public Transform feetN;
    public Transform feetI;
    Quaternion rotateStart;
    Quaternion rotateGoal;
    float gft;
    // Start is called before the first frame update
    void Start()
    {
        FC = this.GetComponent<FPSController>();
        if (inverted)
            Invert();
        cam.transform.localPosition = normalCam.transform.localPosition;
    }

    private void Update()
    {
        if (Input.GetKeyDown("e"))
            Invert();
    }
    private void FixedUpdate()
    {
        if (Time.time < endTime+0.1)
        {
            cam.transform.localPosition = Vector3.Lerp(start, goal, (Time.time - startTime) / turnTime);
            cam.transform.localRotation = Quaternion.Lerp(rotateStart, rotateGoal, (Time.time - startTime) / turnTime);
        }
    }

    // Update is called once per frame
    public void Invert()
    {
        inverted = !inverted;
        Physics.gravity = -Physics.gravity;
        FC.sensitivity = -FC.sensitivity;
        FC.gravity = -FC.gravity;
        gft = FC.baseGroundForce;
        FC.baseGroundForce = -FC.maxGroundForce;
        FC.maxGroundForce = -gft;
        FC.gravityCap = -FC.gravityCap;
        FC.baseFallVelocity = -FC.baseFallVelocity;
        FC.strafeMult = -FC.strafeMult;
        if (inverted)
        {
            start = cam.transform.localPosition;
            goal = invCam.transform.localPosition;
            rotateStart = cam.transform.localRotation;
            rotateGoal = invCam.transform.localRotation;
            feet.localPosition = feetI.localPosition;
        }
        else
        {
            goal = normalCam.transform.localPosition;
            start = cam.transform.localPosition;
            rotateGoal = normalCam.transform.localRotation;
            rotateStart = cam.transform.localRotation;
            feet.localPosition = feetN.localPosition;
        }
        startTime = Time.time;
        endTime = Time.time + turnTime;
    }
}
