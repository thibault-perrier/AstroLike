using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.InputSystem;

public class LevelInit : MonoBehaviour
{

    [SerializeField] private Transform[] _playerSpawns;

    void Start()
    {
        List<PlayerConfig> playerConfigs = PlayerConfigManager.Instance.GetPlayerConfigs();

        Debug.Log("number of players: " + playerConfigs.Count);

        for (int index = 0; index < playerConfigs.Count; index++)
        {
            GameObject player = playerConfigs[index].playerTransform.gameObject;

            player.transform.position = _playerSpawns[index].position;
            player.GetComponent<Rigidbody2D>().bodyType = RigidbodyType2D.Dynamic;
            player.GetComponent<PlayerInputHandler>().enabled = true;
            player.GetComponent<SpriteRenderer>().enabled = true;
            player.GetComponent<BoxCollider2D>().enabled = true;
        }
    }
}
