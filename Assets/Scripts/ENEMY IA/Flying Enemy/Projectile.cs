using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Projectile : MonoBehaviour
{
    private Transform player;
    private int damageAmount = 10;

    private void Start()
    {
        player = GameObject.FindWithTag("Player").transform;
    }
    private void OnTriggerEnter2D(Collider2D collision)
    {
       if(collision.CompareTag("Player")) {
            AttackPlayer();
        }

    }

    private void Update()
    {
        Destroy(gameObject, 1.5f);
    }

    private void AttackPlayer()
    {
        if (player != null)
        {
            Player playerHealth = player.GetComponent<Player>();
            if (playerHealth != null)
            {
                AudioManager.instance.PlaySound("hurt");
                playerHealth.TakeDamage((int)damageAmount);

            }
        }
    }
}
