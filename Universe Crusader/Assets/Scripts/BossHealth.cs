using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BossHealth : Sounds //MonoBehaviour
{
    public Animator animator;
    public float health = 20;
    //public GameObject deathEffect;

    public bool isInvulnerable = false;

    public void TakeDamage(float damage)
    {
        if (isInvulnerable)
            return;

        health -= damage;
        animator.SetTrigger("TakeDamage");
        if (health <= 0)
        {
            Die();
        }
    }

    void Die()
    {
        PlaySound(sounds[0]);
        animator.SetBool("IsDead",true);
        //Instantiate(deathEffect, transform.position, Quaternion.identity);

        Invoke("DestroyGameObject", 2);
    }

    void DestroyGameObject()
    {
        Destroy(gameObject);
    }


}
