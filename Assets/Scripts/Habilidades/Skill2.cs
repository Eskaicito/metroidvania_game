using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Skill2 : MonoBehaviour, ISkill
{
    public void Activate()
    {
        Debug.Log("Skill2 Activated");
        // Aquí podrías realizar configuraciones adicionales o mostrar un ícono
    }

    public void Use()
    {
        Debug.Log("Skill2 Used");
        // Aquí implementas la acción que realiza la habilidad cuando se usa
    }
}
