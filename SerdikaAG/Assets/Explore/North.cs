using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class North : MonoBehaviour {

    public Transform cam;

	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
        transform.position = new Vector3(cam.position.x, cam.position.y + 15, cam.position.z);
	}
}
