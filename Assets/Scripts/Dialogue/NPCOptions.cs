using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class NPCOptions : MonoBehaviour
{
    [SerializeField] DialogueController dialogueController;
    [SerializeField] string npcDialogue;
    [SerializeField] GameObject interactIcon;
    [SerializeField] KarmaSystem karmaSystem;  // Sistema de karma
    private bool isPlayerInRange = false;

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {
            interactIcon.SetActive(true);
            isPlayerInRange = true;
        }
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {
            interactIcon.SetActive(false);
            isPlayerInRange = false;
        }
    }

    private void Start()
    {
        interactIcon.SetActive(false);
    }

    private void Update()
    {
        if (isPlayerInRange && Input.GetKeyDown(KeyCode.F))
        {
            Interact();
        }
    }

    public void Interact()
    {
        // Inicia el diálogo con dos opciones y destruye el NPC después de la elección
        dialogueController.StartDialogueWithOptions(
            npcDialogue,
            () => {
                Debug.Log("Opción A elegida");
                karmaSystem.AddRedencionPoints(10);  // Aumenta redención
                Destroy(gameObject);
            },  // Acción para la opción A
            () => {
                Debug.Log("Opción B elegida");
                karmaSystem.AddVenganzaPoints(10);   // Aumenta venganza
                Destroy(gameObject);
            }   // Acción para la opción B
        );
    }
}

