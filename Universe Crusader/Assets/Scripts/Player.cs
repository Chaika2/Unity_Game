using System;
using System.Collections;
using System.Collections.Generic;
using UnityEditor.SearchService;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Player : Sounds
{
    public float speed;
    public float jumpForce;
    public static float maxHealth = 100;
    public float currentHealth;
    public float CurrentHealth => currentHealth;

    public event Action<float> OnHealthChanged; // Событие изменения здоровья

    private bool spacePress;
    private bool isGrounded;
    private Rigidbody2D rb2;
    private SpriteRenderer spriteRenderer;
    private Animator animator;
    public float attackRange = 2f;

    private void Start()
    {
        rb2 = GetComponent<Rigidbody2D>();
        spriteRenderer = GetComponent<SpriteRenderer>();
        animator = GetComponent<Animator>();
        currentHealth = maxHealth;
    }

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.Space) && isGrounded)
        {
            spacePress = true;
            PlaySound(sounds[1]);
        }

        if (Input.GetKeyDown(KeyCode.F))
        {
            Attack();
        }
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            SceneManager.LoadScene(0);
        }
    }

    private void FixedUpdate()
    {
        float horizontalInput = Input.GetAxis("Horizontal");
        Vector3 position = transform.position;
        position.x += horizontalInput * speed * Time.fixedDeltaTime;
        transform.position = position;

        if (horizontalInput != 0)
        {
            spriteRenderer.flipX = horizontalInput < 0;
        }
        if (!animator.GetCurrentAnimatorStateInfo(0).IsName("Attack"))
        {
            if (isGrounded)
            {
                animator.SetInteger("State", horizontalInput != 0 ? 1 : 0);
            }
            else
            {
                animator.SetInteger("State", 2);
            }
        }
        Jump();
    }

    public void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Ground"))
        {
            isGrounded = true;
            PlaySound(sounds[0]);
            animator.SetTrigger("Land");
        }
    }

    private void OnCollisionExit2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Ground"))
        {
            isGrounded = false;
        }
    }

    private void Jump()
    {
        if (spacePress)
        {
            if (isGrounded)
            {
                isGrounded = false;
                rb2.AddForce(Vector2.up * jumpForce, ForceMode2D.Impulse);
                spacePress = false;
            }
        }
    }

    private void Attack()
    {
        if (!animator.GetCurrentAnimatorStateInfo(0).IsName("Attack"))
        {
            animator.SetTrigger("Attack");
            Debug.Log("Player attacks!");
            PlaySound(sounds[3]);
            Collider2D[] hitEnemies = Physics2D.OverlapCircleAll(transform.position, attackRange);
            
            if (hitEnemies.Length == 0)
            {
                Debug.Log("No enemies hit!");
            }

            foreach (Collider2D enemy in hitEnemies)
            {
                Enemy enemyScript = enemy.GetComponent<Enemy>();

                if (enemyScript != null)
                {
                    enemyScript.TakeDamage(5);
                    Debug.Log("Hit an enemy!");
                }
                else
                {
                    Debug.Log("No Enemy script found!");
                }
            }
            foreach (Collider2D boss in hitEnemies)
            {
                BossHealth bossScript = boss.GetComponent<BossHealth>();

                if (bossScript != null)
                {
                    bossScript.TakeDamage(5);
                    Debug.Log("Hit an enemy!");
                }
                else
                {
                    Debug.Log("No Enemy script found!");
                }
            }
        }
    }

   public void TakeDamage(float damage)
   {
        currentHealth -= damage;
        OnHealthChanged?.Invoke(currentHealth); // Вызываем событие при изменении здоровья
    
        Debug.Log($"Player took damage: {damage}. Current health: {currentHealth}");

        if (currentHealth <= 0)
        {
            Die();
        }
   }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.tag == "Danger")
        {
            SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);   //Перезапуск уровня при задевании ловушки
        }
    }

    private void Die()
    {
        Debug.Log("Player has died!");
        gameObject.SetActive(false);
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
    }
}
