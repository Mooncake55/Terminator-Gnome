using TMPro;
using UnityEngine;

public class CameraController : MonoBehaviour
{
    float screenHeight; //alura pantalla (camerasizex2)
    float screenWidth; //ancho pantalla
    public Transform character;
    private void Start()
    {
        screenHeight = Camera.main.orthographicSize * 2f;
        screenWidth = screenHeight * Camera.main.aspect;
    }
    private void Update()
    {
        CalculatePositionCamera();
    }
    void CalculatePositionCamera()
    {
        //la pantalla empieza entre npantalla x altura o anchura (si le sumo size o width/2 es la mitad)
        int characterScreenY = Mathf.FloorToInt(character.position.y / screenHeight);
        int characterScreenX = Mathf.FloorToInt(character.position.x / screenWidth);

        float cameraY = (characterScreenY * screenHeight) + (screenHeight / 2);
        float cameraX = (characterScreenX * screenWidth) + (screenWidth / 2);

        transform.position = new Vector3(cameraX, cameraY, transform.position.z);
        Debug.Log($"ScrrenY: "+ characterScreenY);
        Debug.Log($"ScrrenX: "+ characterScreenX);
    }
}
