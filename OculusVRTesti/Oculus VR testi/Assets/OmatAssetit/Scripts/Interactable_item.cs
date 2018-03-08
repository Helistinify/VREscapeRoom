using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Interactable_item : MonoBehaviour {

    //This script should be added to all items the player can interact with

    public Rigidbody rb;
    public bool currentlyInteracting;
    private HTC_ViveGrab grabCrab; //this is the grabbing script
    private Transform interactionPoint;

	// Use this for initialization
	void Start () {
        rb = GetComponent<Rigidbody>();
        interactionPoint = new GameObject().transform;
	}
	
	// Update is called once per frame
	void Update ()
    {

	}

    void beginInteraction(HTC_ViveGrab grab)
    {
        grabCrab = grab; //set grabCrab to grab
        interactionPoint.position = grab.transform.position; //position of grabbing
        interactionPoint.rotation = grab.transform.rotation; //rotation of grabbing
        interactionPoint.SetParent(transform, true); //at the positiion of grabbing sets an empty gameobject to child of hand and then makes the item a child of the empty gameobject

        currentlyInteracting = true;
    }

    void endInteraction(HTC_ViveGrab grab)
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
