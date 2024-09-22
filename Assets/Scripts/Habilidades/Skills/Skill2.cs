using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Skill2 : MonoBehaviour, ISkill
{
    private Player player;
    private int energyAmount = 80; 
    [SerializeField] private KarmaSystem karmaSystem;
    [SerializeField] private int requiredRedencionPoints = 10;

    private void Start()
    {
        player = FindAnyObjectByType<Player>();

        if (karmaSystem == null)
        {
            Debug.LogError("KarmaSystem no asignado en Skill2.");
        }
    }

    public void Activate()
    {

        if (karmaSystem != null && karmaSystem.redencionPoints >= requiredRedencionPoints)
        {
            Debug.Log("Skill2 Activated");
        }
        else
        {
            Debug.Log("No puedes usar esta habilidad, necesitas al menos " + requiredRedencionPoints + " puntos de redención.");
            Debug.Log("Puntos de Redención actuales: " + karmaSystem.redencionPoints);
        }
    }

    public void Use()
    {
       
        if (karmaSystem != null && karmaSystem.redencionPoints >= requiredRedencionPoints)
        {
            if (player.HasEnoughEnergy(energyAmount))
            {
                player.UseEnergy(energyAmount);
                Debug.Log("Skill2 Used");
            }
            else
            {
                Debug.Log("No puedes usar esta habilidad, no tienes suficiente energía.");
                Debug.Log("Energía actual: " + player.playerHealthData.currentEnergy);
            }
        }
        else
        {
            Debug.Log("No puedes usar esta habilidad, necesitas al menos " + requiredRedencionPoints + " puntos de redención.");
        }
    }
}
