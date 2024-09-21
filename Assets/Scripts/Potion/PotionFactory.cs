using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class PotionFactory : MonoBehaviour
{

    [SerializeField] private Potion[] potionsPrefabs;
    private Dictionary<string, Potion> idPotions;
   

    private void Awake()
    {
        idPotions = new Dictionary<string, Potion>();
        foreach (var potion in potionsPrefabs) 
        {
        
        idPotions.Add(potion.Id, potion);
        }
    }
    public Potion DropPotion(Vector3 position)
    {
        int randomDrop = Random.Range(0, potionsPrefabs.Length); 
        Potion selectedPotion = potionsPrefabs[randomDrop]; 

       
        Potion droppedPotion = Instantiate(selectedPotion, position, Quaternion.identity);

        return droppedPotion;
    }
}

