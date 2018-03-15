using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovement : MonoBehaviour {

    public float thrust;
    Rigidbody rb;

    private void Start()
    {
        rb = GetComponent<Rigidbody>();
    }

    // Update is called once per frame
    void FixedUpdate () {
        if (GetComponent<PhotonView>().isMine)
        {
            if (Input.GetKey(KeyCode.A))
            {
                rb.AddForce(new Vector3(-1f, 0f, 0f) * thrust);
            }
            if (Input.GetKey(KeyCode.W))
            {
                rb.AddForce(new Vector3(0f, 0f, 1f) * thrust);
            }
            if (Input.GetKey(KeyCode.S))
            {
                rb.AddForce(new Vector3(0f, 0f, -1f) * thrust);
            }
            if (Input.GetKey(KeyCode.D))
            {
                rb.AddForce(new Vector3(1f, 0f, 0f) * thrust);
            }
            if (Input.GetKeyDown(KeyCode.Space))
            {
                GameObject.FindGameObjectWithTag("networkManager").GetComponent<CreateObjects>().callCreateObj();
            }
        }
	}
}
