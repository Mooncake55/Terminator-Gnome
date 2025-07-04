using UnityEngine;
using System;

public class CameraController : MonoBehaviour
{
    [SerializeField] float screenHeight = 14f;  // En units/tiles
    [SerializeField] float screenWidth = 24f;
    [SerializeField] private float cameraSpeed = 6f;

    private Transform player;
    private Vector2 currentScreenCoord;
    private Vector3 targetPosition;
    private bool isMoving = false;
    private Vector3 originPoint;

    public event Action OnCameraMoved;

    private void Start()
    {
        GetPlayer();

        // Guardamos la posición inicial de la cámara como origen de la grilla
        originPoint = new Vector3(
            transform.position.x - (screenWidth / 2f),
            transform.position.y - (screenHeight / 2f),
            transform.position.z
        );

        currentScreenCoord = CalculatePositionOnGrid(player.position);
        targetPosition = GetCenterOfScreen(currentScreenCoord);
        transform.position = targetPosition;
    }

    private void GetPlayer()
    {
        player = GameManager.instance.GetPlayer().transform;
    }

    private void Update()
    {
        if (player == null)
        {
            GetPlayer();
            return;
        }

        Vector2 newScreenCoord = CalculatePositionOnGrid(player.position);

        if (newScreenCoord != currentScreenCoord && !isMoving)
        {
            currentScreenCoord = newScreenCoord;
            targetPosition = GetCenterOfScreen(currentScreenCoord);
            isMoving = true;
            OnCameraMoved?.Invoke();
            Debug.Log("Cambio de pantalla");
        }

        if (isMoving)
        {
            transform.position = Vector3.MoveTowards(transform.position, targetPosition, cameraSpeed * Time.deltaTime);

            if (Vector3.Distance(transform.position, targetPosition) < 0.01f)
            {
                transform.position = targetPosition;
                isMoving = false;

            }
            OnCameraMoved?.Invoke();
        }
    }

    Vector2 CalculatePositionOnGrid(Vector3 position)
    {
        float relativeX = position.x - originPoint.x;
        float relativeY = position.y - originPoint.y;

        int col = Mathf.FloorToInt(relativeX / screenWidth);
        int row = Mathf.FloorToInt(relativeY / screenHeight);
        return new Vector2(col, row);
    }

    Vector3 GetCenterOfScreen(Vector2 screenCoord)
    {
        float x = originPoint.x + (screenCoord.x * screenWidth) + screenWidth / 2f;
        float y = originPoint.y + (screenCoord.y * screenHeight) + screenHeight / 2f;
        return new Vector3(x, y, transform.position.z);
    }
}


//float screenHeight;
//float screenWidth;
//private Transform player;
//Vector3 currentPos;
//Vector3 newPos;
//[SerializeField] private float cameraSpeed = 6f;
//public event Action OnCameraMoved;
//[SerializeField] private Vector2 screenOriginOffset = Vector2.zero;

//private void Start()
//{
//    screenHeight = Camera.main.orthographicSize * 2f; //whole screen Height
//    screenWidth = screenHeight * Camera.main.aspect;
//    GameManager.instance.OnPlayerSpawn += SetPlayer;
//    SetFirstPosition();
//    screenOriginOffset = GameManager.instance.GetPlayerSpawnPoint() - new Vector2(screenWidth / 2f, screenHeight / 2f);
//}
//void SetFirstPosition()
//{
//    Vector3 spawn = GameManager.instance.GetPlayerSpawnPoint();
//    //Debug.Log($"SpawnPosition: {spawn}");
//    Vector3 initialPos = new Vector3(spawn.x, spawn.y, transform.position.z);
//    transform.position = initialPos;
//    //Debug.Log($"InitialPosition: {initialPos}");
//    currentPos = initialPos;
//}
//void SetPlayer()
//{
//    player = GameManager.instance.GetPlayer().transform;
//}
//private void Update()
//{
//    currentPos = transform.position;
//    CalculatePositionCamera();
//}
//void CalculatePositionCamera()
//{
//    if (player == null) { SetPlayer(); return; }
//    // Restamos el offset al jugador antes de calcular en qué pantalla está
//    Vector2 relativePos = new Vector2(
//        player.position.x - screenOriginOffset.x,
//        player.position.y - screenOriginOffset.y
//    );
//    //The screen starts between the screen x(times) the height or width (if I add size or width/2 it is half)
//    //each screen indicates as in a matrix the position of the player either for rows(y) and columns(x)

//    //defines the current row and column 
//    //int characterScreenY = Mathf.FloorToInt(player.position.y / screenHeight);
//    //int characterScreenX = Mathf.FloorToInt(player.position.x / screenWidth);

//    int characterScreenY = Mathf.FloorToInt(relativePos.y / screenHeight);
//    int characterScreenX = Mathf.FloorToInt(relativePos.x / screenWidth);

//    //positions the camera so that the player is in the middle of the screen
//    float cameraY = (characterScreenY * screenHeight) + (screenHeight / 2 + screenOriginOffset.y);
//    float cameraX = (characterScreenX * screenWidth) + (screenWidth / 2 + screenOriginOffset.x);

//    newPos = new Vector3(cameraX, cameraY, transform.position.z);
//    //it will only affect if the screen has changed
//    transform.position = Vector3.Lerp(transform.position, newPos, Time.deltaTime * cameraSpeed);
//    ScreenHasChanged();
//}

////detects if the camera has end its movement and notifies
//void ScreenHasChanged()
//{
//    if (Vector3.Distance(transform.position, newPos) < 0.01f && currentPos != newPos)
//    {
//        transform.position = newPos;
//        currentPos = newPos;
//        Debug.Log("Cambio de pantalla");
//        OnCameraMoved?.Invoke();
//    }
//}