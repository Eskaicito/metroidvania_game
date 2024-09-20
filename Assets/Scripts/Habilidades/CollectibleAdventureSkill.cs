using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CollectibleAdventureSkill : MonoBehaviour, ICollectible
{
    public string skillName; 

    
    public static Dictionary<string, bool> acquiredSkills = new Dictionary<string, bool>();


    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
        {
            
                Collect();
            
        }
    }

    
    public void Collect()
    {
        if (!acquiredSkills.ContainsKey(skillName))
        {
            acquiredSkills.Add(skillName, true);
            Debug.Log($"Habilidad {skillName} recogida.");
        }
        else
        {
            Debug.Log("Esta habilidad ya ha sido recogida.");
        }

        Destroy(gameObject); 
    }
}
