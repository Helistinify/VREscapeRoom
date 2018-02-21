using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Teleporting : MonoBehaviour {

    // Use this for initialization
    public GameObject Controller;
    public GameObject RHP_Object;
    Renderer RHP_Render;
    bool ShowIndicator;
    bool CanTeleport;
    Vector3 TPosition;

	void Start () {
        //Take reference to controller
        Controller = gameObject;
        RHP_Render = RHP_Object.GetComponent<Renderer>();
        CanTeleport = false;
    }

    void Update()
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

    void FixedUpdate () {
		if (ShowIndicator)
        {
            // Raycast forward from Controller
            RaycastHit hit;
            if (Physics.Raycast(transform.position, transform.forward, out hit))
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
