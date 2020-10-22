using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Cinemachine;

public class HidingCamera : MonoBehaviour // Event in Inverter.cs triggers flip of hiding camera.
{
    public CinemachineVirtualCamera normalVCamera;
    public CinemachineVirtualCamera invertedVCamera;

	//// Start is called before the first frame update
	//void OnEnable()
	//{
	//	Inverter.onInvert += DisableHidingCamera;
	//}


	//void DisableHidingCamera() // Disable hiding camera when inverted
	//{
	//	normalVCamera.enabled = false;
	//	invertedVCamera.enabled = false;
	//}

	//void OnDisable()
	//{
	//	Inverter.onInvert -= DisableHidingCamera;
	//}
}
