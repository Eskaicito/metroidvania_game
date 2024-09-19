using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CollectibleSkill : MonoBehaviour, ICollectible
{
    public string skillName;
    public MonoBehaviour skillScript; 
    public Sprite skillIcon;
    private SkillWheel skillWheel;

    private void Start()
    {
        skillWheel = FindAnyObjectByType<SkillWheel>();
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
            Destroy(gameObject);
        }
    }
}
