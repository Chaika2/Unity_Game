
using System.Collections;
using UnityEngine;

public class Enemy : MonoBehaviour
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