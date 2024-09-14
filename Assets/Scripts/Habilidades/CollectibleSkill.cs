using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CollectibleSkill : MonoBehaviour, ICollectible
{
    public string skillName;
    public MonoBehaviour skillScript; // Debe ser un MonoBehaviour para ser agregado en el Inspector
    public Sprite skillIcon;

    public void Collect()
    {
        // Verifica que el script implementa la interfaz ISkill
        if (skillScript is ISkill skill)
        {
            SkillWheel.Instance.AddSkill(skillName, skill, skillIcon);
            Destroy(gameObject);
        }
        else
        {
            Debug.LogError($"El script asignado a {gameObject.name} no implementa la interfaz ISkill.");
        }
    }
}
