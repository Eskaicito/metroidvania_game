using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Skill1 : MonoBehaviour, ISkill
{
    public void Activate()
    {
        Debug.Log("Skill1 Activated");
        // Aqu� podr�as realizar configuraciones adicionales o mostrar un �cono
    }

    public void Use()
    {
        Debug.Log("Skill1 Used");
        // Aqu� implementas la acci�n que realiza la habilidad cuando se usa
    }
}
