using System.Collections;
using UnityEngine;
using UnityEngine.UI;

public class UI : MonoBehaviour
{
    public Image hpBar;
    public Player player; // Ссылка на объект игрока

    // Start is called before the first frame update
    void Start()
    {
        if (player == null)
        {
            player = FindObjectOfType<Player>(); // Поиск объекта игрока, если ссылка не задана
        }
    }

    // Update is called once per frame
    void Update()
    {
        // Обновление шкалы здоровья каждый кадр через свойство CurrentHealth
        hpBar.fillAmount = player.CurrentHealth / Player.maxHealth;
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Enemy"))
        {
            Enemy enemy = collision.gameObject.GetComponent<Enemy>();
            if (enemy != null)
            {
                player.TakeDamage(enemy.attackDamage); // Используем метод TakeDamage игрока
            }
        }
    }
}