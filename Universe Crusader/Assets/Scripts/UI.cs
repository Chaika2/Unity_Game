using System.Collections;
using UnityEngine;
using UnityEngine.UI;

public class UI : MonoBehaviour
{
    public Image hpBar;
    public Player player;

    void Start()
    {
        if (player == null)
        {
            player = FindObjectOfType<Player>();
        }

        // Подписка на событие изменения здоровья
        player.OnHealthChanged += UpdateHealthBar;
        
        // Устанавливаем начальное значение шкалы здоровья
        hpBar.fillAmount = player.CurrentHealth / Player.maxHealth;
    }

    private void UpdateHealthBar(float currentHealth)
    {
        hpBar.fillAmount = currentHealth / Player.maxHealth;
    }

    private void OnDestroy()
    {
        // Отписка от события
        if (player != null)
        {
            player.OnHealthChanged -= UpdateHealthBar;
        }
    }
}