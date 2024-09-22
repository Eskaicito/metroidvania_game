using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Skill1 : MonoBehaviour, ISkill
{
    private Player player;
    private int energyAmount = 60;
    private int damage = 30;
    private PlayerCombat combat;
    public CameraController cameraController;

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
        if (player.HasEnoughEnergy(energyAmount))
        {
            SkillDamage();
            player.UseEnergy(energyAmount);
            Debug.Log("Skill1 Used");
        }
        else
        {
            Debug.Log("No puedes usar esta habilidad, no tienes suficiente energía.");
            Debug.Log("Energía actual: " + player.playerHealthData.currentEnergy);
        }
   

    }

    private void SkillDamage()
    {

        Collider2D[] hitEnemies = Physics2D.OverlapCircleAll(combat.attackPoint.position, combat.attackRadius, combat.enemyLayers);

        if (hitEnemies.Length == 0)
        {
            Debug.Log("No se golpeó a ningún enemigo.");
        }

        foreach (Collider2D enemy in hitEnemies)
        {
            // Intentar detectar el script 'Enemy'
            Enemy groundEnemy = enemy.GetComponent<Enemy>();
            if (groundEnemy != null)
            {
                groundEnemy.TakeDamage(damage);
            }

            // Intentar detectar el script 'EnemyAir'
            EnemyAir flyingEnemy = enemy.GetComponent<EnemyAir>();
            if (flyingEnemy != null)
            {
                flyingEnemy.TakeDamage(damage);
            }

            AudioManager.instance.PlaySound("skill1");
            StartCoroutine(combat.TriggerHitstop());
            cameraController.ShakeCamera();
        }
      
    }
}
