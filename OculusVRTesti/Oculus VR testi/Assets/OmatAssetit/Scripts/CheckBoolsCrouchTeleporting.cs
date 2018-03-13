using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CheckBoolsCrouchTeleporting : MonoBehaviour {

    bool isCrouching;
    bool isTeleporting;
    [SerializeField]
    GameObject whichController; // used to check if controller is trying to teleport or crouch

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

    public bool getCanTeleport()
    {
        return isTeleporting;
    }

    public GameObject getWhichController()
    {
        return whichController;
    }

}
