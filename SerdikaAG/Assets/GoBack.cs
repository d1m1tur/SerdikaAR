using System.Collections;
using System.Collections.Generic;
using UnityEngine.SceneManagement;
using UnityEngine;

public class GoBack : MonoBehaviour {

    public GameObject mainScreen;
    public GameObject[] otherScreen;

    // Use this for initialization
    void Start () {
		
	}

	// Update is called once per frame
	void Update () {
        if (Input.GetKeyDown(KeyCode.Escape)) {
            if(mainScreen.activeSelf) {
                this.Back_Scene();
            } else {
                mainScreen.SetActive(true);
                for (int i = 0; i < otherScreen.Length; i++) {
                    otherScreen[i].SetActive(false);
                }
            }
        }
    }

    public void Back_Scene() {
        string previousScene = PlayerPrefs.GetString("previousScene");
        SceneManager.LoadScene(previousScene);
    }
}

