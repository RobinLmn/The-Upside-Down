﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TheFirstPerson;

public class Inverter : MonoBehaviour
{
    public AudioManager audioManager;
    public FPSController FC;
    public float cameraTurnTime;
    public float gravityFlipTime;
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
        if (inverted)
            Invert();
        cam.transform.localPosition = normalCam.transform.localPosition;
        audioManager = GameObject.Find("AudioManager").GetComponent<AudioManager>();
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
            cam.transform.localPosition = Vector3.Lerp(start, goal, (Time.time - startTime) / cameraTurnTime);
            cam.transform.localRotation = Quaternion.Lerp(rotateStart, rotateGoal, (Time.time - startTime) / cameraTurnTime);
        }
    }

    // Update is called once per frame
    public void Invert()
    {
        inverted = !inverted;
		//Physics.gravity = -Physics.gravity;
		FC.sensitivity = -FC.sensitivity;
		//FC.gravity = -FC.gravity;
		gft = FC.baseGroundForce;
		FC.baseGroundForce = -FC.baseGroundForce;
		FC.maxGroundForce = -FC.maxGroundForce;
		//FC.gravityCap = -FC.gravityCap;
		//FC.baseFallVelocity = -FC.baseFallVelocity;
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
        endTime = Time.time + cameraTurnTime;
        audioManager.Play("GravityReverse");
		StartCoroutine(LerpGravity());
	}

    // NOTE: Player currently falls much faster in one direction than in the other. Ignoring for now, might not be noticeable
	private IEnumerator LerpGravity()
	{
		Vector3 startPhysicsGravity = Physics.gravity;
		//float startSensitivity = FC.sensitivity;
		float startFCGravity = FC.gravity;
		float startGravityCap = FC.gravityCap;


		Vector3 endPhysicsGravity = -Physics.gravity;
		//float endSensitivity = -FC.sensitivity;
		float endFCGravity = -FC.gravity;
		float endGravityCap = -FC.gravityCap;
		float endFallVelocity = -FC.baseFallVelocity;

		float gravityTimer = 0f;

		while (gravityTimer <= gravityFlipTime)
		{
			gravityTimer += Time.fixedDeltaTime;
			Physics.gravity = Vector3.Lerp(Vector3.zero, endPhysicsGravity, (gravityTimer) / gravityFlipTime);
			//FC.sensitivity = Mathf.Lerp(startSensitivity, endSensitivity, (gravityTimer) / gravityFlipTime);
			FC.gravityCap = Mathf.Lerp(0, endGravityCap, (gravityTimer) / gravityFlipTime);
			FC.gravity = Mathf.Lerp(0, endFCGravity, (gravityTimer) / gravityFlipTime);
			FC.baseFallVelocity = Mathf.Lerp(0, endFallVelocity, (gravityTimer) / gravityFlipTime);

            //Debug.Log(Physics.gravity);

			yield return new WaitForFixedUpdate();
		}
	}
}
