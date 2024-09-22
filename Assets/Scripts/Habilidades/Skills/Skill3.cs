using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Skill3 : MonoBehaviour, ISkill
{
    private Player player;
    private int energyAmount = 70;
    [SerializeField] private KarmaSystem karmaSystem; 
    [SerializeField] private int requiredVenganzaPoints = 10;  

    private void Start()
    {
        player = FindAnyObjectByType<Player>();

        if (karmaSystem == null)
        {
            Debug.LogError("KarmaSystem no asignado en Skill3.");
        }
    }

    public void Activate()
    {
        if (karmaSystem != null && karmaSystem.venganzaPoints >= requiredVenganzaPoints)
        {
            Debug.Log("Skill3 Activated");
        }
        else
        {
            Debug.Log("No puedes usar esta habilidad, necesitas al menos " + requiredVenganzaPoints + " puntos de venganza.");
            Debug.Log("Puntos de Venganza actuales: " + karmaSystem.venganzaPoints);
        }
    }

    public void Use()
    {
        if (karmaSystem != null && karmaSystem.venganzaPoints >= requiredVenganzaPoints)
        {

            if (player.HasEnoughEnergy(energyAmount))
            {
                player.UseEnergy(energyAmount);
                Debug.Log("Skill3 Used");
            }
            else
            {
                Debug.Log("No puedes usar esta habilidad, no tienes suficiente energía.");
                Debug.Log("Energía actual: " + player.playerHealthData.currentEnergy);
            }
        }
        else
        {
            Debug.Log("No puedes usar esta habilidad, necesitas al menos " + requiredVenganzaPoints + " puntos de venganza.");
        }
    }
}

