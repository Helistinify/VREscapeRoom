using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace VRGame
{
    public class ConnectToRoom : Photon.PunBehaviour
    {

        public Camera camera;
        public Vector3 spawnPoint;
        public GameObject playerPrefab;

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
            CreatePlayerObject();
        }

        public override void OnDisconnectedFromPhoton()
        {
            Debug.Log("OnDisconnectedFromPhoton!");
        }

        void CreatePlayerObject()
        {
            spawnPoint = new Vector3(3.5f, 3f, 0f);

            GameObject newPlayerObject = PhotonNetwork.Instantiate(this.playerPrefab.name, spawnPoint, Quaternion.identity, 0);
            
            camera.transform.SetParent(newPlayerObject.transform);
        }

        void Awake()
        {
            PhotonNetwork.autoJoinLobby = false;

            PhotonNetwork.automaticallySyncScene = true;
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