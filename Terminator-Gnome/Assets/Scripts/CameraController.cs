using UnityEngine;

public class CameraController : MonoBehaviour
{
    public Transform character;
    private float cameraSize;
    private float screenHeight;
    private float screenWidth;  

    private void Start()
    {
        cameraSize = Camera.main.orthographicSize;
        screenHeight = cameraSize * 2;
        screenWidth = screenHeight * Screen.width / Screen.height;

    }
    private void Update()
    {
        CalculateCameraPosition();
    }
    void CalculateCameraPosition()
    {
        int yCharacterScreen = (int)(character.position.y / screenHeight);
        int xCharacterScreen = (int)(character.position.x / screenWidth);
        float yCameraHeight = (yCharacterScreen * screenHeight) + cameraSize;
        float xCameraHeight = (xCharacterScreen * screenWidth) + cameraSize/2;

        transform.position = new Vector3(xCameraHeight, yCameraHeight, transform.position.z);
    }
}
