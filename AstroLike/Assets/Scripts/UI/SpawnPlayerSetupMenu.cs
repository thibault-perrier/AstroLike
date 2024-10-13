using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.InputSystem.UI;

public class SpawnPlayerSetupMenu : MonoBehaviour
{
    [SerializeField] private GameObject _playerSetupMenuPrefab;

    [SerializeField] private PlayerInput _playerInput;

    void Awake()
    {
        GameObject _playerSetupMenuHolder = GameObject.Find("PlayerSetupMenuHolder");
        GameObject menu = Instantiate(_playerSetupMenuPrefab, _playerSetupMenuHolder.transform);
        _playerInput.uiInputModule = menu.GetComponentInChildren<InputSystemUIInputModule>();
        menu.GetComponent<PlayerSetupMenuController>().SetPlayerIndex(_playerInput.playerIndex);
    }
}
