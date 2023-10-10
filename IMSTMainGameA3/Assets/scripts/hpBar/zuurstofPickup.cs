using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class zuurstofPickup : MonoBehaviour
{
    public int maxHealth = 100;
    public int currentHealth;
    public hpBar hpBar;
    

    void Start()
    {
        currentHealth = maxHealth; // Stel currentHealth in op de maximale gezondheid
        hpBar.setMaxHealth(maxHealth);
    }

  public void zuurstofPickupObject()
{
    PlayerHP playerHP = FindObjectOfType<PlayerHP>(); // Vind de spelergezondheidsscript
    if (playerHP != null)
    {
        playerHP.RestoreHealthToMax(); // Herstel de spelergezondheid naar 100%
    }

    // Reset de schade timer in het PlayerHP-script
    playerHP.damageTimer = 0.1f; // Stel de timer in op 0.1 seconden (1 frame)
}
}
