using UnityEngine;

public class HitDetection : MonoBehaviour
{
    private CollectManager _collectManager;
    private GameManager _gameManager;
    private PlayerManager _playerManager;
    private ObjectPooler _objectPooler;
    private LevelManager _levelManager;

    public PlayerMovementController playerMovementController;

    public GameObject Player;

    public int CollectedSphereToCoin;



    private void Awake()
    {
        _collectManager = CollectManager.Instance;
        _gameManager = GameManager.Instance;
        _playerManager = PlayerManager.Instance;
        _objectPooler = ObjectPooler.Instance;
        _levelManager = LevelManager.Instance;
    }


    private void OnTriggerEnter(Collider other)
    {
        if (other.tag == "Obstacle")
        {
            _collectManager.AddCoin(-CollectedSphereToCoin);
            _collectManager.ObstacleObjects.Add(other.gameObject);
            GameObject particleGO = _objectPooler.SpawnForGameObject("MagicExplosionFire", new Vector3(other.transform.position.x,other.transform.position.y+1,other.transform.position.z), Quaternion.identity, _objectPooler.poolParent.transform.GetChild(0).transform);
            Destroy(particleGO, 1);
            other.gameObject.SetActive(false);

        }

        if (other.tag == "Collectable")
        {
            _collectManager.AddCoin(CollectedSphereToCoin);
            _collectManager.CollectedObjects.Add(other.gameObject);
            GameObject particleGO = _objectPooler.SpawnForGameObject("MoneyCoinBlast", other.gameObject.transform.position, Quaternion.identity, _objectPooler.poolParent.transform.GetChild(0).transform);
            Destroy(particleGO, 1);
            other.gameObject.SetActive(false);
        }

        if (other.tag == "Finish")
        {
            _gameManager.UpdateGameState(GameState.WinGame);
        }
    }
}
