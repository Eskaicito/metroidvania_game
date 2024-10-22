using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.Video;

public class VIdeoController : MonoBehaviour
{
    private VideoPlayer videoPlayer;
    [SerializeField] string sceneName;
    private string scenePath;

    void Start()
    {
       

        videoPlayer = FindObjectOfType<VideoPlayer>();
        if (videoPlayer == null)
        {
            Debug.LogError("VideoPlayer component not found on this GameObject.");
        }
        else
        {
            videoPlayer.loopPointReached += OnVideoEnd;
        }
    }

    void OnVideoEnd(VideoPlayer vp)
    {
        SceneManager.LoadScene("SampleScene");
    }


}