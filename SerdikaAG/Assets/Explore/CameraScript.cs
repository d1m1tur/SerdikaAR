using UnityEngine;
using UnityEngine.UI;
using System.Linq;
using System.Collections;

public class CameraScript : MonoBehaviour {

    public RawImage image;
    public RectTransform imageParent;
    public AspectRatioFitter imageFitter;
    public Canvas canvas;

    // Device cameras
    WebCamDevice cameraDevice;

    WebCamTexture cameraTexture;
    // Image rotation
    Vector3 rotationVector = new Vector3(0f, 0f, 0f);

    // Image uvRect
    Rect defaultRect = new Rect(0f, 0f, 1f, 1f);
    Rect fixedRect = new Rect(0f, 1f, 1f, -1f);

    // Image Parent's scale
    Vector3 defaultScale = new Vector3(1f, 1f, 1f);
    Vector3 fixedScale = new Vector3(-1f, 1f, 1f);

    // Use this for initialization
    void Start()
    {
        // Check for device cameras
        if (WebCamTexture.devices.Length == 0)
        {
            Debug.Log("No devices cameras found");
            return;
        }

        cameraDevice = WebCamTexture.devices.First();
        cameraTexture = new WebCamTexture(cameraDevice.name);
        cameraTexture.filterMode = FilterMode.Trilinear;

        image.texture = cameraTexture;
        image.material.mainTexture = cameraTexture;

        cameraTexture.Play();

        float scaleToFullscreen = canvas.GetComponent<RectTransform>().rect.height / image.rectTransform.rect.height;

        defaultScale = new Vector3(scaleToFullscreen, scaleToFullscreen, 1f);
        fixedScale = new Vector3(-scaleToFullscreen, scaleToFullscreen, 1f);
    }

    // Update is called once per frame
    void Update () {
        // Skip making adjustment for incorrect camera data
        if (cameraTexture.width < 100)
        {
            Debug.Log("Still waiting another frame for correct info...");
            return;
        }

        // Rotate image to show correct orientation 
        rotationVector.z = -cameraTexture.videoRotationAngle;
        image.rectTransform.localEulerAngles = rotationVector;

        // Set AspectRatioFitter's ratio
        float videoRatio =
            (float)cameraTexture.width / (float)cameraTexture.height;
        imageFitter.aspectRatio = videoRatio;

        // Unflip if vertically flipped
        image.uvRect =
            cameraTexture.videoVerticallyMirrored ? fixedRect : defaultRect;

        // Mirror front-facing camera's image horizontally to look more natural
        imageParent.localScale =
            cameraDevice.isFrontFacing ? fixedScale : defaultScale;
    }
}
