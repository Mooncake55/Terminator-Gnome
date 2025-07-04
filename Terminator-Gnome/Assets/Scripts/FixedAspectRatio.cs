using UnityEngine;

[RequireComponent(typeof(Camera))]
[ExecuteAlways]
public class FixedAspectRatio : MonoBehaviour
{
    public Vector2 targetAspect = new Vector2(24, 14); // o 12:7

    private Camera cam;

    void Start()
    {
        cam = GetComponent<Camera>();
        UpdateViewport();
    }

    void Update()
    {
        if (Application.isPlaying == false)
        {
            UpdateViewport(); // también ajusta en modo editor
        }
    }

    void UpdateViewport()
    {
        float target = targetAspect.x / targetAspect.y;
        float window = (float)Screen.width / Screen.height;

        if (Mathf.Approximately(window, target))
        {
            cam.rect = new Rect(0, 0, 1, 1);
        }
        else if (window > target)
        {
            // Ventana más ancha que el target ? bandas negras laterales
            float width = target / window;
            float offsetX = (1f - width) / 2f;
            cam.rect = new Rect(offsetX, 0, width, 1);
        }
        else
        {
            // Ventana más alta que el target ? bandas negras arriba/abajo
            float height = window / target;
            float offsetY = (1f - height) / 2f;
            cam.rect = new Rect(0, offsetY, 1, height);
        }
    }
}
