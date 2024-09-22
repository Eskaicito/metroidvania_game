using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
    
    public static GameManager Instance { get; private set; }

    public Player player; 
    public GameObject victoryCollider; 

    
    private void Awake()
    {
        
        if (Instance != null && Instance != this)
        {
            Destroy(gameObject);
            return;
        }

        
        Instance = this;

        
        DontDestroyOnLoad(gameObject);
    }

    private void Update()
    {
        CheckPlayerHealth();
    }

    
    private void CheckPlayerHealth()
    {
        if (player.playerHealthData.currentHealth <= 0)
        {
            HandleDefeat();
        }
    }

    
    public void HandleVictory()
    {
        Debug.Log("¡Victoria!");
        
        SceneManager.LoadScene("VictoryScene");
    }

    
    private void HandleDefeat()
    {
        Debug.Log("Has sido derrotado.");
        
        SceneManager.LoadScene("DefeatScene");
    }

    
    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.gameObject.CompareTag("Player"))
        {
            Debug.Log("¡Jugador entró en la zona de victoria!");
            HandleVictory();
        }
    }
}
