using UnityEngine;

public class NPCOptions : MonoBehaviour
{
    [SerializeField] DialogueController dialogueController;
    [SerializeField] KarmaSystem karmaSystem;  // Sistema de karma
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
        // Inicia el di�logo con dos opciones y ajusta el karma
        dialogueController.StartDialogueWithOptions(
            npcDialogue,
            () => {
                Debug.Log("Opci�n A elegida");
                karmaSystem.AddRedencionPoints(10);  // Aumenta redenci�n
                Destroy(gameObject);
            },  // Acci�n para la opci�n A
            () => {
                Debug.Log("Opci�n B elegida");
                karmaSystem.AddVenganzaPoints(10);   // Aumenta venganza
                Destroy(gameObject);
            }   // Acci�n para la opci�n B
        );
    }
}
