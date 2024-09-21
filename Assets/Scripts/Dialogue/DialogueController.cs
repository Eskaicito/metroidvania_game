using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class DialogueController : MonoBehaviour
{
    [SerializeField] TextMeshProUGUI dialogueText;    // Texto de diálogo
    [SerializeField] GameObject dialogueUI;           // Objeto que contiene el diálogo
    [SerializeField] Button optionAButton;            // Botón para la opción A
    [SerializeField] Button optionBButton;            // Botón para la opción B

    private System.Action onOptionA;                  // Acción que se ejecuta al elegir la opción A
    private System.Action onOptionB;                  // Acción que se ejecuta al elegir la opción B

    private void Start()
    {
        dialogueUI.SetActive(false);
        optionAButton.gameObject.SetActive(false);
        optionBButton.gameObject.SetActive(false);

        optionAButton.onClick.AddListener(OptionAChosen);
        optionBButton.onClick.AddListener(OptionBChosen);
    }

    // Mostrar el diálogo con dos opciones, ahora con efecto de tipeo
    public void StartDialogueWithOptions(string dialogue, System.Action optionAAction, System.Action optionBAction)
    {
        dialogueUI.SetActive(true);
        onOptionA = optionAAction;
        onOptionB = optionBAction;
        optionAButton.gameObject.SetActive(false);
        optionBButton.gameObject.SetActive(false);

        StartCoroutine(TypeDialogue(dialogue, true)); // Llama a la corutina para el efecto de tipeo
    }

    // Mostrar el diálogo sin opciones (simplemente lo cierra al final), también con efecto de tipeo
    public void StartSimpleDialogue(string dialogue)
    {
        dialogueUI.SetActive(true);
        optionAButton.gameObject.SetActive(false);
        optionBButton.gameObject.SetActive(false);

        StartCoroutine(TypeDialogue(dialogue, false)); // Llama a la corutina para el efecto de tipeo
    }

    // Corutina para mostrar el texto carácter por carácter
    private IEnumerator TypeDialogue(string dialogue, bool hasOptions)
    {
        dialogueText.text = "";
        foreach (char letter in dialogue.ToCharArray())
        {
            dialogueText.text += letter;
            yield return new WaitForSeconds(0.05f); // Ajusta la velocidad del tipeo aquí
        }

        if (hasOptions)
        {
            // Mostrar los botones de opciones solo cuando el texto haya terminado de escribirse
            optionAButton.gameObject.SetActive(true);
            optionBButton.gameObject.SetActive(true);
        }
        else
        {
            StartCoroutine(CloseAfterDelay(2f)); // Cierra el diálogo después de un pequeño retraso
        }
    }

    // Cerrar el diálogo después de un retraso
    private IEnumerator CloseAfterDelay(float delay)
    {
        yield return new WaitForSeconds(delay);
        EndDialogue();
    }

    // Opción A elegida
    private void OptionAChosen()
    {
        Debug.Log("Elegí la opción A");
        onOptionA?.Invoke();  // Ejecuta la acción de la opción A
        EndDialogue();
    }

    // Opción B elegida
    private void OptionBChosen()
    {
        Debug.Log("Elegí la opción B");
        onOptionB?.Invoke();  // Ejecuta la acción de la opción B
        EndDialogue();
    }

    // Terminar el diálogo
    public void EndDialogue()
    {
        dialogueUI.SetActive(false);
        optionAButton.gameObject.SetActive(false);
        optionBButton.gameObject.SetActive(false);
    }
}
