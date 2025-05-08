using System;
using TMPro;
using UnityEngine;

public class CameraController : MonoBehaviour
{
    float screenHeight; //alura pantalla (camerasizex2)
    float screenWidth; //ancho pantalla
    public Transform character;
    Vector3 currentPos;
    Vector3 newPos;
    [SerializeField] private float cameraSpeed = 6f;
    public event Action OnCameraMoved;

    private void Start()
    {
        screenHeight = Camera.main.orthographicSize * 2f;
        screenWidth = screenHeight * Camera.main.aspect;
    }
    private void Update()
    {
        currentPos = transform.position;
        CalculatePositionCamera();
        //ScreenHasChanged();
    }
    void CalculatePositionCamera()
    {
        //la pantalla empieza entre npantalla x altura o anchura (si le sumo size o width/2 es la mitad)
        int characterScreenY = Mathf.FloorToInt(character.position.y / screenHeight);
        int characterScreenX = Mathf.FloorToInt(character.position.x / screenWidth);

        float cameraY = (characterScreenY * screenHeight) + (screenHeight / 2);
        float cameraX = (characterScreenX * screenWidth) + (screenWidth / 2);

        //transform.position = new Vector3(cameraX, cameraY, transform.position.z);
        //Debug.Log($"ScrrenY: "+ characterScreenY);
        //Debug.Log($"ScrrenX: "+ characterScreenX);
        newPos = new Vector3(cameraX, cameraY, transform.position.z);
        transform.position = Vector3.Lerp(transform.position, newPos, Time.deltaTime * cameraSpeed);
        ScreenHasChanged();
    }
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
