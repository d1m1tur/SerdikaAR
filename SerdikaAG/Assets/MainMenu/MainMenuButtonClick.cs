using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MainMenuButtonClick : MonoBehaviour {

    //Object with a name = the button name + info
    public GameObject infoToChange;
    public GameObject defaultInfo;

    // Use this for initialization
    void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		
	}

    public void OnClick() {
        defaultInfo.SetActive(infoToChange.activeSelf);
        infoToChange.SetActive(!infoToChange.activeSelf);
    }
}
