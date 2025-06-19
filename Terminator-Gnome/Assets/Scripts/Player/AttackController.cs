using UnityEngine;
using System.Collections;


public class AttackController : MonoBehaviour
{
    private MeleeAttack meleeAttack;
    private MeleeAttack rangeAttack;

    [SerializeField] Transform joint;
    [SerializeField] float rangeDamage = 55f;

    private Player player;
    private Animator animator;
    private bool isRangeAtk;

    void Start()
    {
        SearchAttacks();
        animator = player.GetAnimator();
    }
    public void SetPlayer(Player data){
        player = data;
    }

    
    public void ExecuteMeleeAttack()
    {
        float atkDuration = player.GetPlayerData().meleeAtkDuration;
        meleeAttack.SetDamage(player.GetPlayerData().meleeAtkDamage);
        meleeAttack.ActivateAttack(player.GetFacingTo(), atkDuration, joint, "isMeleeAttacking");
        StartCoroutine(WaitSeconds(atkDuration));
    }
    public void ExecuteRangeAttack()
    {
        Debug.Log("rangeAtk");
        if(player == null) { return;}
        float atkDuration = player.GetPlayerData().rangeAtkDuration;
        rangeAttack.SetDamage(rangeDamage); //CAMBIAR
        Vector2 direction = (InputController.instance.GetMousePos() - (Vector2)transform.position).normalized;
        //rangeAttack.ActivateAttack(direction, atkDuration, joint);
        rangeAttack.ActivateAttack(direction, atkDuration, joint, "isRangeAttacking");
        StartCoroutine(WaitSeconds(atkDuration));
        player.DashTo(direction, true);
    }
    void  SearchAttacks()
    {
        if (joint != null)
        {
            Transform meleeAttackObj = joint.Find("MeleeAtk");
            Transform rangeAttackObj = joint.Find("RangeAtk");
            if (meleeAttackObj != null)
            {
                meleeAttack = meleeAttackObj.GetComponent<MeleeAttack>();
            }
            if(rangeAttackObj != null)
            {
                rangeAttack = rangeAttackObj.GetComponent<MeleeAttack>();
            }
        }
    }
    public IEnumerator WaitSeconds(float duration)
    {
        yield return new WaitForSeconds(duration);
    }

}
