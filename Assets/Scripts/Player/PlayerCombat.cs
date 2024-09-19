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

    [Header("Hitstop Settings")]
    public float hitstopDuration = 0.1f;
    private bool isHitstopActive = false;

    [Header("Camera Shake Settings")]
    public CameraController cameraController;  // Referencia al script de la cámara

    private Rigidbody2D rb;
    private SpriteRenderer spriteRenderer;
    private SkillWheel skillWheel;

    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        spriteRenderer = GetComponent<SpriteRenderer>();
        skillWheel = FindAnyObjectByType<SkillWheel>();
    }

    void Update()
    {
        // Detectar entrada de ataque
        if (Input.GetButtonDown("Fire1") && !isAttacking && !isHitstopActive)
        {
            // Continuar combo si está en ventana de tiempo
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

        // Detectar habilidad
        if (Input.GetKeyDown(KeyCode.Z) && skillWheel.skillActive != null)
        {
            skillWheel.skillActive.Use();
        }
    }

    private IEnumerator PerformAttack()
    {
        isAttacking = true;

        // Detener al jugador
        rb.velocity = new Vector2(0, rb.velocity.y);

        // Determinar paso del combo
        comboStep = currentCombo - 1;

        // Lanzar la animación de ataque correspondiente
        animator.SetTrigger("Attack" + currentCombo);

        // Esperar hasta que termine la animación actual
        yield return new WaitForSeconds(attackDurations[comboStep]);

        isAttacking = false;

        if (currentCombo >= maxCombo)
        {
            currentCombo = 0;
        }
    }

    public void OnAttackHit()
    {
        // Detectar enemigos golpeados
        Collider2D[] hitEnemies = Physics2D.OverlapCircleAll(attackPoint.position, attackRadius, enemyLayers);

        foreach (Collider2D enemy in hitEnemies)
        {
            enemy.GetComponent<Enemy>().TakeDamage(attackDamage);
            StartCoroutine(TriggerHitstop());

            // Activar el "camera shake" al golpear
            cameraController.ShakeCamera();
        }
    }

    private IEnumerator TriggerHitstop()
    {
        isHitstopActive = true;

        // Pausar el tiempo
        Time.timeScale = 0f;

        yield return new WaitForSecondsRealtime(hitstopDuration);

        // Reanudar el tiempo
        Time.timeScale = 1f;

        isHitstopActive = false;
    }

    private void OnDrawGizmosSelected()
    {
        // Dibujar el área del ataque
        if (attackPoint == null)
            return;

        Gizmos.DrawWireSphere(attackPoint.position, attackRadius);
    }
}
