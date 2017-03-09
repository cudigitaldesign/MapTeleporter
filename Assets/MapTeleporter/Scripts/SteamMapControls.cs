using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Valve.VR;

public class SteamMapControls : MonoBehaviour {

    SteamVR_Controller.Device device;
    SteamVR_TrackedObject trackedobj;

    Vector2 touchpad;

    private float sensitivity = 3.5f;

    void Start()
    {
        trackedobj = GetComponent<SteamVR_TrackedObject>();
        device = SteamVR_Controller.Input((int)trackedobj.index);
    }


    //void Update()
    //{
    //    float tiltAroundX = device.GetAxis(Valve.VR.EVRButtonId.k_EButton_Axis0).x;

    //    if (device.GetTouch(SteamVR_Controller.ButtonMask.Touchpad))
    //    {
    //        Debug.Log(tiltAroundX);
    //    }

    //    if (device.GetTouchUp(SteamVR_Controller.ButtonMask.Touchpad))
    //    {

    //    }

    //}

    void Update()
    {

        //If finger is on touchpad
        if (device.GetTouch(SteamVR_Controller.ButtonMask.Touchpad))
        {
            //Read the touchpad values
            touchpad = device.GetAxis(EVRButtonId.k_EButton_SteamVR_Touchpad);
            Debug.Log(touchpad);
        }
    }
}
