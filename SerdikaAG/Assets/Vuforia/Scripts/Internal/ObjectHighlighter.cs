using UnityEngine;
using System.Collections;

public class ObjectHighlighter : MonoBehaviour
{

    bool isSelected = false;

    public GameObject labelObject;

    // Use this for initialization
    void Start() {

    }

    // Update is called once per frame
    void Update() {

    }

    void OnClick() {
        labelObject.SetActive(!labelObject.activeSelf);
    }
}