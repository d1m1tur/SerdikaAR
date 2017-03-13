using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class CreateLabels : MonoBehaviour {

    public GameObject labelObject;
    public Transform cam;
    public LocationService user;

    public string[] labels;

    IEnumerator Start()
    {
        WWW labelsData = new WWW("http://87.116.75.165:8000/SerdikaAG/getLabels.php");
        yield return labelsData;
        string labelsDataString = labelsData.text;
        labels = labelsDataString.Split(';');

        for(int i = 0; i < labels.Length - 1; i++)
        {
            string label = labels[i];

            print(GetDataValue(label, "ID:"));
            print(GetDataValue(label, "Latitude:"));
            print(GetDataValue(label, "Longitude:"));
            print(GetDataValue(label, "Image:"));

            float latitude = float.Parse(GetDataValue(label, "Latitude:"));
            float longitude = float.Parse(GetDataValue(label, "Longitude:"));

            string image_url = GetDataValue(label, "Image:");
            WWW image = new WWW(image_url);
            yield return image;

            var newInstance = Instantiate(labelObject, new Vector3(0, 0, 0), Quaternion.identity);
            newInstance.GetComponent<LocationWithoutGps>().setLatitude(latitude);
            newInstance.GetComponent<LocationWithoutGps>().setLongitude(longitude);
            newInstance.GetComponent<LocationWithoutGps>().setUser(user);
            newInstance.GetComponent<LocationWithoutGps>().setCam(cam);
            newInstance.GetComponent<LookAt>().setCam(cam);
            newInstance.GetComponent<Renderer>().material.mainTexture = image.texture;
        }
    }

    string GetDataValue(string data, string index)
    {
        string value = data.Substring(data.IndexOf(index) + index.Length);
        if (value.Contains("|")) value = value.Remove(value.IndexOf("|"));
        return value;
    }

    // Update is called once per frame
    void Update () {
		
	}
}
