using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Skill5 : MonoBehaviour, ISkill
{
    public void Activate()
    {
        Debug.Log("Skill5 Activated");
        // Aqu� podr�as realizar configuraciones adicionales o mostrar un �cono
    }

    public void Use()
    {
        Debug.Log("Skill5 Used");
        // Aqu� implementas la acci�n que realiza la habilidad cuando se usa
    }
}
