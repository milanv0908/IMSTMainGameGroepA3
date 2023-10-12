using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerHP : MonoBehaviour
{
    public int maxHealth = 100;
    public int currentHealth;
    public hpBar hpBar;
    public float damageTimer = 0.5f; // Wacht x seconde tussen schade

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
            damageTimer = 0.5f; // Reset de timer naar x seconde
        }

        if(currentHealth == 0){
            Debug.Log("Game Over");
            currentHealth = 0;
            hpBar.setHealth(currentHealth);
        }
    }

    void TakeDamage(int damage)
    {
        currentHealth -= damage;
        hpBar.setHealth(currentHealth);
    }

    public void RestoreHealthToMax()
{
    currentHealth = maxHealth;
    hpBar.setHealth(currentHealth);
}
}
