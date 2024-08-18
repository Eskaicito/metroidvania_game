using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ParallaxEffect : MonoBehaviour
{
    [System.Serializable]
    public class ParallaxLayer
    {
        public Transform layerTransform;  // Transform del fondo
        public float parallaxFactor;      // Factor de parallax
    }

    public List<ParallaxLayer> parallaxLayers;  // Lista de capas de parallax
    public Camera mainCamera;  // Cámara principal
    private Vector3 previousCameraPosition;

    private void Start()
    {
        if (mainCamera == null)
        {
            mainCamera = Camera.main;  // Si no hay cámara asignada, tomar la principal
        }

        previousCameraPosition = mainCamera.transform.position;
    }

    private void LateUpdate()
    {
        Vector3 cameraMovement = mainCamera.transform.position - previousCameraPosition;

        foreach (ParallaxLayer layer in parallaxLayers)
        {
            Vector3 newLayerPosition = layer.layerTransform.position;
            newLayerPosition.x += cameraMovement.x * layer.parallaxFactor;
            newLayerPosition.y += cameraMovement.y * layer.parallaxFactor;
            layer.layerTransform.position = newLayerPosition;
        }

        previousCameraPosition = mainCamera.transform.position;
    }
}
