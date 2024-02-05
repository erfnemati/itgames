using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
using System.IO;


public class PersistentDataManager : MonoBehaviour
{

    public const string m_playersInfoString = "PlayersInfo";
    public static PersistentDataManager _instance;

    PlayersInfo m_playersInfo;
    PlayerPersistentData m_currentData;

    private bool m_isLevelOver = false;

    [SerializeField] string m_phoneNumer;
    [SerializeField] int m_numOfConsumedLives;
    [SerializeField] float m_passedTime;
    [SerializeField] int m_playerLastLevel;

    private string m_saveDataPath;

    private void OnEnable()
    {
        LevelManager.OnLevelDefeat += IncrementNumOfConsumedLives;
        PhoneScreenManager.EndLevel += TurnOffTimer;
        PlayerLifeManager.GameIsOver += GetLevel;
        LevelManager.OnLevelRetreat += GetLevel;
        LevelManager.OnGameWin += GetLevelAfterWin;
    }

    private void OnDisable()
    {
        LevelManager.OnLevelDefeat -= IncrementNumOfConsumedLives;
        PhoneScreenManager.EndLevel -= TurnOffTimer;
        PlayerLifeManager.GameIsOver -= GetLevel;
        LevelManager.OnLevelRetreat -= GetLevel;
        LevelManager.OnGameWin -= GetLevelAfterWin;

    }

    private void Awake()
    {
        if (_instance != null && _instance != this)
        {
            Destroy(this.gameObject);
        }
        else
        {
            _instance = this;
            DontDestroyOnLoad(this.gameObject);
        }
        StartNewPlaythrough();
    }


    private void Start()
    {
        m_playersInfo = new PlayersInfo();
        Debug.Log("Instantiating new playersInfo object");
        m_saveDataPath = Application.dataPath + "LeaderBoardTextFile.json";
        //StartNewPlaythrough();
        RetrieveData();
    }

    private void Update()
    {
        if (m_currentData != null && m_isLevelOver == false )
        {
            m_currentData.UpdateTime(Time.deltaTime);
            m_passedTime = m_currentData.GetPlayingTime();
        }
    }

    public void StartNewPlaythrough()
    {
        m_currentData = new PlayerPersistentData();
    }

    public void AddData(string phoneNumber)
    {
        m_playersInfo.m_playersInfoList.Add
            (new PlayerPersistentData
                (phoneNumber,m_currentData.GetNumOfConsumedLives(),m_currentData.GetPlayingTime(),
                m_currentData.GetPlayerLastLevel(),m_currentData.GetPlayerId())
            );
    }

    public void SaveData(string enteredText)
    {
        string phoneNumber = enteredText;

        SetPhoneNumber(phoneNumber);
        AddData(phoneNumber);
        SortPlayersList();

        string jsonString = JsonUtility.ToJson(m_playersInfo,true);
        Debug.Log("Here is saved note: " + jsonString);
        File.WriteAllText(Application.dataPath + "LeaderBoardTextFile.json", jsonString);
        //PlayerPrefs.SetString(m_playersInfoString, jsonString);
        //PlayerPrefs.Save();
    }

    private void SetPhoneNumber(string phoneNumber)
    {
        m_currentData.SetPhoneNumber(phoneNumber);
    }

    public void RetrieveData()
    {
        //string jsonString = PlayerPrefs.GetString(m_playersInfoString, "Empty");
        Debug.Log("Retriving");
        if (File.Exists(m_saveDataPath))
        {
            string retrivedJsonString = File.ReadAllText(m_saveDataPath);
            Debug.Log(retrivedJsonString);
            if (retrivedJsonString != null)
            {
                m_playersInfo = JsonUtility.FromJson<PlayersInfo>(retrivedJsonString);
            }
            else
            {
                Debug.Log("Nothing to show for now");
            }

        }
    }

    public void IncrementNumOfConsumedLives()
    {
        Debug.Log("Increasing number of lives");
        m_currentData.IncrementConsumedLives();
    }

    public void TurnOffTimer()
    {
        Debug.Log("Timer is off");
        m_isLevelOver = true;
    }

    public PlayerPersistentData GetCurrentPlayerData()
    {
        return m_currentData;
    }

    public void GetLevel()
    {
        Debug.Log("Getting Level");
        int level = BmsPlusSceneManager._instance.GetCurrentLevel();
        m_currentData.SetPlayerLastLevel(level -1);
        m_playerLastLevel = level -1;
    }

    public void GetLevelAfterWin()
    {
        int level = BmsPlusSceneManager._instance.GetCurrentLevel();
        m_currentData.SetPlayerLastLevel(level);
        m_playerLastLevel = level;
        Debug.Log("Our level is : " + level);
    }

    private void SortPlayersList()
    {
        m_playersInfo.Sort();
    }

    public float  GetPlayerCluster()
    {
        if (m_playersInfo.m_playersInfoList.Count <= 4)
        {
            if (m_currentData.GetPlayerLastLevel() >= 11 && m_currentData.GetPlayerLastLevel() <= 14)
            {
                return 0.125f;
            }

            else
            {
                return 0.3f;
            }
        }

        int playerRank = m_playersInfo.GetIndex(m_currentData.GetPlayerId()) + 1;
        float playerCluster = (float)playerRank / m_playersInfo.m_playersInfoList.Count;
        Debug.Log("Player cluster is : " + playerCluster);
        return playerCluster;
        //Debug.Log("Player rank is : " + m_playersInfo.GetIndex(m_currentData.GetPlayerId()));
        //return (m_playersInfo.GetIndex(m_currentData.GetPlayerId()));
    }

    public List<PlayerPersistentData> GetPlayersInfo()
    {
        return m_playersInfo.m_playersInfoList;
    }
}

[Serializable]
public class PlayersInfo
{
    public List<PlayerPersistentData> m_playersInfoList = new List<PlayerPersistentData>();

    public  void Sort()
    {
        m_playersInfoList.Sort();
    }

    public int GetIndex(int id)
    {
        for(int i = 0; i < m_playersInfoList.Count; i++)
        {
            if (m_playersInfoList[i].GetPlayerId() == id)
            {
                return i;
            }
                
        }
        return -1;
    }
}
