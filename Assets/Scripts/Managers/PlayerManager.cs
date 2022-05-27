using System.Threading.Tasks;
using UnityEngine;
using Dreamteck.Splines;

public class PlayerManager : Singleton<PlayerManager>
{
    private GameManager _gameManager;
    private LevelManager _levelManager;

    public GameObject Player;
    private GameObject playerMesh;
    [HideInInspector]
    public float PlayerSpeed;

    public float DefaultPlayerSpeed = 6;

    public SplineFollower _splineFollower;

    private Animator _animator;

    private Rigidbody _rigidbody;

    private CapsuleCollider _capsuleCollider;


    private void Awake()
    {
        _gameManager = GameManager.Instance;
        _levelManager = LevelManager.Instance;
    }

    private void Start()
    {
        _splineFollower = Player.GetComponent<SplineFollower>();
        playerMesh = Player.transform.GetChild(0).gameObject;
        _animator = playerMesh.GetComponent<Animator>();
        _rigidbody = playerMesh.GetComponent<Rigidbody>();
        _capsuleCollider = playerMesh.GetComponent<CapsuleCollider>();
    }


    #region Player Start And Stop Options
    public void StopPlayer()
    {
        PlayerSpeed = 0;
    }

    public void StartPlayer()
    {
        _splineFollower.spline = _levelManager._splineComputer;
        PlayerSpeed = DefaultPlayerSpeed;
        _animator.SetBool("canRun", true);
    }

    public async void RestartPlayer()
    {
        _splineFollower.Restart();
        _animator.enabled = true;
        _rigidbody.isKinematic = true;
        _animator.SetBool("canRun", false);
        _animator.SetBool("canWin", false);
        _capsuleCollider.enabled = false;
        Player.transform.position = new Vector3(0, 0, 0);
        playerMesh.transform.localPosition = new Vector3(0, 0, 0);
        await Task.Delay(2000);
        _splineFollower.follow = true;
        _animator.SetBool("canRun", true);
        _capsuleCollider.enabled = true;
        _gameManager.UpdateGameState(GameState.StartGame);
    }
    #endregion

}
