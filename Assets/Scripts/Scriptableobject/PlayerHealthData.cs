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

    // Variable para saber si los valores ya fueron inicializados
    private bool initialized = false;

    // M�todo para inicializar la salud y la energ�a solo al inicio del juego
    public void InitializeValues()
    {
        // Solo resetear si no ha sido inicializado (primera vez)
        if (!initialized)
        {
            currentHealth = maxHealth;
            currentEnergy = maxEnergy;
            initialized = true; // Marcar como inicializado
        }
    }

    // M�todo para forzar el reset (por ejemplo, si se quiere reiniciar completamente el juego)
    public void ForceResetValues()
    {
        currentHealth = maxHealth;
        currentEnergy = maxEnergy;
        initialized = true; // Marcar como inicializado despu�s del reset
    }
}
