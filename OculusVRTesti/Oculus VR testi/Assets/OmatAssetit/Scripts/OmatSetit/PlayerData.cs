using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.XR;

public class PlayerData : MonoBehaviour {

    private string modelName;
    private void Awake()
    {
        GameObject.FindGameObjectWithTag("GameManager").GetComponent<GameManager>().saveModelName(XRDevice.model);
        modelName = XRDevice.model;
    }

    public string getModelName()
    {
        return modelName;
    }
}
