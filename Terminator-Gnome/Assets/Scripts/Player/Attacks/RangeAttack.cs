using Unity.VisualScripting;
using UnityEngine;


//public class RangeAttack : MonoBehaviour, IAttack
//{
//    private float range;
//    private Transform playerTransform;
//    private float damage;
//    private Collider2D[] hits;
//    private Vector2 dir;
//    [SerializeField] private float coneAngle;

//    private void Start()
//    {
//        range = GetComponent<CircleCollider2D>().radius;
//    }
//    public void Enter()
//    {
//        Collider2D[] hits = Physics2D.OverlapCircleAll(playerTransform.position, range);
//    }
//    public void SetDirection(Vector2 direction)
//    {
//        dir = direction;
//    }
    
//    public void ExecuteAttack()
//    {
//        foreach (var hit in hits)
//        {
//            //Vector2 directionToTarget = (hit.transform.position - transform.position).normalized;
//            float angle = Vector2.Angle(transform.right, dir);
//            IDamageable damageable = hit.GetComponent<IDamageable>();

//            if ( (angle <= coneAngle / 2f) && (damageable != null) && (!hit.CompareTag("Player")))
//            {
//                // El objeto está dentro del "cono"
//                Debug.Log("¡Impacto!");
//                damageable.HandleDamage(damage);
//            }
//        }
//        OnDrawGizmosSelected();
//    }
//    private void OnDrawGizmosSelected()
//    {
//        Gizmos.color = Color.red;
//        Vector3 forward = transform.right;
//        float halfAngle = coneAngle / 2f;

//        Vector3 leftBoundary = Quaternion.Euler(0, 0, -halfAngle) * forward;
//        Vector3 rightBoundary = Quaternion.Euler(0, 0, halfAngle) * forward;

//        Gizmos.DrawLine(transform.position, transform.position + leftBoundary * range);
//        Gizmos.DrawLine(transform.position, transform.position + rightBoundary * range);
//    }
//    public void SetAngle()
//    {
//        Vector2 playerPos = transform.position;
//        dir = (InputController.instance.GetMousePos() - playerPos).normalized;
//    }
//}
