using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class VFXMANAGER : MonoBehaviour
{
    public GameObject[] vfxPrefabs; 

   
    public void PlayVFX(int vfxIndex, Vector3 position, Quaternion rotation = default)
    {
        if (vfxIndex >= 0 && vfxIndex < vfxPrefabs.Length && vfxPrefabs[vfxIndex] != null)
        {
            GameObject vfxInstance = Instantiate(vfxPrefabs[vfxIndex], position, rotation);
            Animator vfxAnimator = vfxInstance.GetComponent<Animator>();

            if (vfxAnimator != null)
            {
                
                Destroy(vfxInstance, vfxAnimator.GetCurrentAnimatorStateInfo(0).length);
            }
            else
            {
        
                Destroy(vfxInstance, 2f);
            }
        }
        else
        {
            Debug.LogWarning("VFX no válido o índice fuera de rango.");
        }
    }
}
