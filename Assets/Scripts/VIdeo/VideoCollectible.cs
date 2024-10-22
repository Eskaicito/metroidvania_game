using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Video;

public class VideoCollectible : MonoBehaviour, ICollectible
{
    [SerializeField] private VideoClip collectibleVideo; 
    [SerializeField] private VideoPlayer videoPlayerUI;   

    private bool isCollected = false;

    private void Start()
    {
        videoPlayerUI.gameObject.SetActive(false);
    }

    public void Collect()
    {
        if (!isCollected)
        {
          
            videoPlayerUI.clip = collectibleVideo;
            videoPlayerUI.gameObject.SetActive(true); 
            videoPlayerUI.Play();

            videoPlayerUI.loopPointReached += OnVideoEnd;

            isCollected = true;  
            Destroy(gameObject);  
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {
            Collect();
        }
    }

    private void OnVideoEnd(VideoPlayer vp)
    {
        videoPlayerUI.gameObject.SetActive(false);  
    }
}
