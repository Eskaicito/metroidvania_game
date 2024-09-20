using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NPCSimple : MonoBehaviour
{
    [SerializeField] DialogueController dialogueController;
    [SerializeField] string npcDialogue;
    [SerializeField] GameObject interactIcon;
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
        // Inicia un diálogo simple sin opciones
        dialogueController.StartSimpleDialogue(npcDialogue);
    }
}
