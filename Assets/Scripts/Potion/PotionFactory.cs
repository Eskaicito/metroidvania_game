using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class PotionFactory : MonoBehaviour
{
    // Referencias a los prefabs de las pociones
    [SerializeField] private GameObject healthPotionPrefab;
    [SerializeField] private GameObject energyPotionPrefab;

    // M�todo que decide qu� poci�n crear (salud o energ�a) y la instancia en la escena
    public void DropPotion(Vector3 position)
    {
        int randomDrop = Random.Range(0, 2); // 0 para salud, 1 para energ�a

        GameObject potionToDrop = null;

        if (randomDrop == 0)
        {
            potionToDrop = healthPotionPrefab;
            Debug.Log("Factory is dropping a Health Potion");
        }
        else
        {
            potionToDrop = energyPotionPrefab;
            Debug.Log("Factory is dropping an Energy Potion");
        }

        // Instanciamos la poci�n en la posici�n dada
        if (potionToDrop != null)
        {
            Instantiate(potionToDrop, position, Quaternion.identity);
        }
    }
}

