using Cinemachine;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraController : MonoBehaviour
{

    public CinemachineVirtualCamera virtualCamera;  
    public Transform playerTransform;               
    public float minYSpeed = -5f;                  
    public float maxYSpeed = 5f;                    
    public float maxDamping = 3f;                  
    public float minDamping = 0.5f;                 
    public float transitionTime = 0.3f;             

    private CinemachineFramingTransposer framingTransposer;
    private Rigidbody2D playerRigidbody;

    private void Start()
    {
    
        framingTransposer = virtualCamera.GetCinemachineComponent<CinemachineFramingTransposer>();

       
        playerRigidbody = playerTransform.GetComponent<Rigidbody2D>();
    }

    private void Update()
    {
        
        AdjustDampingBasedOnSpeed();
    }

    void AdjustDampingBasedOnSpeed()
    {
     
        float playerYSpeed = playerRigidbody.velocity.y;

      
        float targetDamping = Mathf.Lerp(minDamping, maxDamping, Mathf.InverseLerp(minYSpeed, maxYSpeed, Mathf.Abs(playerYSpeed)));


        framingTransposer.m_YDamping = Mathf.Lerp(framingTransposer.m_YDamping, targetDamping, Time.deltaTime / transitionTime);
    }
}
