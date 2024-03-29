using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;
using UnityEngine.SceneManagement;

public class DoshanbeSuriGameManager : MonoBehaviour
{
    public static DoshanbeSuriGameManager _instance;
    [SerializeField] GameObject m_grayScreen;
    [SerializeField] GameObject m_resultMenu;
    [SerializeField] GameObject m_pauseMenu;

    [SerializeField] GameObject m_blurredScreen;
    [SerializeField] GameObject m_wheelOfLuck;
    [SerializeField] GameObject m_wheelOfLuckHandler;
    [SerializeField] GameObject m_tutorialManager;
    [SerializeField] GameObject m_LampParent;
    [SerializeField] GameObject m_winningVfx;

    [SerializeField] DoshanbeSuriBackgroundMusic m_backgroundAudioSource;
    [SerializeField] GameObject m_levelSelectorManager;

    private void Start()
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

    public void FinishGame()
    {
        UpdateLevelSelectManager();
        m_backgroundAudioSource.FadeOutMusic();
        m_winningVfx.gameObject.SetActive(true);
        Invoke(nameof(ShowResultMenu), 2f);
    }

    private void ShowResultMenu()
    {
        //UpdateLevelSelectManager();
        m_pauseMenu.gameObject.SetActive(false);
        m_grayScreen.gameObject.SetActive(true);
        m_resultMenu.gameObject.SetActive(true);
    }

    private void UpdateLevelSelectManager()
    {
        int currentLevelIndex = SceneManager.GetActiveScene().buildIndex;
        Debug.Log("Current level index is : " + currentLevelIndex);

        LevelInfo tempLvlInfo = new LevelInfo(3, true);

        if (LevelSelectManager._levelSelectManagerInstance != null)
        {
            LevelSelectManager._levelSelectManagerInstance.UpdateLevelInfo(currentLevelIndex, tempLvlInfo);
        }
        else
        {
            Instantiate(m_levelSelectorManager);
            LevelSelectManager._levelSelectManagerInstance.UpdateLevelInfo(currentLevelIndex, tempLvlInfo);

        }
        LevelSelectManager._levelSelectManagerInstance.UpdateCurrentLevel
            (SceneManager.GetActiveScene().buildIndex + 1);

    }

    public void StartDoshanbeSuri()
    {
        PrepareScreenForWheelOfLuck();
        ActivateWheelOfLuck();
        ActivateNumOfTriesLamps();
        Invoke(nameof(ActivateTutorialManager), 1f);
    }
    private void PrepareScreenForWheelOfLuck()
    {
        m_blurredScreen.gameObject.SetActive(true);
    }

    private void ActivateWheelOfLuck()
    {
        m_wheelOfLuck.gameObject.SetActive(true);
        m_wheelOfLuckHandler.gameObject.SetActive(true);
    }

    private void ActivateTutorialManager()
    {
        m_tutorialManager.gameObject.SetActive(true);
    }

    private void ActivateNumOfTriesLamps()
    {
        m_LampParent.gameObject.SetActive(true);
    }
}
