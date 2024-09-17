using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnergyPotion : MonoBehaviour
{
    public int energyAmount = 10; // Cantidad de energía que restaura la poción

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {
            Player player = collision.GetComponent<Player>();
            if (player != null)
            {
                // Restaura energía al jugador
                player.RestoreEnergy(energyAmount);
                Debug.Log("Player restored by " + energyAmount + " energy. Current Energy: " + player.playerHealthData.currentEnergy);

                // Destruye la poción después de ser usada
                Destroy(gameObject);
            }
        }
    }
}
