using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class OcculusDebug : MonoBehaviour {


	void Update ()
    {
        if(OVRInput.GetDown(OVRInput.RawButton.A))
        { Debug.Log("Pressed A"); }
        if (OVRInput.GetDown(OVRInput.RawButton.B))
        { Debug.Log("Pressed B"); }
        if (OVRInput.GetDown(OVRInput.RawButton.X))
        { Debug.Log("Pressed X"); }
        if (OVRInput.GetDown(OVRInput.RawButton.Y))
        { Debug.Log("Pressed Y"); }
    }
}
