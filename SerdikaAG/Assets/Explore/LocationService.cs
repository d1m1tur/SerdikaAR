using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class LocationService : MonoBehaviour
{
    private float longitude = 23.250986f;
    private float latitude = 42.712204f;
    private bool setOriginalValues = false;

    public float getLongitude()
    {
        return longitude;
    }

    public float getLatitude()
    {
        return latitude;
    }

    public bool getSetOriginalValues()
    {
        return setOriginalValues;
    }

    void Start()
    {
        StartCoroutine("GetLocation");
    }

    IEnumerator GetLocation()
    {
        //while true so this function keeps running once started.
        while (true)
        {
            // check if user has location service enabled
            if (!Input.location.isEnabledByUser)
            {
                setOriginalValues = true;
                yield break;
            }

            // Start service before querying location
            if (Input.location.status != LocationServiceStatus.Running)
            {
                Input.location.Start(1f, .1f);
            }

            // Wait until service initializes
            int maxWait = 20;
            while (Input.location.status == LocationServiceStatus.Initializing && maxWait > 0)
            {
                yield return new WaitForSeconds(1f);
                maxWait--;
            }

            Input.location.Stop();

            // Service didn't initialize in 20 seconds
            if (maxWait < 1)
            {
                yield break;
            }

            // Connection has failed
            if (Input.location.status == LocationServiceStatus.Failed)
            {
                yield break;
            }
            else
            {
                //if they are already set once
                if (setOriginalValues)
                {
                    // wait a minute to set new 
                    yield return new WaitForSeconds(60f);
                } 
                else
                {
                    setOriginalValues = true;
                }

                longitude = Input.location.lastData.longitude;
                latitude = Input.location.lastData.latitude;
            }
        }
    }

    public float CalculateDistance(float lat1, float lon1, float lat2, float lon2)
    {
        var R = 6378.137; // Radius of earth in KM
        var dLat = lat2 * Mathf.PI / 180 - lat1 * Mathf.PI / 180;
        var dLon = lon2 * Mathf.PI / 180 - lon1 * Mathf.PI / 180;
        float a = Mathf.Sin(dLat / 2) * Mathf.Sin(dLat / 2) +
            Mathf.Cos(lat1 * Mathf.PI / 180) * Mathf.Cos(lat2 * Mathf.PI / 180) *
            Mathf.Sin(dLon / 2) * Mathf.Sin(dLon / 2);
        var c = 2 * Mathf.Atan2(Mathf.Sqrt(a), Mathf.Sqrt(1 - a));
        double distance = R * c;
        distance = distance * 1000f; // meters
        float distanceFloat = (float)distance;

        return distanceFloat;
    }

    void Update()
    {
        
    }
}