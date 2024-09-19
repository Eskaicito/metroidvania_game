using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;



public class EnergyBar : MonoBehaviour
{
    public Image energyBar;
    public PlayerHealthData playerHealthData;

    void Update()
    {
        energyBar.fillAmount = (float)playerHealthData.currentEnergy / playerHealthData.maxEnergy;
    }
}

