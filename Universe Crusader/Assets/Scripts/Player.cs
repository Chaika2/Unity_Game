using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{
    public float speed;
    public float jumpForce;

    private bool spacePress;
    private bool isGrounded;
    private Rigidbody2D rb2;
    private SpriteRenderer spriteRenderer;
    private Animator animator;

    private void Start()
    {
        rb2 = GetComponent<Rigidbody2D>();
        spriteRenderer = GetComponent<SpriteRenderer>();
        animator = GetComponent<Animator>();
    }

    private void Update()
    {
        // Обработка нажатия пробела
        if (Input.GetKeyDown(KeyCode.Space)) // Срабатывает только при нажатии
        {
            spacePress = true;
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

        Jump(); // Вызов метода прыжка

        // Установка состояния анимации
        if (isGrounded)
        {
            // Если игрок на земле
            animator.SetInteger("State", horizontalInput != 0 ? 1 : 0); // 1 - Бег, 0 - Стояние
        }
        else
        {
            animator.SetInteger("State", 2); // 2 - Прыжок
        }
    }

    private void Jump()
    {
        if (spacePress)
        {
            if (isGrounded)
            {
                isGrounded = false;
                spacePress = false;
                rb2.AddForce(Vector2.up * jumpForce, ForceMode2D.Impulse);
            }
        }
    }

    public void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Ground"))
        {
            isGrounded = true; // Игрок на земле
            spacePress = false; // Сбрасываем нажатие пробела
        }
    }
}