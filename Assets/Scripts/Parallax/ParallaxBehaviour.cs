using Cinemachine;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ParallaxBehaviour : MonoBehaviour
{
    public CinemachineVirtualCamera virtualCamera;
    public Vector2 parallaxEffectMultiplier = new Vector2(0.5f, 0.5f);
    public bool lockYAxis = false; 
    public float smoothing = 1f; 

    private Transform cameraTransform;
    private Vector3 lastCameraPosition;

    private void Start()
    {
        if (virtualCamera != null)
        {
            cameraTransform = virtualCamera.VirtualCameraGameObject.transform;
        }
        else
        {
            cameraTransform = Camera.main.transform;
        }
        lastCameraPosition = cameraTransform.position;
    }

    private void LateUpdate()
    {
        Vector3 deltaMovement = cameraTransform.position - lastCameraPosition;

        float parallaxX = deltaMovement.x * parallaxEffectMultiplier.x;
        float parallaxY = lockYAxis ? 0 : deltaMovement.y * parallaxEffectMultiplier.y;

        Vector3 targetPosition = transform.position + new Vector3(parallaxX, parallaxY, 0);
        transform.position = Vector3.Lerp(transform.position, targetPosition, smoothing * Time.deltaTime);

        lastCameraPosition = cameraTransform.position;
    }
}
