using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnergyPotion : MonoBehaviour
{
    public int energyAmount = 10; // Cantidad de energ�a que restaura la poci�n

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {
            Player player = collision.GetComponent<Player>();
            if (player != null)
            {
                // Restaura energ�a al jugador
                player.RestoreEnergy(energyAmount);
                Debug.Log("Player restored by " + energyAmount + " energy. Current Energy: " + player.playerHealthData.currentEnergy);

                // Destruye la poci�n despu�s de ser usada
                Destroy(gameObject);
            }
        }
    }
}
