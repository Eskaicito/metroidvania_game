using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;


public class HealthBar : MonoBehaviour
{
    public Image healthBar;
    public PlayerHealthData playerHealthData;

    void Update()
    {
        healthBar.fillAmount = (float)playerHealthData.currentHealth / playerHealthData.maxHealth;
    }
}
