using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CollectibleAdventureSkill : MonoBehaviour, ICollectible
{
    public string skillName;
    public Sprite skillIcon;
    public string skillDescription;
    private CollectibleSkillUI skillUIManager;


    public static Dictionary<string, bool> acquiredSkills = new Dictionary<string, bool>();

    private void Start()
    {

        skillUIManager = FindObjectOfType<CollectibleSkillUI>();
    }
    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
        {
            AudioManager.instance.PlaySound("collect");
            Collect();
            
        }
    }

    
    public void Collect()
    {
        if (!acquiredSkills.ContainsKey(skillName))
        {
            skillUIManager.ShowSkillPanel(skillIcon, skillDescription);
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
