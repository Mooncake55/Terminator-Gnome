using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

namespace EnemyMelee
{
        public class EnemyController : MonoBehaviour
    {
        [SerializeField] private Transform target;

        private Transform _target;
        private NavMeshAgent _agent;

        void Start()
        {
            _agent = GetComponent<NavMeshAgent>();
            _agent.updateRotation = false;
            _agent.updateUpAxis = false;
        }
        void Update()
        {
            if (target == null) return;
            _agent.SetDestination(target.position);
        
        }

        public void OnTriggerEnter2D(Collider2D other)
        {
            if (other.CompareTag("Player"))
            {
                target = other.transform;
                Debug.Log("Attack Player!");
            }
        }

        public void SetTarget(Transform newTarget)
        {
            _target = target; 
            
        }
    }
}


