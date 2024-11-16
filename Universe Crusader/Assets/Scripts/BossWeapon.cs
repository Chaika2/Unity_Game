using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class BossAttack : MonoBehaviour
{
    public float AttackDamage = 20;
    public Vector3 attackOffset;
    public float attackRange = 1f;
    public LayerMask attackMask;
    private Animator anim;
    Transform player;
        public void OnEnemyAttack()
        {
            Vector3 pos = transform.position;
            pos += transform.right * attackOffset.x;
            pos += transform.up * attackOffset.y;

            Collider2D colInfo = Physics2D.OverlapCircle(pos, attackRange, attackMask);
   
            if (colInfo != null)
            {
                colInfo.GetComponent<Player>().TakeDamage(AttackDamage);
            }
        }

    void OnDrawGizmosSelected()
    {
        Vector3 pos = transform.position;
        pos += transform.right * attackOffset.x;
        pos += transform.up * attackOffset.y;
        Gizmos.DrawWireSphere(pos, attackRange);
    }
}
    
  