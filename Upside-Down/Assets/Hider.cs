using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TheFirstPerson;
using Cinemachine;

public class Hider : MonoBehaviour
{
    public CinemachineVirtualCamera mainNormalVCamera;
    public CinemachineVirtualCamera mainInvertedVCamera;
    public InvertManager invertManager;

    public bool isHiding = false;
    private float rayLength = 5f;

    private RaycastHit hit;
    private FPSController fpsController;
    private float moveSpeed;
    private HidingCamera hidingCamera;

    // Start is called before the first frame update
    void Start()
    {
        fpsController = GetComponent<FPSController>();
        moveSpeed = fpsController.moveSpeed;
    }

    // Update is called once per frame
    void Update()
    {
        int layerMask = ~LayerMask.GetMask("Player"); // ignore player layer for raycast

        if (!isHiding)
        {
            if (Physics.Raycast(transform.position, mainNormalVCamera.transform.forward, out hit, rayLength, layerMask))
            {
				//Debug.Log(hit.collider.name);
				//Debug.DrawRay(transform.position, mainVCamera.transform.forward * rayLength, Color.green);

                if (hit.collider.gameObject.CompareTag("HidingSpot"))
                {
                    hidingCamera = hit.collider.gameObject.GetComponent<HidingCamera>();

                    if (Input.GetKeyDown("f"))
                    {
                        Debug.Log("hiding");

                        isHiding = true;
                        mainNormalVCamera.enabled = false;
                        mainInvertedVCamera.enabled = false;

                        if (invertManager.IsInverted()) // disable/enable hiding spot cameras depending on if we're inverted
                        {
                            hidingCamera.invertedVCamera.enabled = true;
                            hidingCamera.normalVCamera.enabled = false;
                        }
                        else
                        {
                            hidingCamera.invertedVCamera.enabled = false;
                            hidingCamera.normalVCamera.enabled = true;
                        }

                        //hidingVCamera = hit.collider.gameObject.GetComponentInChildren<CinemachineVirtualCamera>();
                        //hidingVCamera.enabled = true;
                        fpsController.moveSpeed = 0f;
                    }
                }
            }
        }

        else if (isHiding)
        {
            if (Input.GetKeyDown("f"))
            {
                Debug.Log("Unhiding");

                hidingCamera.invertedVCamera.enabled = false;
                hidingCamera.normalVCamera.enabled = false;

                if (invertManager.IsInverted())
                    mainInvertedVCamera.enabled = true;
                else
                    mainNormalVCamera.enabled = true;

                isHiding = false;
                //hidingVCamera.enabled = false;
                fpsController.moveSpeed = moveSpeed;
            }
        }
    }
}
