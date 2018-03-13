using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Interactable_item : MonoBehaviour {

    //This script should be added to all items the player can interact with
    //The interactable item should also have a rigidbody for this to work.

    public Rigidbody rb;//oh please...
    public bool currentlyInteracting;//this should be selfexplanatory
    private HTC_ViveGrab grabCrab; //this is the grabbing script
    private Transform interactionPoint;//why even wonder what this is

    private Vector3 posDelta;
    private Vector3 axis;

    private Quaternion rotationDelta;

    private float angle;
    [SerializeField]
    private float rotationFactor = 400f;
    [SerializeField]
    private float velocityFactor = 200f;

	// Use this for initialization
	void Start () {
        rb = GetComponent<Rigidbody>();
        interactionPoint = new GameObject().transform;
        velocityFactor /= rb.mass;//the more heavier the object, the slower it's able to move
    }
	
	// Update is called once per frame
	void Update ()
    {
        if (grabCrab && currentlyInteracting)
        {
            posDelta = grabCrab.transform.position - interactionPoint.position;
            this.rb.velocity = posDelta * velocityFactor * Time.fixedDeltaTime;
            rotationDelta = grabCrab.transform.rotation * Quaternion.Inverse(interactionPoint.rotation);
            rotationDelta.ToAngleAxis(out angle, out axis);

            if (angle > 180)
            {
                angle -= 360;
            }

            this.rb.angularVelocity = (Time.fixedDeltaTime *angle* axis)*rotationFactor;//Heavier object is harder to rotate
        }
	}

    public void beginInteraction(HTC_ViveGrab grab)
    {
        grabCrab = grab; //set grabCrab to grab

        interactionPoint.position = grab.transform.position; //position of grabbing
        interactionPoint.rotation = grab.transform.rotation; //rotation of grabbing
        interactionPoint.SetParent(transform, true); //at the positiion of grabbing sets an empty gameobject to child of hand and then makes the item a child of the empty gameobject

        currentlyInteracting = true;
    }

    public void endInteraction(HTC_ViveGrab grab)
    {
        if(grabCrab == grab)
        {
            grabCrab = null;
            currentlyInteracting = false;
        }

    }

    public bool isInteracting()
    {
        return currentlyInteracting;
    }
}
