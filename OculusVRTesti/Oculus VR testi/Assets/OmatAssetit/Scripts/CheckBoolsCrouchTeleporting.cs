using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CheckBoolsCrouchTeleporting : MonoBehaviour {

    public bool isCrouching;
    public bool isTeleporting;
    [SerializeField]
    GameObject whichController; // used to check if controller is trying to teleport or crouch
    private void Awake()
    {
        isCrouching = false;
        isTeleporting = false;
    }

    //setters
    public void setIsCrouching(bool crouching)
    {
        isCrouching = crouching;
    }

    public void setCanTeleport(bool teleporting)
    {
        isTeleporting = teleporting;
    }

    public void setWhichController(GameObject controller)
    {
        whichController = controller;
    }


    //getters
    public bool getIsCrouching()
    {
        return isCrouching;
    }

    public bool getCanTeleport() //get isTeleporting?
    {
        return isTeleporting;
    }

    public GameObject getWhichController()
    {
        return whichController;
    }

}
