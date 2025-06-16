using UnityEngine;
using System.Collections;
using UnityEditor.Experimental.GraphView;

public class AttackController : MonoBehaviour
{
    private MeleeAttack meleeAttack;
    private MeleeAttack rangeAttack;

    [SerializeField] Transform joint;

    private Player player;

    void Start()
    {
        SearchAttacks();
    }
    public void SetPlayer(Player data){
        player = data;
    }

    
    public void ExecuteMeleeAttack()
    {
        float atkDuration = player.GetPlayerData().meleeAtkDuration;
        meleeAttack.SetDamage(player.GetPlayerData().meleeAtkDamage);
        meleeAttack.ActivateAttack(player.GetFacingTo(), atkDuration, joint);
        StartCoroutine(WaitSeconds(atkDuration));
    }
    public void ExecuteRangeAttack()
    {
        float atkDuration = player.GetPlayerData().meleeAtkDuration;
        rangeAttack.SetDamage(100); //CAMBIAR
        Vector2 direction = (InputController.instance.GetMousePos() - (Vector2)transform.position).normalized;
        rangeAttack.ActivateAttack(direction, atkDuration, joint);
        StartCoroutine(WaitSeconds(atkDuration));
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
