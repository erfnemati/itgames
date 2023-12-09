using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class BmsPlusSceneManager : MonoBehaviour
{
    public static BmsPlusSceneManager _instance;
    [SerializeField] int m_mainMenuIndex;

    private void Awake()
    {
        if(_instance != null && _instance != this)
        {
            Destroy(this.gameObject);
        }
        else
        {
            _instance = this;
        }
    }

    public void LoadNextLevel()
    {
        int currentLevelIndex = SceneManager.GetActiveScene().buildIndex;
        int nextLevelIndex = (currentLevelIndex + 1) % SceneManager.sceneCountInBuildSettings;
        SceneManager.LoadScene(nextLevelIndex);
    }

    public void RestartLevel()
    {
        int currentLevelIndex = SceneManager.GetActiveScene().buildIndex;
        SoundManager._instance.StopAllSoundEffects();
        SceneManager.LoadScene(currentLevelIndex);
        
    }

    public void LoadMainMenu()
    {
        Debug.Log("Loading main menu");
        SoundManager._instance.StopAllSoundEffects();
        SceneManager.LoadScene(m_mainMenuIndex);
        
    }

    public void OnApplicationQuit()
    {
        Application.Quit();
    }
}
