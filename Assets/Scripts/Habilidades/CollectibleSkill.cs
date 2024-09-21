using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CollectibleSkill : MonoBehaviour, ICollectible
{
    public string skillName;
    public MonoBehaviour skillScript;
    public Sprite skillIcon;
    public string skillDescription; // Descripción que aparecerá en la UI
    private SkillWheel skillWheel;
    private CollectibleSkillUI skillUIManager;

    private void Start()
    {
        skillWheel = FindAnyObjectByType<SkillWheel>();
        skillUIManager = FindObjectOfType<CollectibleSkillUI>(); // Encontramos el script de la UI
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
            skillUIManager.ShowSkillPanel(skillIcon, skillDescription); // Mostrar el mensaje en la UI
            Destroy(gameObject); // Destruir el objeto luego de recogerlo
        }
    }
}
