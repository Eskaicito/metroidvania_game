using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraShake : MonoBehaviour
{
    public float shakeIntensity;  
    public float shakeDuration;  

    private Transform cameraTransform;
    private Vector3 originalPosition;
    private float shakeTimeRemaining;

    void Start()
    {
        cameraTransform = transform;
        originalPosition = cameraTransform.localPosition;
    }

    void Update()
    {
        if (shakeTimeRemaining > 0)
        {
            cameraTransform.localPosition = originalPosition + (Vector3)Random.insideUnitCircle * shakeIntensity;
            shakeTimeRemaining -= Time.deltaTime;
        }
        else
        {
            shakeTimeRemaining = 0f;
            cameraTransform.localPosition = originalPosition;
        }
    }

    public void Shake(float intensity, float duration)
    {
        shakeIntensity = intensity;
        shakeDuration = duration;
        shakeTimeRemaining = duration;
    }
}
