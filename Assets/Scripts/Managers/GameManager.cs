using System;
using UnityEngine;
using System.Threading.Tasks;
using DG.Tweening;
using TMPro;

public class GameManager : Singleton<GameManager>
{
    public GameState State;
    [HideInInspector]
    public TimeState StateTime;
    private UIManager _uiManager;
    private PlayerManager _playerManager;
    private LevelManager _levelManager;
    private CollectManager _collectManager;
    private ComponentManager _componentManager;
    private ObjectPooler _objectPooler;

    public GameObject gamePlayCam, winCam,meterCam;

    private void Awake()
    {
        QualitySettings.vSyncCount = 0;
        Application.targetFrameRate = 60;
        _uiManager = UIManager.Instance;
        _playerManager = PlayerManager.Instance;
        _collectManager = CollectManager.Instance;
        _componentManager = ComponentManager.Instance;
        _levelManager = LevelManager.Instance;
        _objectPooler = ObjectPooler.Instance;
    }

    private void Start()
    {
        UpdateGameState(GameState.WaitGame);
    }

    #region Game State Options
    public void UpdateGameState(GameState newState)
    {
        State = newState;

        switch (newState)
        {
            case GameState.WinGame:
                handleWinGame();
            break;
            case GameState.StartGame:
                handleStartGame();
            break;
            case GameState.WaitGame:
                handleWaitGame();
            break;
            case GameState.RestartGame:
                handleRestartGame();
            break;
        }
    }

    private void handleWaitGame()
    {
        gamePlayCam.SetActive(true);
        winCam.SetActive(false);
        _uiManager.UpdatePanelState(PanelCode.StartPanel, true);
        _playerManager.StopPlayer();
    }
    private void handleStartGame()
    {
        gamePlayCam.SetActive(true);
        winCam.SetActive(false);
        _objectPooler.CreatePoolObjects();
        _uiManager.UpdatePanelState(PanelCode.GamePanel, true);
        _playerManager.StartPlayer();
       
    }
    private async void handleWinGame()
    {
        gamePlayCam.SetActive(false);
        winCam.SetActive(false);
        meterCam.SetActive(true);
        _playerManager._splineFollower.follow = false;
        _levelManager.pointer.transform.DOMoveY(_levelManager.Xs.transform.GetChild(_collectManager.CollectedCoin -1 ).transform.position.y, 5f);
        _playerManager.Player.transform.GetChild(2).transform.DOMoveY(_levelManager.Xs.transform.GetChild(_collectManager.CollectedCoin-1).transform.position.y, 5f);
        
        await Task.Delay(5000);
        gamePlayCam.SetActive(false);
        meterCam.SetActive(false);
        winCam.SetActive(true);
        _uiManager.UpdatePanelState(PanelCode.WinPanel, true);
        _playerManager.StopPlayer();
        _playerManager.Player.transform.GetChild(0).gameObject.GetComponent<Animator>().SetBool("canWin", true);
    }

    private void handleRestartGame()
    {
        gamePlayCam.SetActive(true);
        winCam.SetActive(false);
        _uiManager.UpdatePanelState(PanelCode.GamePanel, true);
        _playerManager.RestartPlayer();
        _objectPooler.CreatePoolObjects();
        _collectManager.ActiveCollectedObject();
        _collectManager.ActiveObstacleObject();
    }
   
    private void OnValueChangedCallback()
    {
        UpdateGameState(State);
    }
    #endregion

}


public enum GameState
{
    WinGame,
    StartGame,
    WaitGame,
    RestartGame 
}

public enum TimeState
{
    HandleSetMaxTime
}