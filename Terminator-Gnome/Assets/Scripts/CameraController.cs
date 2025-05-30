using System;
using TMPro;
using UnityEngine;

public class CameraController : MonoBehaviour
{
    float screenHeight; 
    float screenWidth; 
    public Transform player;
    Vector3 currentPos;
    Vector3 newPos;
    [SerializeField] private float cameraSpeed = 6f;
    public event Action OnCameraMoved;

    private void Start()
    {
        screenHeight = Camera.main.orthographicSize * 2f; //whole screen Height
        screenWidth = screenHeight * Camera.main.aspect;
    }
    private void Update()
    {
        currentPos = transform.position;
        CalculatePositionCamera();
    }
    void CalculatePositionCamera()
    {
        //The screen starts between the screen x(times) the height or width (if I add size or width/2 it is half)
        //each screen indicates as in a matrix the position of the player either for rows(y) and columns(x)

        //defines the current row and column 
        int characterScreenY = Mathf.FloorToInt(player.position.y / screenHeight);
        int characterScreenX = Mathf.FloorToInt(player.position.x / screenWidth);

        //positions the camera so that the player is in the middle of the screen
        float cameraY = (characterScreenY * screenHeight) + (screenHeight / 2);
        float cameraX = (characterScreenX * screenWidth) + (screenWidth / 2);

        newPos = new Vector3(cameraX, cameraY, transform.position.z);
        //it will only affect if the screen has changed
        transform.position = Vector3.Lerp(transform.position, newPos, Time.deltaTime * cameraSpeed); 
        ScreenHasChanged();
    }

    //detects if the camera has end its movement and notifies
    void ScreenHasChanged()
    { 
        if (Vector3.Distance(transform.position, newPos) < 0.01f && currentPos != newPos)
        {
            transform.position = newPos;
            currentPos = newPos;
            Debug.Log("Cambio de pantalla");
            OnCameraMoved?.Invoke();
        }
    }
}
