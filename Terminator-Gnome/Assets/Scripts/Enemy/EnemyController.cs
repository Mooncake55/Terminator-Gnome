using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;
using UnityEngine.InputSystem.LowLevel;

public class EnemyController : MonoBehaviour, IDamageable
{
    [SerializeField] private List<MonoBehaviour> stateComponents; // Para asignar en el inspector
    private List<IEnemyState> statesList = new List<IEnemyState>();

    [SerializeField] private Transform target;
    //[SerializeField] Transform joint; //ataque  
    //private MeleeAttack meleeAttack; //ataque
    //[SerializeField] private LayerMask playerLayer;
    //[SerializeField] private float atkDuration;
    //[SerializeField] private float attackRange = 0.5f;
    //private NavMeshAgent _agent;

    //Coroutine attackCoroutine;
    HealthSystem healthSystem;
    //IEnemyState movementState;
    IEnemyState currentState;
    //IEnemyState attackState;

    void Awake()
    {
        //foreach (var stateComponent in stateComponents)
        //{
        //    if (stateComponent is IEnemyState state)
        //    {
        //        statesList.Add(state);
        //    }
        //    else
        //    {
        //        Debug.LogWarning($"{stateComponent.name} no implementa IEnemyState");
        //    }
        //}
    }

    void Start()
    {
        SetState("InitialState");
    }

    public void SetState(string stateTag)
    {
        foreach (var state in stateComponents)
        {
            if (state != null && state.CompareTag(stateTag))
            {
                var newState = state as IEnemyState;
                if (newState != null)
                {
                    currentState = newState;
                    currentState.Enter();
                    break; // Opcional: rompe el bucle al encontrar el primer estado
                }
            }
        }
        //algo para crear un estado por el caso de que no haya uno
    }

    void Update()
    {
        currentState.UpdateAction(); //para que no sea el estsado quien tenga el update (monobehaviour)
    }

    //public void SetState(IEnemyState newState)
    //{
    //    currentState = newState;
    //    currentState.Enter();
    //}
    //public void SetState(string newStateTag)
    //{

    //    currentState = newState;
    //    currentState.Enter();
    //}

    public void HandleDamage(int amount)
    {
        throw new System.NotImplementedException();
    }
    public Transform GetTarget()
    {
        return target;
    }
}



