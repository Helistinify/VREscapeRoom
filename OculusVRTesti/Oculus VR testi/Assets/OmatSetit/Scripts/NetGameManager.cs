using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class NetGameManager : MonoBehaviour {

    public Camera camera;
    public Vector3 spawnPoint;
    public GameObject playerPrefab;

    private void Start()
    {
        if(playerPrefab == null)
        {
            Debug.LogError("<Color=Red><a>Missing</a></Color> playerPrefab Reference.");
        }
        else
        {
            CreatePlayerObject();
        }
    }

    void CreatePlayerObject()
    {
        spawnPoint = new Vector3(3.5f, 3f, 0f);

        GameObject newPlayerObject = PhotonNetwork.Instantiate(this.playerPrefab.name, spawnPoint, Quaternion.identity, 0);
        camera.transform.SetParent(newPlayerObject.transform);
    }

    public void OnLeftRoom()
    {

    }
}
