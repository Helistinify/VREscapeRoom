using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class playerColliderFollowsPlayer : MonoBehaviour {

    Vector3 colliderPos;
    public float underCam = .8f;

	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
        colliderPos = transform.parent.GetChild(0).Find("Camera (eye)").transform.position;
        colliderPos.y -= underCam;
        transform.position = colliderPos;
    }
}
