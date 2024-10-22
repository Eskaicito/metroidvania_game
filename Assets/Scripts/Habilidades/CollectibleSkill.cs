using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CollectibleSkill : MonoBehaviour, ICollectible
{
    public string skillName;
    public MonoBehaviour skillScript;
    public Sprite skillIcon;
    public string skillDescription; 
    private SkillWheel skillWheel;
    private CollectibleSkillUI skillUIManager;

    private void Start()
    {
        skillWheel = FindAnyObjectByType<SkillWheel>();
        skillUIManager = FindObjectOfType<CollectibleSkillUI>(); 
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
        {
            ICollectible collectible = GetComponent<ICollectible>();
            if (collectible != null)
            {
                
                Collect();
            }
        }
    }

    public void Collect()
    {
        if (skillScript is ISkill skill)
        {
            skillWheel.AddSkill(skillName, skill, skillIcon);
            skillUIManager.ShowSkillPanel(skillIcon, skillDescription);
            Destroy(gameObject); 
        }
    }
}
