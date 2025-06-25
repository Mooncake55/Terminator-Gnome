using UnityEngine;
using System.Collections;


public class AttackController : MonoBehaviour
{
    [SerializeField] private MeleeAttack meleeAttack;
    public AttackData meleData;
    [SerializeField] private MeleeAttack rangeAttack;
    public AttackData rangeData;

    [SerializeField] Transform joint;
    //[SerializeField] float rangeDamage = 55f;

    private Player player;
    private Animator animator;
    private bool isAttacking = false;

    private Coroutine currentCoroutine;
    [SerializeField] SpriteRenderer hitBox;
    void Start()
    {
        //SearchAttacks();
        player = GetComponent<Player>();
        animator = player.GetAnimator();
        
    }

    public Transform GetJoint() { return joint; }
    public Vector2 GetFaceTo() { return player.GetFacingTo(); }


    public void ExecuteMeleeAttack()
    {
        //float atkDuration = player.GetPlayerData().meleeAtkDuration;
        //meleeAttack.SetDamage(player.GetPlayerData().meleeAtkDamage);
        //meleeAttack.ActivateAttack(player.GetFacingTo(), atkDuration, joint, "isMeleeAttacking");
        //StartCoroutine(WaitSeconds(atkDuration));
        if (!isAttacking) 
        { 
            isAttacking = true;
            BasicSwordAttackState attack = new BasicSwordAttackState(this, meleData, "isMeleeAttacking", meleeAttack);
            attack.Execute(player.GetFacingTo());
        }
    }
    public void ExecuteRangeAttack()
    {
        hitBox.enabled = true;
        //Debug.Log("rangeAtk");
        //if(player == null) { return;}
        //float atkDuration = player.GetPlayerData().rangeAtkDuration;
        //rangeAttack.SetDamage(rangeDamage); //CAMBIAR
        Vector2 direction = (InputController.instance.GetMousePos() - (Vector2)transform.position).normalized;
        //rangeAttack.ActivateAttack(direction, atkDuration, joint);
        //rangeAttack.ActivateAttack(direction, atkDuration, joint, "isRangeAttacking");
        //StartCoroutine(WaitSeconds(atkDuration));  
        if (!isAttacking) 
        { 
            isAttacking = true;  
            BasicSwordAttackState attack = new BasicSwordAttackState(this, rangeData, "isRangeAttacking", rangeAttack);
            player.DashTo(direction, true);
            attack.Execute(direction);
        }
    }
    public void HasFinishAttack()
    {
        hitBox.enabled = false;
        isAttacking = false;
    }
    //IEnumerator AttackCoroutine(int option)
    //{
    //    float cooldDown = data.meleeAtkDuration;
    //    //isAttacking = true;
    //    if (option == 0) { attackController.ExecuteMeleeAttack(); }
    //    else
    //    {
    //        attackController.ExecuteRangeAttack();
    //        cooldDown = data.rangeAtkCoolDown;
    //    }
    //    yield return new WaitForSeconds(cooldDown);
    //    attackCoroutine = null;
    //}
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

    public IEnumerator WaitSeconds(float duration)
    {
        yield return new WaitForSeconds(duration);
    }

}
