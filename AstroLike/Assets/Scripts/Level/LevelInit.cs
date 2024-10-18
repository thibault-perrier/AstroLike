using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.InputSystem;

public class LevelInit : MonoBehaviour
{

    [SerializeField] private Transform[] _playerSpawns;
    [SerializeField] private GameObject _playerPrefab;

    void Start()
    {
        List<PlayerConfig> playerConfigs = PlayerConfigManager.Instance.GetPlayerConfigs();
        Destroy(PlayerConfigManager.Instance.gameObject);

        Debug.Log("number of players: " + playerConfigs.Count);

        for (int index = 0; index < playerConfigs.Count; index++)
        {
            GameObject player = Instantiate(_playerPrefab, _playerSpawns[index].position, _playerSpawns[index].rotation, gameObject.transform);
            player.GetComponent<PlayerInit>().InitializePlayer(playerConfigs[index]);
        }
    }
}
