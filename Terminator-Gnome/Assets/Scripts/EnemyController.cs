using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

namespace EnemyMelee
{
        public class EnemyController : MonoBehaviour, IDamageable
    {
        [SerializeField] private Transform target;
        [SerializeField] Transform joint; //ataque  
        private MeleeAttack meleeAttack; //ataque
        [SerializeField] private LayerMask playerLayer;
        [SerializeField] private float atkDuration;
        Coroutine attackCoroutine;
        HealthSystem healthSystem;


        private Transform _target;
        private NavMeshAgent _agent;
        private Vector2 _startPosition;
        private Vector2 direction;

        void Start()
        {
            _agent = GetComponent<NavMeshAgent>();
            _agent.updateRotation = false;
            _agent.updateUpAxis = false;
            _startPosition = transform.position;
            SearchMeleeAttack();
            healthSystem = GetComponent<HealthSystem>();
            meleeAttack = GetComponent<MeleeAttack>();
            
            healthSystem.OnDeath += HandelDeath;
        }
        void Update()
        {
            if (_target == null) return;
            _agent.SetDestination(target.position);
            direction = _agent.velocity.normalized;
            CheckForAttack();

        }
        public void CheckForAttack()
        {
            //SearchMeleeAttack();
            if (direction != Vector2.zero)
            {
                // Dibuja la línea de detección (solo visual)
                Debug.DrawRay(transform.position, direction * 0.16f, Color.red);

                RaycastHit2D hit = Physics2D.Raycast(transform.position, direction, 0.16f, playerLayer);
                if (hit.collider != null && hit.collider.CompareTag("Player") && attackCoroutine == null)
                {
                    attackCoroutine =  StartCoroutine(Attack());
                }
            }
        }
        public void SearchMeleeAttack()
        {
            if (joint != null)
            {
                Transform meleeAttackObj = joint.Find("MeleeAtk");
                if (meleeAttackObj != null)
                {
                    meleeAttack = meleeAttackObj.GetComponent<MeleeAttack>();
                }
            }
        }

        public IEnumerator Attack()
        {
            if (meleeAttack != null)
            {
                meleeAttack.ActivateAttack(direction, 1f, joint);
            }
            yield return new WaitForSeconds(atkDuration);
            attackCoroutine = null;
        }
        

        public void SetTarget(Transform newTarget)
        {
            _target = target;
            if (_target == null)
            {
                _agent.SetDestination (_startPosition);
            }
        }

        public void HandleDamage(int amount)
        {
            Debug.Log("Daño");
            healthSystem.TakeDamage(5);
            ChangeColour(2f);
        }
        public IEnumerator ChangeColour(float seconds)
        {
            //isTakingDamage = true;
            SpriteRenderer sprite = GetComponent<SpriteRenderer>();
            Color original = sprite.color;
            sprite.color = Color.red;
            yield return new WaitForSeconds(seconds);
            sprite.color = original;
            //isTakingDamage = true;
            //damageCoroutine = null;
        }
        void HandelDeath()
        {
            Debug.Log("Muerto");
            gameObject.SetActive(false);
        }
    }
}


