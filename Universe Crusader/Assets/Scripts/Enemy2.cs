using UnityEngine;

public class Enemy2 : MonoBehaviour
{
    public Transform player;
    private Player playerScript;
    public float damage = 10f;
    public float health = 50f; // Максимальное здоровье врага
    private float currentHealth; // Текущее здоровье врага
    public float attackRange = 2f; // Радиус атаки врага
    public bool isDead = false;
    void Update()   //Поворот спрайта
    {
        if (player != null)
        {
            Vector3 direction = player.position - transform.position;
            bool facingRight = direction.x < 0; // true если игрок слева, false если справа

            // Изменяем scale.x для поворота
            if (facingRight && transform.localScale.x < 0)
            {
                transform.localScale = new Vector3(-transform.localScale.x, transform.localScale.y, transform.localScale.z);
            }
            else if (!facingRight && transform.localScale.x > 0)
            {
                transform.localScale = new Vector3(-transform.localScale.x, transform.localScale.y, transform.localScale.z);
            }

        }
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Player") && !isDead)
        {
            Player playerScript = collision.gameObject.GetComponent<Player>(); // Получаем скрипт игрока
            if (playerScript != null)
            {
                playerScript.TakeDamage(damage); // Вызываем метод TakeDamage игрока
            }
        }
    }

    public void TakeDamage(float damageAmount)
    {
        health -= damageAmount;
        if (health <= 0 && !isDead)
        {
            Die();
        }
    }

    void Die()
    {
        isDead = true;
        Destroy(gameObject); // Удаление врага из сцены
    }
}

/*

using System.Collections;
using UnityEngine;

public class Enemy : MonoBehaviour
{
    public float moveSpeed = 3f; // Скорость движения врага
    public float attackRange = 2f; // Радиус атаки врага
    private Transform playerTransform; // Ссылка на трансформацию игрока
    private Player playerScript; // Ссылка на скрипт игрока
    
    // Определение границ области
    public float minX, maxX, minY, maxY;
    
    public float attackDamage = 10f; // Урон, который враг наносит игроку
    public float attackCooldown = 2f; // Время между атаками
    private float lastAttackTime = 0f; // Время последней атаки
    public float maxHealth = 50f; // Максимальное здоровье врага
    private float currentHealth; // Текущее здоровье врага
    
    public float visionRadius = 5f; // Радиус зрения врага
    private bool playerInSight = false; // Находится ли игрок в поле зрения

    private Animator animator;  // Ссылка на аниматор

    private void Start()
    {
        currentHealth = maxHealth;

        // Поиск объекта с тегом "Player"
        GameObject playerObject = GameObject.FindGameObjectWithTag("Player");
        if (playerObject != null)
        {
            playerTransform = playerObject.transform;
            playerScript = playerObject.GetComponent<Player>();
        }

        // Получаем аниматор
        animator = GetComponent<Animator>();

        // Добавление кругового коллайдера для зоны зрения
        CircleCollider2D visionCollider = gameObject.AddComponent<CircleCollider2D>();
        visionCollider.isTrigger = true;  // Устанавливаем коллайдер как триггер
        visionCollider.radius = visionRadius;  // Задаем радиус триггера для зрения
        
        // Убедимся, что у объекта есть Rigidbody2D
        if (gameObject.GetComponent<Rigidbody2D>() == null)
        {
            gameObject.AddComponent<Rigidbody2D>().isKinematic = true; // Rigidbody2D, который не участвует в физике
        }
    }

    private void Update()
    {
        // Если игрок в поле зрения и существует, двигаться к нему
        if (playerInSight && playerTransform != null)
        {
            MoveTowardsPlayer();
        }
        else
        {
            // Враг не видит игрока, переходит в Idle
            animator.SetBool("isMoving", false);
        }
    }

    private void MoveTowardsPlayer()
    {
        // Вычисление расстояния до игрока
        float distanceToPlayer = Vector2.Distance(transform.position, playerTransform.position);

        // Если игрок в пределах радиуса атаки, то атаковать
        if (distanceToPlayer <= attackRange && Time.time >= lastAttackTime + attackCooldown)
        {
            AttackPlayer();
            lastAttackTime = Time.time; // Обновляем время последней атаки
        }
        else
        {
            // Двигаться к игроку
            Vector2 direction = (playerTransform.position - transform.position).normalized;
            Vector2 newPosition = (Vector2)transform.position + direction * moveSpeed * Time.deltaTime;

            // Ограничение движения в области
            newPosition.x = Mathf.Clamp(newPosition.x, minX, maxX);
            newPosition.y = Mathf.Clamp(newPosition.y, minY, maxY);

            transform.position = newPosition;

            // Устанавливаем анимацию движения
            animator.SetBool("isMoving", true);

            // Поворот спрайта в сторону игрока
            FlipSprite(direction);
        }
    }

    private void AttackPlayer()
    {
        Debug.Log("Enemy attacks the player!");

        // Проверяем, есть ли у игрока компонент Player, чтобы нанести ему урон
        if (playerScript != null)
        {
            playerScript.TakeDamage(attackDamage); // Наносим урон игроку через метод TakeDamage
        }
    }

    public void TakeDamage(float damage)
    {
        currentHealth -= damage;
        Debug.Log("Enemy takes damage: " + damage);
        if (currentHealth <= 0)
        {
            Die();
        }
    }

    private void Die()
    {
        Debug.Log("Enemy died!");
        Destroy(gameObject);
    }

    // Поворот спрайта в сторону игрока
    private void FlipSprite(Vector2 direction)
    {
        if (direction.x < 0)
        {
            transform.localScale = new Vector3(1, 1, 1); // Поворот вправо
        }
        else if (direction.x > 0)
        {
            transform.localScale = new Vector3(-1, 1, 1); // Поворот влево
        }
    }

    // Этот метод вызывается, когда игрок входит в зону зрения врага
    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
        {
            playerInSight = true;
            Debug.Log("Player is in enemy's sight!");
        }
    }

    // Этот метод вызывается, когда игрок покидает зону зрения врага
    private void OnTriggerExit2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
        {
            playerInSight = false;
            Debug.Log("Player left enemy's sight!");
        }
    }
}
*/