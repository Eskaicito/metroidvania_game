using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CollectibleSkill : MonoBehaviour, ICollectible
{
    public string skillName;
    public SkillBase skillScript;
    public Sprite skillIcon;

    public void Collect()
    {
    
        SkillWheel.Instance.AddSkill(skillName, skillScript, skillIcon);
        Destroy(gameObject); 
    }
}
