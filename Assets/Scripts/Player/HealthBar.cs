using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class HealthBar : MonoBehaviour
{
    public Image healthBar;

    public float ActualHealth;
    public float HealthMax;

    void Update()
    {
        healthBar.fillAmount = ActualHealth / HealthMax;

    }
}
