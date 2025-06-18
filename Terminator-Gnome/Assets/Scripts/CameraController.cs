using System;
using System.Collections;
using TMPro;
using UnityEngine;

public class CameraController : MonoBehaviour
{
    float screenHeight;
    float screenWidth;
    private Transform player;
    Vector3 currentPos;
    Vector3 newPos;
    [SerializeField] private float cameraSpeed = 6f;
    public event Action OnCameraMoved;
    [SerializeField] private Vector2 screenOriginOffset = Vector2.zero;

    private void Start()
    {
        screenHeight = Camera.main.orthographicSize * 2f; //whole screen Height
        screenWidth = screenHeight * Camera.main.aspect;
        GameManager.instance.OnPlayerSpawn += SetPlayer;
        SetFirstPosition();
        screenOriginOffset = GameManager.instance.GetPlayerSpawnPoint() - new Vector2(screenWidth / 2f, screenHeight / 2f);
    }
    void SetFirstPosition()
    {
        Vector3 spawn = GameManager.instance.GetPlayerSpawnPoint();
        Debug.Log($"SpawnPosition: {spawn}");
        Vector3 initialPos = new Vector3(spawn.x, spawn.y, transform.position.z);
        transform.position = initialPos;
        Debug.Log($"InitialPosition: {initialPos}");
        currentPos = initialPos;
    }
    void SetPlayer()
    {
        player = GameManager.instance.GetPlayer().transform;
    }
    private void Update()
    {
        currentPos = transform.position;
        CalculatePositionCamera();
    }
    void CalculatePositionCamera()
    {
        if(player == null) { SetPlayer(); return; }
        // Restamos el offset al jugador antes de calcular en qué pantalla está
        Vector2 relativePos = new Vector2(
            player.position.x - screenOriginOffset.x,
            player.position.y - screenOriginOffset.y
        );
        //The screen starts between the screen x(times) the height or width (if I add size or width/2 it is half)
        //each screen indicates as in a matrix the position of the player either for rows(y) and columns(x)

        //defines the current row and column 
        //int characterScreenY = Mathf.FloorToInt(player.position.y / screenHeight);
        //int characterScreenX = Mathf.FloorToInt(player.position.x / screenWidth);

        int characterScreenY = Mathf.FloorToInt(relativePos.y / screenHeight);
        int characterScreenX = Mathf.FloorToInt(relativePos.x / screenWidth);

        //positions the camera so that the player is in the middle of the screen
        float cameraY = (characterScreenY * screenHeight) + (screenHeight / 2 + screenOriginOffset.y);
        float cameraX = (characterScreenX * screenWidth) + (screenWidth / 2 + screenOriginOffset.x);

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
//public class CameraController : MonoBehaviour
//{
//    private float screenHeight;
//    private float screenWidth;
//    private Vector3 currentPos;
//    private Vector3 newPos;
//    private bool hasSetPlayer = false;

//    public Transform player;
//    [SerializeField] private float cameraSpeed = 6f;

//    public event Action OnCameraMoved;

//    private bool hasPlayerAssigned = false;

//    private void Awake()
//    {
//        // Establece el tamaño ortográfico para ver 14 tiles verticales
//        Camera.main.orthographicSize = 7f; // 14 tiles de alto
//    }

//    private void Start()
//    {
//        // Calcula el tamaño de pantalla en unidades del mundo
//        screenHeight = Camera.main.orthographicSize * 2f;
//        screenWidth = screenHeight * Camera.main.aspect;

//        // Se suscribe al evento del GameManager
//        GameManager.instance.OnPlayerSpawn += SetPlayer;

//        // Intenta asignar de inmediato si el jugador ya está
//        FirstPosition();
//        if (GameManager.instance.GetPlayer() != null)
//        {
//            //SetPlayer();
//            StartCoroutine(DelaySetPlayer());
//        }
//    }
//    private IEnumerator DelaySetPlayer()
//    {
//        yield return null; // Espera un frame para asegurar que el jugador fue posicionado
//        SetPlayer();
//    }

//    void SetPlayer()
//    {

//        player = GameManager.instance.GetPlayer().transform;
//        if (player == null) return;

//        hasPlayerAssigned = true;

//        // Posiciona directamente la cámara en la pantalla donde está el jugador
//        //newPos = CalculateCameraTargetPosition(player.position);
//        Vector3 startPosition = CalculateCameraTargetPosition(player.position);
//        //transform.position = newPos;
//        transform.position = startPosition;
//        //currentPos = newPos;
//        currentPos = startPosition;
//        newPos = startPosition;

//        //Debug.Log("Cámara inicial colocada en: " + newPos);
//        Debug.Log("Cámara inicial colocada en: " + startPosition);
//        hasSetPlayer = true;
//    }
//    private void FirstPosition()
//    {
//        Vector2 centerPosition = GameManager.instance.GetPlayerSpawnPoint();
//        transform.position = new Vector3(centerPosition.x, centerPosition.y, transform.position.z);
//    }
//    private void Update()
//    {
//        if (!hasPlayerAssigned || player == null) return;

//        currentPos = transform.position;
//        CalculatePositionCamera();
//    }

//    void CalculatePositionCamera()
//    {
//        //if (!hasSetPlayer) { SetPlayer(); }
//        newPos = CalculateCameraTargetPosition(player.position);
//        transform.position = Vector3.Lerp(transform.position, newPos, Time.deltaTime * cameraSpeed);
//        ScreenHasChanged();
//    }

//    Vector3 CalculateCameraTargetPosition(Vector2 targetPosition)
//    {
//        int characterScreenY = Mathf.FloorToInt((player.position.y + screenHeight / 2f) / screenHeight);
//        int characterScreenX = Mathf.FloorToInt((player.position.x + screenWidth / 2f) / screenWidth);

//        float cameraY = (characterScreenY * screenHeight) + (screenHeight / 2f);
//        float cameraX = (characterScreenX * screenWidth) + (screenWidth / 2f);

//        return new Vector3(cameraX, cameraY, transform.position.z);
//    }

//    void ScreenHasChanged()
//    {
//        if (Vector3.Distance(transform.position, newPos) < 0.01f && currentPos != newPos)
//        {
//            transform.position = newPos;
//            currentPos = newPos;
//            OnCameraMoved?.Invoke();
//        }
//    }
//}

//public class CameraController : MonoBehaviour
//{
//    float screenHeight;
//    float screenWidth;
//    public Transform player;
//    Vector3 currentPos;
//    Vector3 newPos;
//    [SerializeField] private float cameraSpeed = 6f;
//    public event Action OnCameraMoved;
//    private bool hasPlayerAsigned = false;
//    private bool hasInitialized = false;

//    private void Start()
//    {
//        screenHeight = Camera.main.orthographicSize * 2f; //whole screen Height
//        screenWidth = screenHeight * Camera.main.aspect;
//        FirstPosition();
//        if (GameManager.instance.GetPlayer() != null)
//        {
//            SetPlayer();
//        }
//        GameManager.instance.OnPlayerSpawn += SetPlayer;


//        //GameManager.instance.OnPlayerSpawn += () =>
//        //{
//        //    SetPlayer(); // Esto se llama exactamente cuando el jugador se instancia y está listo
//        //};
//    }
//    //void SetPlayer()
//    //{
//    //    player = GameManager.instance.GetPlayer().transform;
//    //}
//    private void FirstPosition()
//    {
//        Vector2 centerPosition = GameManager.instance.GetPlayerSpawnPoint();
//        transform.position = new Vector3(centerPosition.x, centerPosition.y, transform.position.z);
//    }

//    void SetPlayer()
//    {
//        player = GameManager.instance.GetPlayer().transform;
//        if (player == null) return;

//        hasPlayerAsigned = true;

//        // Calcular en qué "pantalla" está el jugador
//        int characterScreenY = Mathf.FloorToInt(player.position.y / screenHeight);
//        int characterScreenX = Mathf.FloorToInt(player.position.x / screenWidth);

//        float cameraY = (characterScreenY * screenHeight) + (screenHeight / 2f);
//        float cameraX = (characterScreenX * screenWidth) + (screenWidth / 2f);

//        Vector3 startPosition = new Vector3(cameraX, cameraY, transform.position.z);

//        // Asignar directamente la posición
//        transform.position = startPosition;
//        currentPos = startPosition;
//        newPos = startPosition;

//        Debug.Log("Cámara posicionada inicialmente en: " + startPosition);
//    }
//    private void Update()
//    {
//        Debug.Log("Llamando desde UpdateCamera");
//        if (player == null) { return; }
//        Debug.Log("AAAAAAAAA");
//        //if (!hasPlayerAsigned) { SetPlayer(); return; }
//        //LateUpdate();
//        Debug.DrawLine(transform.position, player.position, Color.green); // línea visible en escena
//        Debug.Log("Pos jugador: " + player.position);
//        currentPos = transform.position;
//        CalculatePositionCamera();
//    }

//    //private void LateUpdate()
//    //{
//    //    if (!hasInitialized && player != null)
//    //    {
//    //        ForceInitialCameraPosition();
//    //        hasInitialized = true;
//    //    }
//    //}
//    //private void ForceInitialCameraPosition()
//    //{
//    //    int characterScreenY = Mathf.FloorToInt(player.position.y / screenHeight);
//    //    int characterScreenX = Mathf.FloorToInt(player.position.x / screenWidth);

//    //    float cameraY = (characterScreenY * screenHeight) + (screenHeight / 2);
//    //    float cameraX = (characterScreenX * screenWidth) + (screenWidth / 2);

//    //    Vector3 firstPosition = new Vector3(cameraX, cameraY, transform.position.z);
//    //    transform.position = firstPosition;
//    //    currentPos = firstPosition;
//    //}
//    void CalculatePositionCamera()
//    {
//        newPos = CalculateCameraTargetPosition(player.position);

//        // Desplazamiento suave
//        transform.position = Vector3.Lerp(transform.position, newPos, Time.deltaTime * cameraSpeed);
//        ScreenHasChanged();
//    }
//    Vector3 CalculateCameraTargetPosition(Vector2 targetPosition)
//    {
//        int characterScreenY = Mathf.FloorToInt(targetPosition.y / screenHeight);
//        int characterScreenX = Mathf.FloorToInt(targetPosition.x / screenWidth);

//        float cameraY = (characterScreenY * screenHeight) + (screenHeight / 2);
//        float cameraX = (characterScreenX * screenWidth) + (screenWidth / 2);

//        return new Vector3(cameraX, cameraY, transform.position.z);
//    }
//    //void CalculatePositionCamera()
//    //{
//    //    //The screen starts between the screen x(times) the height or width (if I add size or width/2 it is half)
//    //    //each screen indicates as in a matrix the position of the player either for rows(y) and columns(x)

//    //    //defines the current row and column 
//    //    int characterScreenY = Mathf.FloorToInt(player.position.y / screenHeight);
//    //    int characterScreenX = Mathf.FloorToInt(player.position.x / screenWidth);

//    //    //positions the camera so that the player is in the middle of the screen
//    //    float cameraY = (characterScreenY * screenHeight) + (screenHeight / 2);
//    //    float cameraX = (characterScreenX * screenWidth) + (screenWidth / 2);

//    //    newPos = new Vector3(cameraX, cameraY, transform.position.z);
//    //    //it will only affect if the screen has changed
//    //    transform.position = Vector3.Lerp(transform.position, newPos, Time.deltaTime * cameraSpeed); 
//    //    ScreenHasChanged();
//    //}

//    //detects if the camera has end its movement and notifies
//    void ScreenHasChanged()
//    {
//        //antes 0.01f
//        if (Vector3.Distance(transform.position, newPos) < 0.1f && currentPos != newPos)
//        {
//            transform.position = newPos;
//            currentPos = newPos;
//            Debug.Log("Cambio de pantalla");
//            OnCameraMoved?.Invoke();
//        }
//    }
//}
