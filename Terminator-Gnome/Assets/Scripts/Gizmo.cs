using UnityEngine;

public class Gizmo : MonoBehaviour
{
    public float radius = 5f;
    public Color gizmoColor = Color.red;

    void OnDrawGizmosSelected()
    {
        Gizmos.color = gizmoColor;
        Gizmos.DrawWireSphere(transform.position, radius);
    }

    void Update()
    {
        Collider2D[] colliders = Physics2D.OverlapCircleAll(transform.position, radius);
        // Acá podés hacer lo que quieras con los colliders detectados
    }
}
