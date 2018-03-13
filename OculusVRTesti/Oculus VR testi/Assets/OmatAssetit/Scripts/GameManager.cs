using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.XR; // Change VR to XR when updating unity past 2018.1b !!!

public class GameManager : MonoBehaviour {

    public GameObject VivePlayer;
    public GameObject OculusPlayer;
    List<GameObject> VRDToSpawn = new List<GameObject>();
    public GameObject TeleporterIndicator;
    public List<string> VRModel = new List<string>();
    private int players;

    private void Awake() // Check before the game is loaded for what device the player is using.
    {
        players = 0;
    }

    public void saveModelName(string deviceName)
    {
        VRModel.Add(deviceName);

        if (VRModel[players] == "Oculus Rift CV1") // CHANGE VRDevice to XRDevice when updating !!!
        {
            Debug.Log("Oculus connected");
            VRDToSpawn.Add(OculusPlayer);
            players++;
        }
        else if (VRModel[players] == "Vive MV") // CHANGE VRDevice to XRDevice when updating !!!
        {
            Debug.Log("Vive connected");
            VRDToSpawn.Add(VivePlayer);
            players++;
        }
        else
        {
            Debug.Log("Unsupported / no VR device connected");
        }
    }

    // Use this for initialization
    void Start () {
        //Instantiate(TeleporterIndicator, new Vector3(0, 0, 0), Quaternion.identity); //
        //Instantiate(VRDToSpawn, new Vector3(0, 2.3f, 0), Quaternion.identity); //Spawn the correct player prefab

    }

}
