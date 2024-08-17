using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerCombat : MonoBehaviour
{
    [Header("Combat Settings")]
public float attackDamage = 10f;
public float comboWindow = 0.5f;
public int maxCombo = 3;
public float attackRadius = 0.5f;
public Transform attackPoint;
public LayerMask enemyLayers;

[Header("Animation Settings")]
public Animator animator;  
private int comboStep = 0;  

[Header("Attack Timing")]
public float[] attackDurations = { 0.3f, 0.35f, 0.4f };  

private int currentCombo = 0;
private float lastAttackTime = 0;
private bool isAttacking = false;

private Rigidbody2D rb;
private SpriteRenderer spriteRenderer;
private PlayerMovement playerMovement;

void Start()
{
    rb = GetComponent<Rigidbody2D>();
    spriteRenderer = GetComponent<SpriteRenderer>();
    playerMovement = GetComponent<PlayerMovement>();
}

void Update()
{
    // Comenzar el ataque cuando se presiona el botón de ataque
    if (Input.GetButtonDown("Fire1") && !isAttacking)
    {
        if (Time.time - lastAttackTime <= comboWindow && currentCombo < maxCombo)
        {
            currentCombo++;
        }
        else
        {
            currentCombo = 1;
        }

        lastAttackTime = Time.time;
        StartCoroutine(PerformAttack());
    }
}

private IEnumerator PerformAttack()
{
    isAttacking = true;

    // Detener el movimiento durante el ataque
    rb.velocity = new Vector2(0, rb.velocity.y);

    // Determinar el ataque del combo actual
    comboStep = currentCombo - 1;

    // Ejecutar el ataque
    DoAttack();

    // Asignar la animación correcta
    animator.SetTrigger("Attack" + currentCombo);

    // Esperar la duración del ataque antes de permitir el movimiento de nuevo
    yield return new WaitForSeconds(attackDurations[comboStep]);

    isAttacking = false;

    if (currentCombo >= maxCombo)
    {
        currentCombo = 0;
    }
}

void DoAttack()
{
    Vector2 attackDirection = spriteRenderer.flipX ? Vector2.left : Vector2.right;
    Collider2D[] hitEnemies = Physics2D.OverlapCircleAll(attackPoint.position, attackRadius, enemyLayers);

    foreach (Collider2D enemy in hitEnemies)
    {
        enemy.GetComponent<Enemy>().TakeDamage(attackDamage);
    }
}
}
