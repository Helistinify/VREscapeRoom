using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class PlayerChangeColor : Photon.PunBehaviour
{

    void Start()
    {
        GetComponent<PhotonView>().RPC("changeMaterial", PhotonTargets.AllBuffered, PhotonNetwork.player.ID);
    }

    [PunRPC]
    public void changeMaterial(int id)
    {
        if (id == 1)
        {
            GetComponent<Renderer>().material.color = Color.red;

        }
        else
        {
            GetComponent<Renderer>().material.color = Color.blue;
        }
    }
}
