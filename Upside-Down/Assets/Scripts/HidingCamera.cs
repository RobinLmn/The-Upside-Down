using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Cinemachine;

public class HidingCamera : MonoBehaviour // Event in Inverter.cs triggers flip of hiding camera.
{
    public CinemachineVirtualCamera normalVCamera;
    public CinemachineVirtualCamera invertedVCamera;

	// Start is called before the first frame update
	void OnEnable()
	{
		Inverter.onInvert += InvertHidingCamera;
	}


	void InvertHidingCamera()
	{
		if (invertedVCamera.enabled || normalVCamera.enabled) // If one of this hiding spot's cameras is enabled, the player is in this spot.
		{
			normalVCamera.enabled = !normalVCamera.enabled;
			invertedVCamera.enabled = !invertedVCamera.enabled;
		}
	}

	void OnDisable()
	{
		Inverter.onInvert -= InvertHidingCamera;
	}
}
