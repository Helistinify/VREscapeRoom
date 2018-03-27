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
    public bool tagGround=false;
    GameObject Grandparent; // Parentti

    // Use this for initialization
    void Start () {
        GMscript = GameObject.FindGameObjectWithTag("GameManager").GetComponent<GameManager>();
        if (XRDevice.model == "Vive MV")//------------------------------------Vive-----------------------------------------------------------
        {
            Grandparent = gameObject.transform.parent.parent.gameObject;
            device = GetComponent<Teleporting>().GetDevice();
        }
        else if (XRDevice.model == "Oculus Rift CV1")//------------------------------Oculus--------------------------------------------------------------------------------------
        {
            Grandparent=gameObject.transform.parent.parent.parent.gameObject;
        }
       Grandparent.GetComponent<CheckBoolsCrouchTeleporting>().setIsCrouching(false);

    }

    // Update is called once per frame
    void Update () {
        if (XRDevice.model == "Oculus Rift CV1")//------------------------Oculus------------------------------------------------------------
        {
            if ((OVRInput.GetDown(OVRInput.RawButton.B) || OVRInput.GetDown(OVRInput.RawButton.Y)) && !Grandparent.GetComponent<CheckBoolsCrouchTeleporting>().getIsCrouching()) // When the button is pressed down
            {
                Grandparent.GetComponent<CheckBoolsCrouchTeleporting>().setIsCrouching(true);
                //   SetCrouch(true);
                Debug.Log("Crouch");
            }
            if ((OVRInput.GetUp(OVRInput.RawButton.B) || OVRInput.GetUp(OVRInput.RawButton.Y)) && Grandparent.GetComponent<CheckBoolsCrouchTeleporting>().getIsCrouching())// When the button is released
            {
                Grandparent.GetComponent<CheckBoolsCrouchTeleporting>().setIsCrouching(false);
                // SetCrouch(false);
                Debug.Log("StopCrouch");

            }
        } 
        else if(XRDevice.model == "Vive MV")//----------------------------------Vive------------------------------------------------------------
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
        }//-------------------------------------------------------------------------------------------------------------------------------------

    }

    void SetCrouch(bool b)
    {
        int LayerMask = ~(1 << 8);  //Set layer to ignore layer 8 (=player layer)
        if (XRDevice.model == "Oculus Rift CV1")//-------------------------------------oculus------------------------------------------------------
        {
            if (b)
                transform.position -= offset;
            else
                transform.position += offset;
        }
        if (XRDevice.model == "Vive MV")//--------------------------------------------------Vive----------------------------------------------------------
        {
            if (b && !transform.parent.parent.GetComponent<CheckBoolsCrouchTeleporting>().getIsCrouching())
            {
                // Raycast down to check how low the player will be set for crouching
                RaycastHit hit;
                

                //if (Physics.Raycast(transform.parent.Find("Camera (eye)").transform.position, Vector3.down,out hit)) // old if sentence
                if (Physics.Raycast(transform.parent.Find("Camera (eye)").transform.position, Vector3.down, out hit, Mathf.Infinity, LayerMask))

                    {
                        Debug.Log("in physics.raycast hit" + hit.transform.name);
                    if (hit.transform.tag == "Ground")
                    {
                        offset = transform.parent.Find("Camera (eye)").transform.position - hit.point;
                        offset.y *= 0.45f;
                        Debug.Log("change offset, new offset is" + offset);

                    }
                }
                transform.parent.parent.position -= offset;
                //Debug.Log("alas"+offset+ hit.transform.tag);
                

            }
            else if (!b && transform.parent.parent.GetComponent<CheckBoolsCrouchTeleporting>().getIsCrouching())
            {
                transform.parent.parent.position += offset;

            }
        }//----------------------------------------------------------------------------------------------------------------------------------------------------
    }
}
