using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.VR;

//using Valve.VR;

public class SteamMapControls : MonoBehaviour
{

	//	SteamVR_Controller.Device device;
	//	SteamVR_TrackedObject trackedobj;
	//
	//	Vector2 touchpad;
	//
	//	//private float sensitivity = 3.5f;
	//
	//	void Start ()
	//	{
	//		if (VRDevice.isPresent) {
	//			trackedobj = GetComponent<SteamVR_TrackedObject> ();
	//			device = SteamVR_Controller.Input ((int)trackedobj.index);
	//		}
	//	}
	//
	//	void Update ()
	//	{
	//
	//		if (VRDevice.isPresent) {
	//			//If finger is on touchpad
	//			if (device.GetTouch (SteamVR_Controller.ButtonMask.Touchpad)) {
	//				//Read the touchpad values
	//				touchpad = device.GetAxis (EVRButtonId.k_EButton_SteamVR_Touchpad);
	//				//Debug.Log (touchpad);
	//			}
	//		} else {
	//			touchpad = Input.mousePosition.normalized;
	//			//Debug.Log (touchpad.x.ToString ("F4"));
	//		}
	//	}
}
