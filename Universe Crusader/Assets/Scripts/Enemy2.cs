using UnityEngine;

public class Enemy2 : MonoBehaviour
{
    public Transform player;
    private Player playerScript;
    public float damage = 10f;
    public float health = 50f; // ������������ �������� �����
    private float currentHealth; // ������� �������� �����
    public float attackRange = 2f; // ������ ����� �����
    public bool isDead = false;
    void Update()   //������� �������
    {
        if (player != null)
        {
            Vector3 direction = player.position - transform.position;
            bool facingRight = direction.x < 0; // true ���� ����� �����, false ���� ������

            // �������� scale.x ��� ��������
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
            Player playerScript = collision.gameObject.GetComponent<Player>(); // �������� ������ ������
            if (playerScript != null)
            {
                playerScript.TakeDamage(damage); // �������� ����� TakeDamage ������
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
        Destroy(gameObject); // �������� ����� �� �����
    }
}

/*

using System.Collections;
using UnityEngine;

public class Enemy : MonoBehaviour
{
    public float moveSpeed = 3f; // �������� �������� �����
    public float attackRange = 2f; // ������ ����� �����
    private Transform playerTransform; // ������ �� ������������� ������
    private Player playerScript; // ������ �� ������ ������
    
    // ����������� ������ �������
    public float minX, maxX, minY, maxY;
    
    public float attackDamage = 10f; // ����, ������� ���� ������� ������
    public float attackCooldown = 2f; // ����� ����� �������
    private float lastAttackTime = 0f; // ����� ��������� �����
    public float maxHealth = 50f; // ������������ �������� �����
    private float currentHealth; // ������� �������� �����
    
    public float visionRadius = 5f; // ������ ������ �����
    private bool playerInSight = false; // ��������� �� ����� � ���� ������

    private Animator animator;  // ������ �� ��������

    private void Start()
    {
        currentHealth = maxHealth;

        // ����� ������� � ����� "Player"
        GameObject playerObject = GameObject.FindGameObjectWithTag("Player");
        if (playerObject != null)
        {
            playerTransform = playerObject.transform;
            playerScript = playerObject.GetComponent<Player>();
        }

        // �������� ��������
        animator = GetComponent<Animator>();

        // ���������� ��������� ���������� ��� ���� ������
        CircleCollider2D visionCollider = gameObject.AddComponent<CircleCollider2D>();
        visionCollider.isTrigger = true;  // ������������� ��������� ��� �������
        visionCollider.radius = visionRadius;  // ������ ������ �������� ��� ������
        
        // ��������, ��� � ������� ���� Rigidbody2D
        if (gameObject.GetComponent<Rigidbody2D>() == null)
        {
            gameObject.AddComponent<Rigidbody2D>().isKinematic = true; // Rigidbody2D, ������� �� ��������� � ������
        }
    }

    private void Update()
    {
        // ���� ����� � ���� ������ � ����������, ��������� � ����
        if (playerInSight && playerTransform != null)
        {
            MoveTowardsPlayer();
        }
        else
        {
            // ���� �� ����� ������, ��������� � Idle
            animator.SetBool("isMoving", false);
        }
    }

    private void MoveTowardsPlayer()
    {
        // ���������� ���������� �� ������
        float distanceToPlayer = Vector2.Distance(transform.position, playerTransform.position);

        // ���� ����� � �������� ������� �����, �� ���������
        if (distanceToPlayer <= attackRange && Time.time >= lastAttackTime + attackCooldown)
        {
            AttackPlayer();
            lastAttackTime = Time.time; // ��������� ����� ��������� �����
        }
        else
        {
            // ��������� � ������
            Vector2 direction = (playerTransform.position - transform.position).normalized;
            Vector2 newPosition = (Vector2)transform.position + direction * moveSpeed * Time.deltaTime;

            // ����������� �������� � �������
            newPosition.x = Mathf.Clamp(newPosition.x, minX, maxX);
            newPosition.y = Mathf.Clamp(newPosition.y, minY, maxY);

            transform.position = newPosition;

            // ������������� �������� ��������
            animator.SetBool("isMoving", true);

            // ������� ������� � ������� ������
            FlipSprite(direction);
        }
    }

    private void AttackPlayer()
    {
        Debug.Log("Enemy attacks the player!");

        // ���������, ���� �� � ������ ��������� Player, ����� ������� ��� ����
        if (playerScript != null)
        {
            playerScript.TakeDamage(attackDamage); // ������� ���� ������ ����� ����� TakeDamage
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

    // ������� ������� � ������� ������
    private void FlipSprite(Vector2 direction)
    {
        if (direction.x < 0)
        {
            transform.localScale = new Vector3(1, 1, 1); // ������� ������
        }
        else if (direction.x > 0)
        {
            transform.localScale = new Vector3(-1, 1, 1); // ������� �����
        }
    }

    // ���� ����� ����������, ����� ����� ������ � ���� ������ �����
    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
        {
            playerInSight = true;
            Debug.Log("Player is in enemy's sight!");
        }
    }

    // ���� ����� ����������, ����� ����� �������� ���� ������ �����
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