using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CreateObjects : Photon.PunBehaviour {

    public GameObject sceneObj;
    public void callCreateObj()
    {
        photonView.RPC("createObj", PhotonTargets.AllBuffered);
    }

    [PunRPC]
	void createObj()
    {
        PhotonNetwork.InstantiateSceneObject(sceneObj.name, new Vector3(0,0,0), Quaternion.identity, 0, null);
    }
}
