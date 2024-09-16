using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Skill1 : MonoBehaviour, ISkill
{
    public void Activate()
    {
        Debug.Log("Skill1 Activated");
        // Aquí podrías realizar configuraciones adicionales o mostrar un ícono
    }

    public void Use()
    {
        Debug.Log("Skill1 Used");
        // Aquí implementas la acción que realiza la habilidad cuando se usa
    }
}
