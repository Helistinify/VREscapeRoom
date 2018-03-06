using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Teleporting : MonoBehaviour {

    public GameObject Controller;
    GameObject RHP_Object; //Raycast Hit Point Object
    Renderer RHP_Render; //Raycast Hit Point Renderer
    bool ShowIndicator;
    bool CanTeleport;
    Vector3 TPosition;
    GameManager GMscript;

    void Start () {
        //Take reference to controller
        Controller = gameObject;
        RHP_Object = GameObject.Find("TeleporterIndicator(Clone)");
        RHP_Render = RHP_Object.GetComponent<Renderer>();
        CanTeleport = false;
        GMscript = GameObject.Find("GameManager").GetComponent<GameManager>();
    }

    void Update()
    {
        if (GMscript.VRModel == "Oculus Rift CV1")
        {
            if (Input.GetButtonDown("Submit")) // When the button is pressed down
            {
                ShowIndicator = true;
            }
            if (Input.GetButtonUp("Submit"))// When the button is released
            {
                ShowIndicator = false;
                // Move player object to wherever the raycast hit point is at
                if (CanTeleport)
                {
                    transform.parent.parent.parent.position = TPosition;
                    CanTeleport = false;
                    RendererEnabled(CanTeleport);
                }
            }
        }
       /* else if (GMscript.VRModel == "Vive MV")
        {
            if (Input.GetDown(KeyCode.Joystick1Button9)) // When on trackpad touch is above middle and trackpad button is pressed. // TÄMÄ EI TOIMI KORJAA TÄMÄ !!!!!
            {
                ShowIndicator = true;
                Debug.Log("trackpad button is pressed!!!!");
            }
            if (Input.GetButtonUp("Submit"))// When the button is released
            {
                ShowIndicator = false;
                // Move player object to wherever the raycast hit point is at
                if (CanTeleport)
                {
                    transform.parent.parent.parent.position = TPosition;
                    CanTeleport = false;
                    RendererEnabled(CanTeleport);
                }
            }
        }*/

    }

    void FixedUpdate () {
		if (ShowIndicator)
        {
            // Raycast forward from Controller
            RaycastHit hit;
            if (Physics.Raycast(transform.position, transform.forward, out hit)) // Also check if raycast hit not touching an object with tag "Wall"
            {
                // Move an object to the raycast hit point
                if (hit.transform.tag == "Ground")
                {
                    RHP_Object.transform.position = hit.point;
                    TPosition = hit.point;
                    CanTeleport = true;
                }
                else
                { 
                    CanTeleport = false;
                }
                RendererEnabled(CanTeleport);
            }
        }
    }

    void RendererEnabled(bool b)
    {
        RHP_Render.enabled = b; 
    }
}
