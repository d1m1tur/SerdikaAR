using UnityEngine;
using System.Collections;
using UnityEngine.SceneManagement;
using Vuforia;

public class ImageRecognised : MonoBehaviour, ITrackableEventHandler
{

    private TrackableBehaviour mTrackableBehaviour;

    private bool imageIsRecognised = false;

    void Start()
    {
        mTrackableBehaviour = GetComponent<TrackableBehaviour>();
        if (mTrackableBehaviour)
        {
            mTrackableBehaviour.RegisterTrackableEventHandler(this);
        }
    }

    public void OnTrackableStateChanged(
                                    TrackableBehaviour.Status previousStatus,
                                    TrackableBehaviour.Status newStatus)
    {
        if (newStatus == TrackableBehaviour.Status.DETECTED ||
            newStatus == TrackableBehaviour.Status.TRACKED)
        {
            PlayerPrefs.SetString("previousScene", SceneManager.GetActiveScene().name);
            SceneManager.LoadScene("NevskyInfo");
        }
    }
}