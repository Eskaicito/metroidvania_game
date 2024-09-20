using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Projectile : MonoBehaviour
{
    private Player player;
    private int damageAmount = 10;


    private void Start()
    {
       player = GetComponent<Player>();
    }
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision != null)
        {
            if(collision.CompareTag("Player"))
            {
                player.TakeDamage(damageAmount);        
            }
        }

    }

    private void Update()
    {
        Destroy(gameObject, 1.5f);
    }
}
