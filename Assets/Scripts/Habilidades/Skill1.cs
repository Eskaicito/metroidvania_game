using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Skill1 : MonoBehaviour, ISkill
{
    private Player player;
    private int energyAmount = 10;
    private int damage = 30;
    private PlayerCombat combat;

    private void Start()
    {
        player = FindAnyObjectByType<Player>();
        combat = FindAnyObjectByType<PlayerCombat>();
    }

    public void Activate()
    {
        Debug.Log("Skill1 Activated");
    }

    public void Use()
    {
        SkillDamage();
        player.UseEnergy(energyAmount);
        Debug.Log("Skill1 Used");
    }

    private void SkillDamage()
    {
        Collider2D[] hitEnemies = Physics2D.OverlapCircleAll(combat.attackPoint.position, combat.attackRadius, combat.enemyLayers);

        foreach (Collider2D enemy in hitEnemies)
        {
            enemy.GetComponent<Enemy>().TakeDamage(damage);
        }
    }
}
