using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LocationWithCordinates : MonoBehaviour {

    private float longitude = 23.333063f;
    private float latitude = 42.695709f;
    public LocationService user;
    public Transform cam;

    public void setCam(Transform _cam)
    {
        cam = _cam;
    }

    public void setUser(LocationService _user)
    {
        user = _user;
    }

    public void setLongitude(float _longitude)
    {
        longitude = _longitude;
    }

    public void setLatitude(float _latitude)
    {
        latitude = _latitude;
    }

    // Use this for initialization
    void Start ()
    {

    }

    void SetLocation()
    {
        //if the gps of the user has already set the initial values
        if (user.getSetOriginalValues())
        {
            double uLat = Math.Round(user.getLatitude(), 5);
            double uLong = Math.Round(user.getLongitude(), 5);
            double myLat = Math.Round(latitude, 5);
            double myLong = Math.Round(longitude, 5);

            double side_a = uLong - myLong;
            //23.25064 - 23.33312 = -0.08250;
            double side_b = uLat - myLat;
            //42.71125 - 42.69575 = 0.01550;
            double side_c = Math.Sqrt(Math.Pow(side_a, 2) + Math.Pow(side_b, 2));

            double sin = side_a / side_c;

            double fixN = 1; // used to fix the negative value
            if(side_b > 0) { fixN = -1; }

            side_c = 10; //new Distance 
            side_a = side_c * sin;
            side_a *= -1;
            side_b = Math.Sqrt(Math.Pow(side_c, 2) - Math.Pow(side_a, 2));

            transform.position = new Vector3((float)(cam.position.x + side_a), (float)(cam.position.y + (side_b * fixN)), cam.position.z);
            float distance = user.CalculateDistance((float)uLat, (float)uLong, (float)myLat, (float)myLong);
            distance = Mathf.Floor(distance);
            GetComponentInChildren<TextMesh>().text = "Distance: " + distance + " meters";
        }
    }

    void Update()
    {
        SetLocation();
    }
}
