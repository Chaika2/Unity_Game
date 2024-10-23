using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Player : MonoBehaviour
{
    public float speed;
    public float jumpForce;
    public int maxHealth = 100; // Максимальное здоровье
    public int currentHealth; // Текущее здоровье

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
        currentHealth = maxHealth; // Установить текущее здоровье в максимальное значение
    }

    private void Update()
    {
        // Обработка нажатия пробела для прыжка
        if (Input.GetKeyDown(KeyCode.Space) && isGrounded)
        {
            spacePress = true;
        }

        // Обработка нажатия кнопки удара (например, клавиша "F")
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
        // Движение игрока
        float horizontalInput = Input.GetAxis("Horizontal");
        Vector3 position = transform.position;
        position.x += horizontalInput * speed * Time.fixedDeltaTime; 
        transform.position = position;

        // Обработка поворота спрайта
        if (horizontalInput != 0)
        {
            spriteRenderer.flipX = horizontalInput < 0; // Поворот спрайта в зависимости от направления
        }
         if (!animator.GetCurrentAnimatorStateInfo(0).IsName("Attack"))
        {
            if (isGrounded)
            {
                animator.SetInteger("State", horizontalInput != 0 ? 1 : 0); // переходы между "Idle" и "Walk"
            }
            else
            {
                animator.SetInteger("State", 2); // "Jump"
            }
        }
        Jump();
}


    public void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Ground"))
        {
            isGrounded = true; // Игрок на земле
            animator.SetTrigger("Land"); // Активируйте триггер приземления
        }
    }

    private void OnCollisionExit2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Ground"))
        {
            isGrounded = false; // Игрок оторвался от земли
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

        // Используем 2D версию OverlapCircle
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
                    enemyScript.TakeDamage(5); // Наносим 10 единиц урона
                    Debug.Log("Hit an enemy!");
                }
                else
                {
                    Debug.Log("No Enemy script found!");
                }
            }   
        }
    }

    public void TakeDamage(int damage)
    {
        currentHealth -= damage; // Уменьшение текущего здоровья
        Debug.Log($"Player took damage: {damage}. Current health: {currentHealth}");

        if (currentHealth <= 0)
        {
            Die(); // Вызвать метод смерти, если здоровье упало до 0
        }
    }

    private void Die()
    {
        // Здесь напишите логику смерти игрока
        Debug.Log("Player has died!");

        // Отключение игрового объекта
        gameObject.SetActive(false);

        // Загрузка сцены меню (предполагаем, что сцена меню имеет индекс 0)
        SceneManager.LoadScene("Menu"); // Можно также использовать индекс: SceneManager.LoadScene(0);
    }
}