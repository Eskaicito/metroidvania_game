using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Skill4 : MonoBehaviour, ISkill
{
    public void Activate()
    {
        Debug.Log("Skill4 Activated");
        // Aquí podrías realizar configuraciones adicionales o mostrar un ícono
    }

    public void Use()
    {
        Debug.Log("Skill4 Used");
        // Aquí implementas la acción que realiza la habilidad cuando se usa
    }
}
