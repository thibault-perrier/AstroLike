using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShopManager : MonoBehaviour
{
    public static ShopManager instance;

    public int Coins = 10;


    [Header("Random coin generator")]
    public List<Transform> _bounds;
    [SerializeField] private float _minTimeBeforeCoinSpawn;
    [SerializeField] private float _maxTimeBeforeCoinSpawn;
    [SerializeField] private float _coinLifeTime;
    [SerializeField] private GameObject _coinPrefab;
    [SerializeField] private int _minCoinValue;
    [SerializeField] private int _maxCoinValue;

    public bool _gameIsRunning;


    private void Awake()
    {
        instance = this;
    }

    public void UpgradeStat(int statIndex)
    {
        Coins -= 10;
        StatsManager.instance.UpgradeStat(statIndex);
    }

    public void GainCoin()
    {
        Coins += Random.Range(_minCoinValue, _maxCoinValue);
        GameManager.instance.UpdateUI();
    }

    public IEnumerator RandomCoinGenerator()
    {
        if (_gameIsRunning)
        {
            float actualTimer = Random.Range(_minTimeBeforeCoinSpawn, _maxTimeBeforeCoinSpawn);

            yield return new WaitForSeconds(actualTimer);

            Vector2 randPos = MathUtils.GetRandomPosOnAreaWithCorners(_bounds[0].position.x, _bounds[0].position.z, _bounds[1].position.x, _bounds[1].position.z);
            GameObject coinGO = Instantiate(_coinPrefab, new Vector3(randPos.x, GameManager.instance._playerSpawn.position.y, randPos.y), new Quaternion());
            Destroy(coinGO, _coinLifeTime);

            StartCoroutine(RandomCoinGenerator());
        }
    }
}
