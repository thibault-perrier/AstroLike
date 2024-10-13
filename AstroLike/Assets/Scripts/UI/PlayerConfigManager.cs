using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.SceneManagement;

public class PlayerConfigManager : MonoBehaviour
{
    public static PlayerConfigManager Instance { get; private set; }

    [Header("Game Config")]
    [SerializeField] private int _minPlayers = 2;
    [SerializeField] private int _maxPlayers = 4;

    [Header("Sprites")]
    [SerializeField] private List<Sprite> _sprites = new List<Sprite>();


    private List<PlayerConfig> _playerConfigs = new List<PlayerConfig>();
    void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
            DontDestroyOnLoad(Instance);
        }
    }


    public void SetPlayerSprite(int playerIndex, int spriteIndex)
    {
        _playerConfigs[playerIndex].PlayerSprite = _sprites[spriteIndex];
    }

    public void SetPlayerReady(int playerIndex, bool isReady)
    {
        _playerConfigs[playerIndex].IsReady = isReady;

        if (_playerConfigs.Count >= _minPlayers && _playerConfigs.All(p => p.IsReady == true))
        {
            SceneManager.LoadScene("Movement");
        }
    }

    public void HandlePlayerJoin(PlayerInput pi)
    {
        Debug.Log("Player " + pi.playerIndex + " joined!");
        if (!_playerConfigs.Any(p => p.PlayerIndex == pi.playerIndex))
        {
            pi.transform.SetParent(transform);
            _playerConfigs.Add(new PlayerConfig(pi));
        }
    }






}

public class PlayerConfig
{

    public PlayerConfig(PlayerInput pi)
    {
        Input = pi;
        PlayerIndex = pi.playerIndex;
    }


    public PlayerInput Input { get; set; }
    public int PlayerIndex { get; set; }
    public bool IsReady { get; set; }
    public Sprite PlayerSprite { get; set; }
}