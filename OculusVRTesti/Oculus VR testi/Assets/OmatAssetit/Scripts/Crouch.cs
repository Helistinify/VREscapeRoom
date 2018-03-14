using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.XR;

public class Crouch : MonoBehaviour {
    
    Vector3 offset;
    GameManager GMscript;
    private SteamVR_Controller.Device device;
    public SteamVR_TrackedObject controller;
    Vector2 touchpadFingerPos;

    // Use this for initialization
    void Start () {
        transform.parent.parent.GetComponent<CheckBoolsCrouchTeleporting>().setIsCrouching(false);
        GMscript = GameObject.FindGameObjectWithTag("GameManager").GetComponent<GameManager>();
        if (XRDevice.model == "Vive MV")
        {
            device = GetComponent<Teleporting>().GetDevice();
        }
    }
	
	// Update is called once per frame
	void Update () {
        if (XRDevice.model == "Oculus Rift CV1")
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
        else if(XRDevice.model == "Vive MV")
        {
            if (device.GetTouch(Valve.VR.EVRButtonId.k_EButton_SteamVR_Touchpad))
            {
                touchpadFingerPos = device.GetAxis(Valve.VR.EVRButtonId.k_EButton_SteamVR_Touchpad);
            }
            if (device.GetPressDown(Valve.VR.EVRButtonId.k_EButton_SteamVR_Touchpad) && touchpadFingerPos.y < -0.5f && transform.parent.parent.GetComponent<CheckBoolsCrouchTeleporting>().getWhichController() == null)
            {
                transform.parent.parent.GetComponent<CheckBoolsCrouchTeleporting>().setWhichController(gameObject);
                SetCrouch(true);
                transform.parent.parent.GetComponent<CheckBoolsCrouchTeleporting>().setIsCrouching(true);
            }
            if (device.GetPressUp(Valve.VR.EVRButtonId.k_EButton_SteamVR_Touchpad) && transform.parent.parent.GetComponent<CheckBoolsCrouchTeleporting>().getWhichController() != null)
            {
                if (transform.parent.parent.GetComponent<CheckBoolsCrouchTeleporting>().getWhichController().transform.name == transform.name)
                {
                    transform.parent.parent.GetComponent<CheckBoolsCrouchTeleporting>().setWhichController(null);
                    SetCrouch(false);
                    transform.parent.parent.GetComponent<CheckBoolsCrouchTeleporting>().setIsCrouching(false);
                }
            }
        }
    }

    void SetCrouch(bool b)
    {
        if (XRDevice.model == "Oculus Rift CV1")
        {
            if (b)
                transform.position -= offset;
            else
                transform.position += offset;
        }
        if (XRDevice.model == "Vive MV")
        {
            if (b && !transform.parent.parent.GetComponent<CheckBoolsCrouchTeleporting>().getIsCrouching())
            {
                // Raycast down to check how low the player will be set for crouching
                RaycastHit hit;
                if (Physics.Raycast(transform.parent.Find("Camera (eye)").transform.position, Vector3.down, out hit))
                {
                    if (hit.transform.tag == "Ground")
                    {
                        offset = transform.parent.Find("Camera (eye)").transform.position - hit.point;
                        offset.y *= 0.45f;
                    }
                }
                transform.parent.parent.position -= offset;
            }
            else if (!b && transform.parent.parent.GetComponent<CheckBoolsCrouchTeleporting>().getIsCrouching())
            {
                transform.parent.parent.position += offset;
            }
        }
    }
}
