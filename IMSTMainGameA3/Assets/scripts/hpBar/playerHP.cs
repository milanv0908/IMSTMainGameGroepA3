using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerHP : MonoBehaviour
{
    public int maxHealth = 100;
    public int currentHealth;
    public hpBar hpBar;
    private float damageTimer = 0.1f; // Wacht 1 seconde tussen schade

    void Start()
    {
        currentHealth = maxHealth;
        hpBar.setMaxHealth(maxHealth);
    }

    void Update()
    {
        // Verminder de timer met de verstreken tijd sinds de vorige frame
        damageTimer -= Time.deltaTime;

        // Als de timer is verstreken, haal 1 HP af en reset de timer
        if (damageTimer <= 0)
        {
            TakeDamage(1);
            damageTimer = 0.1f; // Reset de timer naar 1 seconde
        }
    }

    void TakeDamage(int damage)
    {
        currentHealth -= damage;
        hpBar.setHealth(currentHealth);
    }
}
