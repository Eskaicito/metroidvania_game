using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerCombat : MonoBehaviour
{
    [Header("Combat Settings")]
    public float comboWindow = 0.5f;
    public int maxCombo = 3;
    public float attackRadius = 0.5f;
    public Transform attackPoint;
    public LayerMask enemyLayers;

    [Header("Damage Settings")]
    private float[] comboDamage = { 10f, 15f, 25f };  

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
    public CameraController cameraController;  

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
        
        if (Input.GetButtonDown("Fire1") && !isAttacking && !isHitstopActive)
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


        if (Input.GetKeyDown(KeyCode.Q) && skillWheel.skillActive != null)
        {
            skillWheel.skillActive.Use();
            StartCoroutine(TriggerHitstop());
            cameraController.ShakeCamera();
        }
    }

    private IEnumerator PerformAttack()
    {
        isAttacking = true;

      
        rb.velocity = new Vector2(0, rb.velocity.y);

       
        comboStep = currentCombo - 1;

        
        animator.SetTrigger("Attack" + currentCombo);

        AudioManager.instance.PlaySound("s" + currentCombo);

      
        yield return new WaitForSeconds(attackDurations[comboStep]);

        isAttacking = false;

        if (currentCombo >= maxCombo)
        {
            currentCombo = 0;
        }
    }

    public void OnAttackHit()
    {

        Collider2D[] hitEnemies = Physics2D.OverlapCircleAll(attackPoint.position, attackRadius, enemyLayers);

        foreach (Collider2D enemy in hitEnemies)
        {
            // Intentar detectar el script 'Enemy'
            Enemy groundEnemy = enemy.GetComponent<Enemy>();
            if (groundEnemy != null)
            {
                groundEnemy.TakeDamage(comboDamage[comboStep]);
            }

            // Intentar detectar el script 'EnemyAir'
            EnemyAir flyingEnemy = enemy.GetComponent<EnemyAir>();
            if (flyingEnemy != null)
            {
                flyingEnemy.TakeDamage(comboDamage[comboStep]);
            }

            AudioManager.instance.PlaySound("sword" + currentCombo);
            StartCoroutine(TriggerHitstop());
            cameraController.ShakeCamera();
        }
    }

    private IEnumerator TriggerHitstop()
    {
        isHitstopActive = true;

        Time.timeScale = 0f;

        yield return new WaitForSecondsRealtime(hitstopDuration);

       
        Time.timeScale = 1f;

        isHitstopActive = false;
    }

    private void OnDrawGizmosSelected()
    {
       
        if (attackPoint == null)
            return;

        Gizmos.DrawWireSphere(attackPoint.position, attackRadius);
    }
}
