using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
    public Player player; // Referencia al jugador
    public GameObject victoryCollider; // Collider que representa el área de victoria

    private void Update()
    {
        CheckPlayerHealth();
    }

    // Verificar si el jugador ha perdido
    private void CheckPlayerHealth()
    {
        if (player.playerHealthData.currentHealth <= 0)
        {
            HandleDefeat();
        }
    }

    // Llamado cuando el jugador toca el collider de victoria
    public void HandleVictory()
    {
        Debug.Log("¡Victoria!");
        // Aquí puedes cargar una pantalla de victoria o hacer la transición a otro nivel
        // Por ejemplo, cargamos una escena de victoria:
        SceneManager.LoadScene("VictoryScene");
    }

    // Llamado cuando el jugador pierde toda su vida
    private void HandleDefeat()
    {
        Debug.Log("Has sido derrotado.");
        // Cargar una pantalla de derrota o reiniciar el nivel
        SceneManager.LoadScene("DefeatScene");
    }

    // Método que será llamado cuando el jugador colisione con el collider de victoria
    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.gameObject.CompareTag("Player"))
        {
            Debug.Log("¡Jugador entró en la zona de victoria!");
            HandleVictory();
        }
    }


}
