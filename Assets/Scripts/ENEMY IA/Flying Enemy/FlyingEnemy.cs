using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FlyingEnemy : MonoBehaviour
{
    [SerializeField] private GameObject projectilePrefab;
    [SerializeField] private Transform firePoint;
    [SerializeField] private float fireRate;
    [SerializeField] private float nextFire;

    private void Update()
    {
        if (Time.time >= nextFire)
        {
            Shoot();
            nextFire = Time.time + fireRate;
        }
    }

    private void Shoot()
    {
        GameObject projectile = Instantiate(projectilePrefab, firePoint.position, firePoint.rotation);
        Rigidbody2D rb = projectile.GetComponent<Rigidbody2D>();

        rb.velocity = -transform.up * 10f;
    }
}
