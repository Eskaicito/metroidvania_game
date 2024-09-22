using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ParallaxMat : MonoBehaviour
{
    [SerializeField] private Vector2 ParallaxMovementSpeed;
    private Vector2 offset;
    private Material material;
    private Rigidbody2D rbPlayer;

    private void Awake()
    {
        material = GetComponent<SpriteRenderer>().material;
        rbPlayer = GameObject.FindGameObjectWithTag("Player").GetComponent<Rigidbody2D>();
    }

    private void Update()
    {
        offset = (rbPlayer.velocity.x * 0.1f) * ParallaxMovementSpeed * Time.deltaTime;
        material.mainTextureOffset += offset;
    }
}
