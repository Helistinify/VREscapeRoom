using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class OcculusTeleGrab : MonoBehaviour {

    //crouch
    private CheckBoolsCrouchTeleporting checkScript;
    public GameObject Eye;
    public GameObject camera;
    Vector3 offset;
    
    //teleport
    public GameObject LeftController;   // Use X button
    public GameObject RightController; //Use A button

    GameObject RHP_Object; //Raycast Hit Point Object
    Renderer RHP_Render; //Raycast Hit Point Renderer
    GameManager GMscript; // ?
    bool touchpadDown; //used to check if touchpad button is clicked
    bool ShowIndicator;
    Vector3 TPosition;
    [SerializeField]
    float raycastHeight;

    // Use this for initialization
    void Start () // Awake?
    {
        checkScript = GetComponent<CheckBoolsCrouchTeleporting>();
        checkScript.setIsCrouching(false);

        //Teleport-------------------------->
        //Take reference to controller
        RHP_Object = transform.Find("TeleporterIndicator(Clone)").gameObject;
        RHP_Render = RHP_Object.GetComponent<Renderer>();

        checkScript.setCanTeleport(false);

        GMscript = GameObject.Find("GameManager").GetComponent<GameManager>();
        raycastHeight = 1.1f;
        touchpadDown = false;
        ShowIndicator = false;
        RHP_Render.enabled = false;
    }

    // Update is called once per frame
    void Update()
    {
        
        if ((OVRInput.GetDown(OVRInput.RawButton.B) || OVRInput.GetDown(OVRInput.RawButton.Y)) && !checkScript.getIsCrouching()) // When the button is pressed down->crouch
            {
            Debug.Log("Crouch");
            SetCrouch(true);
            checkScript.setIsCrouching(true);
            }
        if ((OVRInput.GetUp(OVRInput.RawButton.B) && checkScript.getIsCrouching() && !OVRInput.Get(OVRInput.RawButton.Y))
            || (OVRInput.GetUp(OVRInput.RawButton.Y) && checkScript.getIsCrouching() && !OVRInput.Get(OVRInput.RawButton.B)))// When the button is released -> get up
            {
            Debug.Log("StopCrouch");
            SetCrouch(false);
            checkScript.setIsCrouching(false);

            }
        if (OVRInput.GetDown(OVRInput.RawButton.A)) // When the button is pressed down // (Input.GetButtonDown("Submit"))
            {
                ShowIndicator = true;
            }
        if (OVRInput.GetUp(OVRInput.RawButton.A)) // When the button is released
            {
             ShowIndicator = false;
             // Move player object to wherever the raycast hit point is at
             if (checkScript.getCanTeleport())
              {
                 Debug.Log(TPosition);
                 transform.position = TPosition+ new Vector3(0.0f, 10.0f, 0.0f);
                 checkScript.setCanTeleport(false);
                 RendererEnabled(checkScript.getCanTeleport());
              }
            }

    }
    void FixedUpdate()
    {
        if (ShowIndicator)
        {
            // Raycast forward from Controller
            RaycastHit hit;
            if (Physics.Raycast(RightController.transform.position, transform.forward, out hit)) // Also check if raycast hit not touching an object with tag "Wall"
            {
                // Move an object to the raycast hit point
                if (hit.transform.tag == "Ground" && !checkScript.getIsCrouching())//if target is ground and is not crounching can teleport
                {
                    // Raycast from teleportindicator to up to check if there is an obstacle, the player shouldn't be able to teleport under narrow spaces
                    RaycastHit fromTPositionUp;
                    if (Physics.Raycast(hit.point, Vector3.up, out fromTPositionUp, raycastHeight))
                    {
                        if (hit.transform.tag == "Obstacle")
                        {
                            checkScript.setCanTeleport(false);
                        }
                    }
                    else
                    {
                        RHP_Object.transform.position = hit.point;
                        TPosition = hit.point;

                        checkScript.setCanTeleport(true);
                    }
                }
                else
                {
                    checkScript.setCanTeleport(false);
                }
                RendererEnabled(checkScript.getCanTeleport());
            }
        }
    }

    void SetCrouch(bool b)
    {
        int LayerMask = ~(1 << 8);  //Set layer to ignore layer 8 (=player layer)
            if (b)
            {
               //Raycast down to check how low the player will be set for crouching
               RaycastHit hit;

               if (Physics.Raycast(Eye.transform.position, Vector3.down, out hit, Mathf.Infinity, LayerMask))
                 {
                  //Debug.Log("in physics.raycast hit" + hit.transform.name);
                  if (hit.transform.tag == "Ground")
                     {
                       offset = Eye.transform.position - hit.point;
                       offset.y *= 0.45f;
                      // Debug.Log("change offset, new offset is" + offset);
                      }
                  }
               camera.transform.position -= offset; // go crouch
               //Debug.Log("alas" + offset + hit.transform.tag);
            }

            else
            {
                camera.transform.position += offset; //get up
            }
    }
    void RendererEnabled(bool b)
    {
        RHP_Render.enabled = b;
    }
}
