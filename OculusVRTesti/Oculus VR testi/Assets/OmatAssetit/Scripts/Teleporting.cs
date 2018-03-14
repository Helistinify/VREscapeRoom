using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.XR;

public class Teleporting : MonoBehaviour
{

    public GameObject Controller;
    GameObject RHP_Object_Grandparent;
    GameObject RHP_Object; //Raycast Hit Point Object
    Renderer RHP_Render; //Raycast Hit Point Renderer
    bool ShowIndicator;
    Vector3 TPosition;
    GameManager GMscript;
    private SteamVR_Controller.Device device;
    public SteamVR_TrackedObject controller;
    bool touchpadDown; //used to check if touchpad button is clicked
    Vector2 touchpadFingerPos;
    [SerializeField]
    float raycastHeight;

    void Start()
    {
        //Take reference to controller
        Controller = gameObject;
        if (XRDevice.model == "Oculus Rift CV1")
        {
            RHP_Object_Grandparent = gameObject.transform.parent.parent.parent.gameObject;
        } else if(XRDevice.model == "Vive MV")
        {
            RHP_Object_Grandparent = gameObject.transform.parent.parent.gameObject;
        }
        RHP_Object = RHP_Object_Grandparent.transform.Find("TeleporterIndicator(Clone)").gameObject;
        RHP_Render = RHP_Object.GetComponent<Renderer>();

        transform.parent.parent.GetComponent<CheckBoolsCrouchTeleporting>().setCanTeleport(false);

        GMscript = GameObject.Find("GameManager").GetComponent<GameManager>();
        if (XRDevice.model == "Vive MV")
        {
            controller = GetComponent<SteamVR_TrackedObject>();
            device = SteamVR_Controller.Input((int)controller.index);
        }
        raycastHeight = 1.1f;
        touchpadDown = false;
        ShowIndicator = false;
        RHP_Render.enabled = false;
    }

    void Update()
    {
            if (XRDevice.model == "Oculus Rift CV1")
            {
                if (Input.GetButtonDown("Submit")) // When the button is pressed down
                {
                    ShowIndicator = true;
                }
                if (Input.GetButtonUp("Submit"))// When the button is released
                {
                    ShowIndicator = false;
                    // Move player object to wherever the raycast hit point is at
                    if (transform.parent.parent.GetComponent<CheckBoolsCrouchTeleporting>().getCanTeleport())
                    {
                        transform.parent.parent.parent.position = TPosition;
                        transform.parent.parent.GetComponent<CheckBoolsCrouchTeleporting>().setCanTeleport(false);
                        RendererEnabled(transform.parent.parent.GetComponent<CheckBoolsCrouchTeleporting>().getCanTeleport());
                    }
                }
            }
            else if (XRDevice.model == "Vive MV")
            {
                if (device.GetTouch(Valve.VR.EVRButtonId.k_EButton_SteamVR_Touchpad))
                {
                    touchpadFingerPos = device.GetAxis(Valve.VR.EVRButtonId.k_EButton_SteamVR_Touchpad);
                }
                if (device.GetPressDown(Valve.VR.EVRButtonId.k_EButton_SteamVR_Touchpad) && touchpadFingerPos.y > 0.5f && transform.parent.parent.GetComponent<CheckBoolsCrouchTeleporting>().getWhichController() == null)
                {
                    transform.parent.parent.GetComponent<CheckBoolsCrouchTeleporting>().setWhichController(gameObject);
                    ShowIndicator = true;
                }
                if (device.GetPressUp(Valve.VR.EVRButtonId.k_EButton_SteamVR_Touchpad) && transform.parent.parent.GetComponent<CheckBoolsCrouchTeleporting>().getWhichController() != null 
                && transform.parent.parent.GetComponent<CheckBoolsCrouchTeleporting>().getWhichController().transform.name == transform.name)
                {
                    ShowIndicator = false;
                    // Move player object to wherever the raycast hit point is at
                    if (transform.parent.parent.GetComponent<CheckBoolsCrouchTeleporting>().getCanTeleport())
                    {
                        transform.parent.parent.position = TPosition;
                        transform.parent.parent.GetComponent<CheckBoolsCrouchTeleporting>().setCanTeleport(false);
                        RendererEnabled(transform.parent.parent.GetComponent<CheckBoolsCrouchTeleporting>().getCanTeleport());
                    }
                }
            }
        

    }

    void FixedUpdate()
    {
        if (ShowIndicator)
        {
            // Raycast forward from Controller
            RaycastHit hit;
            if (Physics.Raycast(transform.position, transform.forward, out hit)) // Also check if raycast hit not touching an object with tag "Wall"
            {
                // Move an object to the raycast hit point
                if (hit.transform.tag == "Ground" && !transform.parent.parent.GetComponent<CheckBoolsCrouchTeleporting>().getIsCrouching())//if target is ground and is not crounching can teleport
                {
                    // Raycast from teleportindicator to up to check if there is an obstacle, the player shouldn't be able to teleport under narrow spaces
                    RaycastHit fromTPositionUp;
                    if (Physics.Raycast(hit.point, Vector3.up, out fromTPositionUp, raycastHeight))
                    {
                        if(hit.transform.tag == "Obstacle")
                        {
                            transform.parent.parent.GetComponent<CheckBoolsCrouchTeleporting>().setCanTeleport(false);
                        }
                    }
                    else
                    {
                        RHP_Object.transform.position = hit.point;
                        TPosition = hit.point;
                        transform.parent.parent.GetComponent<CheckBoolsCrouchTeleporting>().setCanTeleport(true);
                    }
                }
                else
                {
                    transform.parent.parent.GetComponent<CheckBoolsCrouchTeleporting>().setCanTeleport(false);
                }
                RendererEnabled(transform.parent.parent.GetComponent<CheckBoolsCrouchTeleporting>().getCanTeleport());
            }
        }
    }

    void RendererEnabled(bool b)
    {
        RHP_Render.enabled = b;
    }

    public SteamVR_Controller.Device GetDevice()
    {
        return device;
    }

}
