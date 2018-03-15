using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class playerColliderFollowsPlayer : MonoBehaviour {

    Vector3 colliderPos;
    Vector3 temp;
    public float underCam = .8f;
    public float ColliderOffset;

	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
        colliderPos = transform.parent.GetChild(0).Find("Camera (eye)").transform.position;
        temp = transform.parent.GetChild(0).Find("Camera (eye)").forward;
        temp.y = 0;
        temp=temp.normalized;
        colliderPos = temp = new Vector3(colliderPos.x + temp.x*ColliderOffset, colliderPos.y - underCam, colliderPos.z + temp.z * ColliderOffset);
       // colliderPos.y -= underCam;


        transform.position = colliderPos;
    }
}
