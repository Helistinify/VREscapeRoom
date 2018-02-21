using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Crouch : MonoBehaviour {
    
    Vector3 offset;
	// Use this for initialization
	void Start () {
        offset = new Vector3(0f, 1.2f, 0f);
	}
	
	// Update is called once per frame
	void Update () {
        if (Input.GetButtonDown("Cancel")) // When the button is pressed down
        {
            SetCrouch(true);
        }
        if (Input.GetButtonUp("Cancel"))// When the button is released
        {
            SetCrouch(false);
        }
    }

    void SetCrouch(bool b)
    {
        if (b)
            transform.position -= offset;
        else
            transform.position += offset;
    }
}
