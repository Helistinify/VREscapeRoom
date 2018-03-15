using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class Micketest : Photon.MonoBehaviour
{

    public Material blackHair;
    public Material redHair;

    private Renderer Rend;

    private void Start()
    {
        Rend = GetComponent<Renderer>();
    }
    void Update()
    {

        if (photonView.isMine)
        {
            if (Input.GetKeyDown(KeyCode.E))
            {
                photonView.RPC("ChangeHairColour", PhotonTargets.All, 1);
            }

            if (Input.GetKeyDown(KeyCode.P))
            {
                photonView.RPC("ChangeHairColour", PhotonTargets.All, 2);
            }
        }//if (photonView.isMine)

    }


    //&#91;RPC&#93;
    [PunRPC]
    void ChangeHairColour(int hairNum)
    {

        if (hairNum == 1)
        {
            Rend.material = blackHair;
        }
        else if (hairNum == 2)
        {
            Rend.material= redHair;

        }


    }

}//ChangeTexture