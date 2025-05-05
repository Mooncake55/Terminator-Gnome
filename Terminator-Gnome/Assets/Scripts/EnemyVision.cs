using System;
using EnemyMelee;
using UnityEngine;

namespace EnemyMelee
{
    [RequireComponent(typeof(CircleCollider2D))]
    public class EnemyVision : MonoBehaviour
    {
        [SerializeField] private EnemyController _enemyController;
        private CircleCollider2D _circleCollider2D;

        private void Start()
        {
            _circleCollider2D = GetComponent<CircleCollider2D>();
        }

        public void OnTriggerEnter2D(Collider2D other)
        {
            if (!other.CompareTag("Player")) return;
            {
                Vector3 playerDirection = other.transform.position - transform.position;
                RaycastHit2D rayCast = Physics2D.Raycast(transform.position, playerDirection, _circleCollider2D.radius);
                
                if (rayCast.collider == null) return;
                
                if (rayCast.collider.CompareTag("Player"))
                {
                   _enemyController.SetTarget(other.transform);
                    Debug.Log("Player Detected!");
                }

               
            }
        }
    }
}