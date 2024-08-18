using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class PauseMenu : MonoBehaviour
{
    public GameObject pauseMenuInstance;
    public GameObject pauseMenuPrefab;
    private bool isPaused = false;

    void Start()
    {
        
        if (pauseMenuInstance == null)
        {
            pauseMenuInstance = Instantiate(pauseMenuPrefab);
            DontDestroyOnLoad(pauseMenuInstance);
            pauseMenuInstance.SetActive(false); 
        }
    }

    void Update()
    {
        if (Input.GetKeyUp(KeyCode.Escape))
        {
            if (isPaused)
            {
                ResumeGame();
            }
            else
            {
                PauseGame();
            }
        }
       
    }

    public void ResumeGame()
    {
        pauseMenuInstance.SetActive(false);
        Time.timeScale = 1f;
        isPaused = false;
        
    }

    public void PauseGame()
    {
        pauseMenuInstance.SetActive(true);
        Time.timeScale = 0;
        isPaused = true;
    }

    public void QuitGame()
    {
        Application.Quit();
    }

    void OnEnable()
    {
        
        SceneManager.sceneLoaded += OnSceneLoaded;
    }

    void OnDisable()
    {
        
        SceneManager.sceneLoaded -= OnSceneLoaded;
    }

    void OnSceneLoaded(Scene scene, LoadSceneMode mode)
    {
        
        if (pauseMenuInstance == null)
        {
            pauseMenuInstance = Instantiate(pauseMenuPrefab);
            DontDestroyOnLoad(pauseMenuInstance);
            pauseMenuInstance.SetActive(false);
        }
    }
}
