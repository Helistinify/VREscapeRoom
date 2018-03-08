using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Crouch : MonoBehaviour {
    
    Vector3 offset;
    GameManager GMscript;
    private SteamVR_Controller.Device device;
    public SteamVR_TrackedObject controller;
    Vector2 touchpadFingerPos;

    // Use this for initialization
    void Start () {
        offset = new Vector3(0f, 1.2f, 0f);
        GMscript = GameObject.FindGameObjectWithTag("GameManager").GetComponent<GameManager>();
        device = SteamVR_Controller.Input((int)controller.index);
    }
	
	// Update is called once per frame
	void Update () {
        if (GMscript.VRModel == "Oculus Rift CV1")
        {
            if (Input.GetButtonDown("Cancel")) // When the button is pressed down
            {
                SetCrouch(true);
            }
            if (Input.GetButtonUp("Cancel"))// When the button is released
            {
                SetCrouch(false);
            }
        } 
        else if(GMscript.VRModel == "Vive MV")
        {
            if(device.GetTouch(Valve.VR.EVRButtonId.k_EButton_SteamVR_Touchpad))
            {
                touchpadFingerPos = device.GetAxis(Valve.VR.EVRButtonId.k_EButton_SteamVR_Touchpad);
            }
            if (device.GetPressDown(Valve.VR.EVRButtonId.k_EButton_SteamVR_Touchpad) && touchpadFingerPos.y < -0.5f)
            {
                SetCrouch(true);
            }
            if (device.GetPressUp(Valve.VR.EVRButtonId.k_EButton_SteamVR_Touchpad))
            {
                SetCrouch(false);
            }
        }
    }

    void SetCrouch(bool b)
    {
        if (b)
            transform.parent.parent.position -= offset;
        else
            transform.parent.parent.position += offset;
    }
}
