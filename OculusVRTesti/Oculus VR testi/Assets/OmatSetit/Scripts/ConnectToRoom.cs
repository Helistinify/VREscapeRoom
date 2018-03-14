using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.XR;

namespace VRGame
{
    public class ConnectToRoom : Photon.PunBehaviour
    {

        //public Camera camera;
        public Vector3 spawnPoint;
        //public GameObject playerPrefab;

        public GameObject VivePlayer;
        public GameObject OculusPlayer;
        List<GameObject> VRDToSpawn = new List<GameObject>();
        public GameObject TeleporterIndicator;
        List<string> VRModel = new List<string>();
        private int numOfPlayers;

        string _gameVersion = "1";
        public byte maxPlayersPerRoom = 2;

        public override void OnConnectedToMaster()
        {
            Debug.Log("OnConnectedToMaster!");
            PhotonNetwork.JoinRandomRoom();
        }

        public override void OnPhotonRandomJoinFailed(object[] codeAndMsg)
        {
            Debug.Log("OnPhotonRandomJoinFailed!");
            PhotonNetwork.CreateRoom(null, new RoomOptions() { MaxPlayers = maxPlayersPerRoom }, null);
        }

        public override void OnJoinedRoom()
        {
            Debug.Log("OnJoinedRoom!");
            saveModelNameNet();
            CreatePlayerObject();
        }

        public override void OnDisconnectedFromPhoton()
        {
            Debug.Log("OnDisconnectedFromPhoton!");
        }

        void CreatePlayerObject()
        {
            if (XRDevice.model == "Oculus Rift CV1")
            {
                spawnPoint = new Vector3(0f, 3f, 0f);
            }
            else if (XRDevice.model == "Vive MV")
            {
                spawnPoint = new Vector3(0f, 1.8f, 0f);
            }
                

            GameObject newPlayerObject = PhotonNetwork.Instantiate(this.VRDToSpawn[PhotonNetwork.player.ID-1].name, spawnPoint, Quaternion.identity, 0); // this.VRDToSpawn.name old code. PhotonNetwork.player.ID-1 is the current player's ID
            GameObject teleportInd = PhotonNetwork.Instantiate("TeleporterIndicator", spawnPoint, Quaternion.identity, 0);

            teleportInd.transform.SetParent(newPlayerObject.transform);
            
            //camera.transform.SetParent(newPlayerObject.transform);
        }

        void Awake()
        {
            PhotonNetwork.autoJoinLobby = false;

            PhotonNetwork.automaticallySyncScene = true;
        }

        public void saveModelNameNet()
        {
            VRModel.Add(XRDevice.model);

            if (VRModel[PhotonNetwork.player.ID - 1] == "Oculus Rift CV1") // CHANGE VRDevice to XRDevice when updating !!!
            {
                Debug.Log("Oculus connected");
                VRDToSpawn.Add(OculusPlayer);
            }
            else if (VRModel[PhotonNetwork.player.ID - 1] == "Vive MV") // CHANGE VRDevice to XRDevice when updating !!!
            {
                Debug.Log("Vive connected");
                VRDToSpawn.Add(VivePlayer);
            }
            else
            {
                Debug.Log("Unsupported / no VR device connected");
            }
        }

        void Start()
        {
            Connect();
        }


        public void Connect()
        {
            if (PhotonNetwork.connected)
            {
                PhotonNetwork.JoinRandomRoom();
            }
            else
            {
                PhotonNetwork.ConnectUsingSettings(_gameVersion);
            }
        }
    }
}