using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "PlayerHealthData", menuName = "ScriptableObjects/PlayerHealthData", order = 1)]
public class PlayerHealthData : ScriptableObject
{
    public int maxHealth = 50;
    public int currentHealth;

    public int maxEnergy = 100;
    public int currentEnergy;

   
    private bool initialized = false;

    
    public void InitializeValues()
    {
       
        if (!initialized)
        {
            currentHealth = maxHealth;
            currentEnergy = maxEnergy;
            initialized = true; 
        }
    }

    
    public void ForceResetValues()
    {
        currentHealth = maxHealth;
        currentEnergy = maxEnergy;
        initialized = true; 
    }
}
