using System.Collections;
using System.Collections.Generic;
using UnityEngine;



[CreateAssetMenu(fileName = "PlayerHealthData", menuName = "ScriptableObjects/PlayerHealthData", order = 1)]
public class PlayerHealthData : ScriptableObject
{
    public int maxHealth = 50;
    public int currentHealth;

    // Variables para la energía
    public int maxEnergy = 100;
    public int currentEnergy;
}

