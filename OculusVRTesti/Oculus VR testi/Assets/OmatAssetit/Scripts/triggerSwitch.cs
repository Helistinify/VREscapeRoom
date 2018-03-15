using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class triggerSwitch : MonoBehaviour {

    private void OnTriggerEnter(Collider other)
    {
        if(other.transform.CompareTag("Action"))
        {
            Debug.Log("JEEE!!!");
        }
    }
}
