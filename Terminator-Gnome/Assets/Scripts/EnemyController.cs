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
        [SerializeField] private float attackRange = 0.5f;

        Coroutine attackCoroutine;
        HealthSystem healthSystem;



        //private Transform _target;
        private NavMeshAgent _agent;
        private Vector2 _startPosition;
        private Vector2 direction;
        private Vector2 lastTargetPosition;
        void Start()
        {
            _agent = GetComponent<NavMeshAgent>();
            _agent.updateRotation = false;
            _agent.updateUpAxis = false;
            _startPosition = transform.position;
            SearchMeleeAttack();
            healthSystem = GetComponent<HealthSystem>();
            meleeAttack = GetComponent<MeleeAttack>();
            _agent.SetDestination(target.position);
            healthSystem.OnDeath += HandelDeath;
        }
        //private void Awake()
        //{
        //    if (target == null)
        //    {
        //        GameObject playerObj = GameObject.FindGameObjectWithTag("Player");
        //        if (playerObj != null)
        //        {
        //            target = playerObj.transform;
        //            lastTargetPosition = target.position;
        //        }
        //    }
        //    SearchMeleeAttack();
        //}
        private void Awake()
        {
            if (target == null)
            {
                GameObject playerObj = GameObject.FindGameObjectWithTag("Player");
                if (playerObj != null)
                {
                    target = playerObj.transform;
                    lastTargetPosition = target.position;
                }
            }

            if (joint == null)
            {
                joint = transform.Find("EnemyJoint"); // Cambiar por el nombre real del hijo si aplica
            }

            //SearchMeleeAttack();
        }
        void Update()
        {
            //_agent.SetDestination(target.position);
            float distance = Vector3.Distance(lastTargetPosition, target.position);
            if (distance > 0.5f)
            {
                _agent.SetDestination(target.position);
                lastTargetPosition = target.position;
            }
            if (target == null) { return; }
            direction = _agent.velocity.normalized;
            CheckForAttack();

        }
        //public void CheckForAttack()
        //{
        //    ////SearchMeleeAttack();
        //    //if (direction != Vector2.zero)
        //    //{
        //    //    // Dibuja la línea de detección (solo visual)
        //    //    Debug.DrawRay(transform.position, direction * 0.16f, Color.red);

        //    //    RaycastHit2D hit = Physics2D.Raycast(transform.position, direction, 0.16f, playerLayer);
        //    //    if (hit.collider != null && hit.collider.CompareTag("Player") && attackCoroutine == null)
        //    //    {
        //    //        attackCoroutine =  StartCoroutine(Attack());
        //    //    }
        //    //}
        //    // Dibuja la línea de detección (solo visual)
        //    direction = ((Vector2)(target.position - transform.position)).normalized;
        //    if (target == null) { return; }

        //    float distanceToTarget = Vector2.Distance(transform.position, target.position);

        //    if (distanceToTarget <= attackRange && attackCoroutine == null)
        //    {
        //        attackCoroutine = StartCoroutine(Attack());
        //    }
        //}
        public void CheckForAttack()
        {
            if (target == null) return;

            float distanceToTarget = Vector2.Distance(transform.position, target.position);
            //Debug.Log("Distancia al jugador: " + distanceToTarget);

            if (distanceToTarget <= attackRange && attackCoroutine == null)
            {
                Debug.Log("¡En rango para atacar!");
                //Debug.Log(meleeAttack.name);
                direction = ((Vector2)(target.position - transform.position)).normalized;
                attackCoroutine = StartCoroutine(Attack());
            }
        }
        //public void SearchMeleeAttack()
        //{
        //    if (joint != null)
        //    {
        //        Transform meleeAttackObj = joint.Find("EnemyMeleeAtk");
        //        if (meleeAttackObj != null)
        //        {
        //            meleeAttack = meleeAttackObj.GetComponent<MeleeAttack>();
        //        }
        //    }
        //}
        public void SearchMeleeAttack()
        {
            if (joint == null)
            {
                Debug.LogWarning("Joint no asignado.");
                return;
            }

            Transform meleeAttackObj = joint.Find("EnemyMeleeAtk"); // Usá el nombre actualizado
            if (meleeAttackObj != null)
            {
                meleeAttack = meleeAttackObj.GetComponent<MeleeAttack>();
                if (meleeAttack != null)
                    Debug.Log("MeleeAttack asignado correctamente: " + meleeAttack.name);
                else
                    Debug.LogWarning("No se encontró el componente MeleeAttack en " + meleeAttackObj.name);
            }
            else
            {
                Debug.LogWarning("No se encontró el objeto hijo EnemyMeleeAtk en " + joint.name);
            }
        }

        //public IEnumerator Attack()
        //{
        //    Debug.Log("Deberia Atackar");
        //    if (meleeAttack != null)
        //    {
        //        Debug.Log("Llamando a ACtivateAttack");
        //        meleeAttack.ActivateAttack(direction, 1f, joint);
        //    }
        //    yield return new WaitForSeconds(atkDuration);
        //    attackCoroutine = null;
        //}
        public IEnumerator Attack()
        {
            Debug.Log("Debería atacar");

            if (meleeAttack == null)
            {
                Debug.LogWarning("meleeAttack era null en Attack(), intentando buscar nuevamente...");
                SearchMeleeAttack(); // Intenta recuperar la referencia en tiempo de ejecución
            }

            if (meleeAttack != null)
            {
                Debug.Log("Llamando a ActivateAttack");
                meleeAttack.ActivateAttack(direction, 1f, joint);
            }
            else
            {
                Debug.LogError("No se pudo ejecutar el ataque porque meleeAttack sigue siendo null");
            }

            yield return new WaitForSeconds(atkDuration);
            attackCoroutine = null;
        }


        public void SetTarget(Transform newTarget)
        {
            target = newTarget;
            if (target == null)
            {
                _agent.SetDestination(_startPosition);
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


