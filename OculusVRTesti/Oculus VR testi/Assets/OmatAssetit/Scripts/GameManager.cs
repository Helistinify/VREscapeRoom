using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.XR; // Change VR to XR when updating unity past 2018.1b !!!

public class GameManager : MonoBehaviour {

    public GameObject VivePlayer;
    public GameObject OculusPlayer;
    public GameObject VRDToSpawn;
    public GameObject TeleporterIndicator;
    public string VRModel;

    private void Awake() // Check before the game is loaded for what device the player is using.
    {
        VRModel = XRDevice.model;

        if (VRModel == "Oculus Rift CV1") // CHANGE VRDevice to XRDevice when updating !!!
        {
            Debug.Log("Oculus connected");
            VRDToSpawn = OculusPlayer;
        }
        else if (VRModel == "Vive MV") // CHANGE VRDevice to XRDevice when updating !!!
        {
            Debug.Log("Vive connected");
            VRDToSpawn = VivePlayer;
        }
        else
        {
            Debug.Log("Unsupported / no VR device connected");
        }
    }


    // Use this for initialization
    void Start () {
        Instantiate(TeleporterIndicator, new Vector3(0, 0, 0), Quaternion.identity); //
        Instantiate(VRDToSpawn, new Vector3(0, 2.3f, 0), Quaternion.identity); //Spawn the correct player prefab

    }
	
	// Update is called once per frame
	void Update () {
		
	}
}
