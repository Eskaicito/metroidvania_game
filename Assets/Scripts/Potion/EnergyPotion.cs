using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnergyPotion : Potion, ICollectible
{
    public int energyAmount = 30; 
    private Player playerEnergy;

    private void Start()
    {
        playerEnergy = FindAnyObjectByType<Player>();
    }
    public void Collect()
    {

        if (playerEnergy != null)
        {
           
            playerEnergy.RestoreEnergy(energyAmount);
            Destroy(gameObject);
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {
            AudioManager.instance.PlaySound("energy");
          Collect();
        }
    }
}
