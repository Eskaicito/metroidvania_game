using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Skill3 : MonoBehaviour, ISkill
{
    public void Activate()
    {
        Debug.Log("Skill3 Activated");
        // Aqu� podr�as realizar configuraciones adicionales o mostrar un �cono
    }

    public void Use()
    {
        Debug.Log("Skill3 Used");
        // Aqu� implementas la acci�n que realiza la habilidad cuando se usa
    }
}
