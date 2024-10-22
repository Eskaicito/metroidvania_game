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
        //Scene currentScene = SceneManager.GetActiveScene();
        //scenePath = currentScene.name;

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

    //public void SkipCinematic(GameObject button)
    //{
    //    if (videoPlayer != null)
    //    {
    //        videoPlayer.Stop(); 
    //        AudioSource audioSource = videoPlayer.GetComponent<AudioSource>();
    //        if (audioSource != null)
    //        {
    //            audioSource.Stop(); 
    //        }
    //    }
    //    if (!string.IsNullOrEmpty(sceneName) && sceneLoader != null)
    //    {
    //        sceneLoader.SkipCinematic(sceneName, button);
    //        if (scenePath == "FINALBOSS")
    //        {
    //            sceneLoader.SkipCinematic("MENU", button);
    //        }
    //    }
    //}
}