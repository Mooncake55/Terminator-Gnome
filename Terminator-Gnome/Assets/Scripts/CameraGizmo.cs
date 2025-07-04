using UnityEngine;

[ExecuteAlways] // Así se dibuja en modo edición y en play
public class CameraGizmo : MonoBehaviour
{
    private void OnDrawGizmos()
    {
        Camera cam = GetComponent<Camera>();
        if (cam == null || !cam.orthographic)
        {
            Debug.LogWarning("Este script requiere una cámara ortográfica en el mismo GameObject.");
            return;
        }

        float camHeight = cam.orthographicSize * 2f;
        float camWidth = camHeight * cam.aspect;

        // Dibujamos un rectángulo con Gizmos
        Gizmos.color = Color.cyan;
        Gizmos.DrawWireCube(cam.transform.position, new Vector3(camWidth, camHeight, 0));

        // Opcional: mostrar info en consola
        // Debug.Log($"Cam size (units): {camWidth} x {camHeight}");
    }
}
