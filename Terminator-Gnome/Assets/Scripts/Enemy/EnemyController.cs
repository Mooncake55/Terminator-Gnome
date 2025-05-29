using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;
using UnityEngine.InputSystem.LowLevel;

public class EnemyController : MonoBehaviour, IDamageable
{
    public EnemyData enemyData;

    //MovementState
    private NavMeshAgent _agent;
    [SerializeField] private Transform target;
    private Vector2 facingTo;

    //AttackState
    [SerializeField] Transform joint;

    //[SerializeField] private List<MonoBehaviour> stateComponents; // Para asignar en el inspector
    private Coroutine currentCoroutine;
    HealthSystem healthSystem;
    IEnemyState currentState;

    
    void Awake()
    {
        var agent = GetComponent<NavMeshAgent>();
        agent.updateRotation = false;
        agent.updateUpAxis = false;
    }
    void Start()
    {
        healthSystem = GetComponent<HealthSystem>();
        healthSystem.OnDeath += HandelDeath;
        healthSystem.SetLifePoints(enemyData.lifePoints);
        Debug.Log("NUEVO ENEMIGO");
        if(currentState == null)
        {
            SetState(new MeleeEnemyIdleState(this));
        }
    }

    public void SetState(IEnemyState newState)
    {
        Debug.Log("CAMBIANDO DE ESTADO");
        currentState = newState;
    }

    void Update()
    {
        if(currentState != null) { currentState.UpdateAction(); }
        //currentState.UpdateAction(); //para que no sea el estsado quien tenga el update (monobehaviour)
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

    //SETTERS Y GETTERS
    public Transform GetTarget() { return target; }
    public Transform GetJoint() { return joint; }
    public void SetFaceTo(Vector2 direction) { facingTo = direction; }
    public Vector2 GetFaceTo() { return facingTo; }
    public EnemyData GetEnemyData() { return enemyData; }   

    //MANEJO DE CORRUTINAS 
    
    public void StartStateCoroutine(IEnumerator coroutine)
    {
        if (currentCoroutine != null)
        {
            StopCoroutine(currentCoroutine);
        }

        currentCoroutine = StartCoroutine(coroutine);
    }

    public void StopCurrentCoroutine()
    {
        if (currentCoroutine != null)
        {
            StopCoroutine(currentCoroutine);
            currentCoroutine = null;
        }
    }


}
//public void AddState(MonoBehaviour newState) //por si las dudas :p
//{
//    stateComponents.Add(newState);
//}
//void Start()
//{
//    healthSystem = GetComponent<HealthSystem>();
//    healthSystem.OnDeath += HandelDeath;
//    Debug.Log("ENTRANDO AL ENEMIGO");
//    SetState("InitialState");
//}
//public void SetState(string stateTag)
//{
//    Debug.Log($"Tratando de entra al Estado: " + stateTag);
//    foreach (var state in stateComponents)
//    {
//        if (state != null && state.CompareTag(stateTag))
//        {
//            var newState = state as IEnemyState;
//            if (newState != null)
//            {
//                currentState = newState;
//                Debug.Log($"Entrando  al Estado: " + stateTag);
//                currentState.SetContext(this);
//                currentState.Enter();
//                break; // Opcional: rompe el bucle al encontrar el primer estado
//            }
//            Debug.Log($"No se encontro el estado: " + stateTag);
//        }
//    }
//    Debug.Log($"Estado Actual: " + currentState);
//    //algo para crear un estado por el caso de que no haya uno
//}

