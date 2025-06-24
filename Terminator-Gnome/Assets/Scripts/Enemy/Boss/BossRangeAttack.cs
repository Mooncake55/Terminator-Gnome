using System;
using System.Collections;
using System.Drawing;
using UnityEngine;

public class BossRangeAttack : MonoBehaviour
{
    [SerializeField] private Transform[] firePoints; // Arrastrás los 3 puntos desde el inspector
    [SerializeField] private ProjectileFactory projectileFactory;
    [SerializeField] private float attackDelay = 0.3f;
    public Action OnFinishedAttack;
    private Transform player;
    [SerializeField] private int waves;

    [SerializeField] Animator aniamtor;
    private void Start()
    {
        player = GameManager.instance.GetPlayer().transform;
    }
    public void ExecuteAttack()
    {    
        StartCoroutine(FireSequence());      
    }

    IEnumerator FireSequence()
    {
        aniamtor.SetBool("Fire", true);
        int count = 0;
        while(count <= waves)
        {
            count++;
            if (player == null) { player = GameManager.instance.GetPlayer().transform; }
            foreach (var point in firePoints)
            {
                Vector2 direction = (player.position - point.position).normalized;
                projectileFactory.Fire(direction, point.position);
                yield return new WaitForSeconds(attackDelay); // Dispara uno tras otro, no todos a la vez
            }
        }
        aniamtor.SetBool("Fire", false);
        // Avisar al BossController que terminó el ataque (si usás evento)
        Debug.Log("Range attack finished");
        OnFinishedAttack?.Invoke();


    }
}
