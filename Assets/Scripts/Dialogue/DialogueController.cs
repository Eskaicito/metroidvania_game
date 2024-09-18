using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class DialogueController : MonoBehaviour
{
    [SerializeField] TextMeshProUGUI dialogueText;    // Texto de di�logo
    [SerializeField] GameObject dialogueUI;           // Objeto que contiene el di�logo
    [SerializeField] Button optionAButton;            // Bot�n para la opci�n A
    [SerializeField] Button optionBButton;            // Bot�n para la opci�n B

    private System.Action onOptionA;                  // Acci�n que se ejecuta al elegir la opci�n A
    private System.Action onOptionB;                  // Acci�n que se ejecuta al elegir la opci�n B

    private void Start()
    {
        dialogueUI.SetActive(false);
        optionAButton.gameObject.SetActive(false);
        optionBButton.gameObject.SetActive(false);

        optionAButton.onClick.AddListener(OptionAChosen);
        optionBButton.onClick.AddListener(OptionBChosen);
    }

    // Mostrar el di�logo con dos opciones, ahora con efecto de tipeo
    public void StartDialogueWithOptions(string dialogue, System.Action optionAAction, System.Action optionBAction)
    {
        dialogueUI.SetActive(true);
        onOptionA = optionAAction;
        onOptionB = optionBAction;
        optionAButton.gameObject.SetActive(false);
        optionBButton.gameObject.SetActive(false);

        StartCoroutine(TypeDialogue(dialogue, true)); // Llama a la corutina para el efecto de tipeo
    }

    // Mostrar el di�logo sin opciones (simplemente lo cierra al final), tambi�n con efecto de tipeo
    public void StartSimpleDialogue(string dialogue)
    {
        dialogueUI.SetActive(true);
        optionAButton.gameObject.SetActive(false);
        optionBButton.gameObject.SetActive(false);

        StartCoroutine(TypeDialogue(dialogue, false)); // Llama a la corutina para el efecto de tipeo
    }

    // Corutina para mostrar el texto car�cter por car�cter
    private IEnumerator TypeDialogue(string dialogue, bool hasOptions)
    {
        dialogueText.text = "";
        foreach (char letter in dialogue.ToCharArray())
        {
            dialogueText.text += letter;
            yield return new WaitForSeconds(0.05f); // Ajusta la velocidad del tipeo aqu�
        }

        if (hasOptions)
        {
            // Mostrar los botones de opciones solo cuando el texto haya terminado de escribirse
            optionAButton.gameObject.SetActive(true);
            optionBButton.gameObject.SetActive(true);
        }
        else
        {
            StartCoroutine(CloseAfterDelay(2f)); // Cierra el di�logo despu�s de un peque�o retraso
        }
    }

    // Cerrar el di�logo despu�s de un retraso
    private IEnumerator CloseAfterDelay(float delay)
    {
        yield return new WaitForSeconds(delay);
        EndDialogue();
    }

    // Opci�n A elegida
    private void OptionAChosen()
    {
        Debug.Log("Eleg� la opci�n A");
        onOptionA?.Invoke();  // Ejecuta la acci�n de la opci�n A
        EndDialogue();
    }

    // Opci�n B elegida
    private void OptionBChosen()
    {
        Debug.Log("Eleg� la opci�n B");
        onOptionB?.Invoke();  // Ejecuta la acci�n de la opci�n B
        EndDialogue();
    }

    // Terminar el di�logo
    public void EndDialogue()
    {
        dialogueUI.SetActive(false);
        optionAButton.gameObject.SetActive(false);
        optionBButton.gameObject.SetActive(false);
    }
}
