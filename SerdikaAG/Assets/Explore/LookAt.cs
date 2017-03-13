using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LookAt : MonoBehaviour {

    public Transform cam;

    public void setCam(Transform _cam)
    {
        cam = _cam;
    }

    // Use this for initialization
    void Start () {
		
	}

    // Update is called once per frame
    void LookAtCam()
    {
        float a = transform.position.x - cam.position.x;
        float b = transform.position.y - cam.position.y;
        float c = Mathf.Sqrt(Mathf.Pow(a, 2) + Mathf.Pow(b, 2));

        if (c == 0) return; //cant divide by zero

        float cos = b / c;
        float angle = Mathf.Acos(cos) * Mathf.Rad2Deg;

        if (a < 0) { angle *= -1; }

        transform.eulerAngles = new Vector3(transform.eulerAngles.x, 180, angle);
    }

    void Update()
    {
        LookAtCam();
    }
}
