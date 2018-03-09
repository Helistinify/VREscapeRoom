using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Collections.Generic;
using UnityEngine.XR;

public class HTC_ViveGrab : MonoBehaviour {

    //Needs Interactable_item.cs to be in the item to work properly

    GameManager GMscript;
    private SteamVR_Controller.Device device;
    public SteamVR_TrackedObject controller;

    private GameObject pickup;

    //this contains nearby items
    HashSet<Interactable_item> objectHoveringOver = new HashSet<Interactable_item>();

    private Interactable_item closestItem;
    private Interactable_item interactingItem;

    // Use this for initialization
    void Awake()
    {
        GMscript = GameObject.FindGameObjectWithTag("GameManager").GetComponent<GameManager>();
        device = SteamVR_Controller.Input((int)controller.index);
    }

    // Update is called once per frame
    void Update()
    {
        if (XRDevice.model == "Vive MV")
        {
            if (device.GetPressDown(Valve.VR.EVRButtonId.k_EButton_SteamVR_Trigger))
            {
                float minDistance = float.MaxValue;
                float distance;

                foreach(Interactable_item item in objectHoveringOver)
                {
                    distance = (item.transform.position - transform.position).sqrMagnitude;

                    if (distance < minDistance)
                    {
                        minDistance = distance;
                        closestItem = item;
                    }
                }
                interactingItem = closestItem;

                if (interactingItem)
                {
                    if (interactingItem.isInteracting()) //check for if interaction is already going on
                    {
                        interactingItem.endInteraction(this);
                    }
                }

                interactingItem.beginInteraction(this);
            }
            else if (device.GetPressUp(Valve.VR.EVRButtonId.k_EButton_SteamVR_Trigger) && !interactingItem)
            {
                interactingItem.endInteraction(this);
            }
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        //ontriggerenter add the collided item to the list of nearby items
        Interactable_item collidedItem = other.GetComponent<Interactable_item>();
        if (collidedItem)
        {
            objectHoveringOver.Add(collidedItem);
        }
    }

    private void OnTriggerExit(Collider other)
    {
        //ontriggerexit remove collided item from list of nearby items
        Interactable_item collidedItem = other.GetComponent<Interactable_item>();
        if (collidedItem)
        {
            objectHoveringOver.Remove(collidedItem);
        }
    }

}
