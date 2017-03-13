using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GyroScript : MonoBehaviour {


    // Use this for initialization
    void Start ()
    {
        if (SystemInfo.supportsGyroscope)
        {
            Input.gyro.enabled = true;
        } 
    }

    // Update is called once per frame
    void Update ()
    {
        if (Input.gyro.enabled)
        {
            Quaternion cameraRotation = new Quaternion(Input.gyro.attitude.x, Input.gyro.attitude.y, -Input.gyro.attitude.z, -Input.gyro.attitude.w);
            transform.localRotation = cameraRotation;
        }
    }
}
