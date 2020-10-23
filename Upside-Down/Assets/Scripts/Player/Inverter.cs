using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TheFirstPerson;
using Cinemachine;

public class Inverter : MonoBehaviour
{
    public AudioManager audioManager;
    public FPSController FC;
    public Hider hider;

    public float gravityFlipTime;

    [SerializeField]public bool inverted = false;

    public Transform feet;
    public Transform feetN;
    public Transform feetI;

    public CinemachineVirtualCamera normalVCamera;
    public CinemachineVirtualCamera invertedVCamera;

    public delegate void OnInvert();
    public static event OnInvert onInvert;

    float gft;

    // Start is called before the first frame update
    void Start()
    {
        if (inverted)
            Invert();

        audioManager = GameObject.Find("AudioManager").GetComponent<AudioManager>();
    }

    private void Update()
    {
        if (Input.GetKeyDown("e"))
        {
            Debug.Log("invert");
            Invert();
        }
    }

    // Update is called once per frame
    public void Invert()
    {
		onInvert();

		inverted = !inverted;
		FC.sensitivity = -FC.sensitivity;
		gft = FC.baseGroundForce;
		FC.baseGroundForce = -FC.baseGroundForce;
		FC.maxGroundForce = -FC.maxGroundForce;

		FC.strafeMult = -FC.strafeMult;
        if (inverted)
            feet.localPosition = feetI.localPosition;
        else
            feet.localPosition = feetN.localPosition;

        if (inverted) // spaghetti code please ignore for now
        {
            invertedVCamera.enabled = true;
            normalVCamera.enabled = false;
        }
        else if (!inverted)
        {
            normalVCamera.enabled = true;
            invertedVCamera.enabled = false;
        }
<<<<<<< HEAD

=======
        startTime = Time.time;
        endTime = Time.time + cameraTurnTime;
        audioManager.Play("GravityReverse");
>>>>>>> main
		StartCoroutine(LerpGravity());
	}

    // NOTE: Player currently falls much faster in one direction than in the other. Ignoring for now, might not be noticeable
	private IEnumerator LerpGravity()
	{
		Vector3 endPhysicsGravity = -Physics.gravity;
		float endFCGravity = -FC.gravity;
		float endGravityCap = -FC.gravityCap;
		float endFallVelocity = -FC.baseFallVelocity;

		float gravityTimer = 0f;

		while (gravityTimer <= gravityFlipTime)
		{
			gravityTimer += Time.fixedDeltaTime;
			Physics.gravity = Vector3.Lerp(Vector3.zero, endPhysicsGravity, (gravityTimer) / gravityFlipTime);
			FC.gravityCap = Mathf.Lerp(0, endGravityCap, (gravityTimer) / gravityFlipTime);
			FC.gravity = Mathf.Lerp(0, endFCGravity, (gravityTimer) / gravityFlipTime);
			FC.baseFallVelocity = Mathf.Lerp(0, endFallVelocity, (gravityTimer) / gravityFlipTime);

			yield return new WaitForFixedUpdate();
		}
	}
}
