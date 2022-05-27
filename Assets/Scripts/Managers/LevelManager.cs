using UnityEngine;
using Dreamteck.Splines;

public class LevelManager : Singleton<LevelManager>
{

    private CollectManager _collectManager;
    private GameManager _gameManager;
    private PlayerManager _playerManager;

    public int LevelNumber;

    [HideInInspector]
    public int MaxLevel;

    public int DisplayLevelNumber = 1;

    public GameObject LevelHolder;
    [HideInInspector]
    public GameObject[] SpawnedLevels;

    public GameObject[] Levels;

    public SplineComputer _splineComputer;

    public Transform spheresTransform;

    public GameObject Xs;

    public GameObject pointer;

    public GameObject levelFinish;
    
    private void Awake()
    {
        _gameManager = GameManager.Instance;
        _collectManager = CollectManager.Instance;
        _playerManager = PlayerManager.Instance;
        MaxLevel = Levels.Length - 1;
    }

    void Start()
    {
        LoadLevel();
        NextLevelCall();
    }

    public void NextLevel()
    {
        if (LevelNumber == MaxLevel)
        {
            LevelNumber = 0;
        }
        else
        {
            LevelNumber++;
        }
        DisplayLevelNumber++;
        SaveLevel();
        NextLevelCall();
    }

    public void NextLevelCall()
    {
        SpawnedLevels = GameObject.FindGameObjectsWithTag("Level");
        for (int i = 0; i < SpawnedLevels.Length; i++)
        {
            Destroy(SpawnedLevels[i].gameObject);
        }

        GameObject levelGONext = Instantiate(Levels[LevelNumber].gameObject, LevelHolder.transform);
        _splineComputer = levelGONext.transform.GetChild(2).GetComponent<SplineComputer>();
        spheresTransform = levelGONext.transform.GetChild(3).gameObject.transform.GetChild(2).gameObject.transform;
        Xs = levelGONext.transform.GetChild(4).gameObject;
        pointer = levelGONext.transform.GetChild(5).gameObject;
        levelFinish = levelGONext.transform.GetChild(3).gameObject.transform.GetChild(0).gameObject;
    }

    // For level failed system
    public void RestartLevel()
    {
        SpawnedLevels = GameObject.FindGameObjectsWithTag("Level");
        for (int i = 0; i < SpawnedLevels.Length; i++)
        {
            Destroy(SpawnedLevels[i].gameObject);
        }
        GameObject levelGORestart = Instantiate(Levels[LevelNumber].gameObject, LevelHolder.transform);
        _splineComputer = levelGORestart.transform.GetChild(2).GetComponent<SplineComputer>();
        spheresTransform = levelGORestart.transform.GetChild(3).gameObject.transform.GetChild(2).gameObject.transform;
        Xs = levelGORestart.transform.GetChild(4).gameObject;
        pointer = levelGORestart.transform.GetChild(5).gameObject;

    }


    public void SaveLevel()
    {
        PlayerPrefs.SetInt("Level", LevelNumber);
        PlayerPrefs.SetInt("LevelNumber", DisplayLevelNumber);
        PlayerPrefs.Save();
    }

    public void LoadLevel()
    {
        if (PlayerPrefs.HasKey("Level"))
        {
            LevelNumber = PlayerPrefs.GetInt("Level");
        }
        if (PlayerPrefs.HasKey("LevelNumber"))
        {
            DisplayLevelNumber = PlayerPrefs.GetInt("LevelNumber");
        }

        if (DisplayLevelNumber > 1)
        {
            Time.timeScale = 1;
        }
    }
}
