using System;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class ComponentManager : Singleton<ComponentManager>
{

    private UIManager _uiManager;
    private LevelManager _levelManager;
    private GameManager _gameManager;
    private CollectManager _collectManager;
    private PlayerManager _playerManager;

    public Button PlayButton;

    public Button WinButton;
    

    public TextMeshProUGUI PlayButtonText;

    public TextMeshProUGUI WinButtonText;

    public TextMeshProUGUI CoinNumberText;

    public TextMeshProUGUI LevelNumberTextOnStartPanel;

    public GameObject DistanceSlider;
    public Slider DistanceSliderComponent;

    private float dist;
    private GameObject FinishLine;

    public GameObject CoinHolder;

    public bool isCoinHolder;

    public GameObject LevelNumberText;

    private void Awake()
    {
        _uiManager = UIManager.Instance;
        _gameManager = GameManager.Instance;
        _collectManager = CollectManager.Instance;
        _levelManager = LevelManager.Instance;
        _playerManager = PlayerManager.Instance;
    }

    private void Start()
    {
        PlayButton.onClick.AddListener(() => HandlePlayButton());
        WinButton.onClick.AddListener(() => HandleNextButton());

        if (isCoinHolder)
        {
            CoinHolder.SetActive(true);
        }
        
    }

    private void Update()
    {
        CoinNumberText.text = _collectManager.CollectedCoin.ToString();
        LevelNumberText.GetComponent<TextMeshProUGUI>().text = "Level : " + _levelManager.DisplayLevelNumber.ToString();
        LevelNumberTextOnStartPanel.text = "Level : " + _levelManager.DisplayLevelNumber.ToString();

        FinishLine = _levelManager.levelFinish;
        dist = _playerManager.Player.transform.position.z - (FinishLine.transform.position.z - _playerManager.Player.transform.position.z) * -0.1f;
        float value = dist / FinishLine.transform.position.z;
        DistanceSliderComponent.value = value;
    }

    #region UI Button Options
    private void HandleNextButton()
    {
        _levelManager.NextLevel();
        _collectManager.CollectedCoin = 0;
        _collectManager.ActiveCollectedObject();
        _collectManager.ActiveObstacleObject();
        _gameManager.UpdateGameState(GameState.RestartGame);
    }

    private void HandlePlayButton()
    {
        _gameManager.UpdateGameState(GameState.StartGame);
    }
    #endregion
}
