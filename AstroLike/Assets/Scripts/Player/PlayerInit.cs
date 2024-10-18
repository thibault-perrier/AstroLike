using System;
using System.Collections;
using System.Collections.Generic;
using UnityEditor.U2D;
using UnityEngine;
using UnityEngine.InputSystem;

[RequireComponent(typeof(SpriteRenderer))]
public class PlayerInit : MonoBehaviour
{
    private PlayerConfig _playerConfig;

    public void InitializePlayer(PlayerConfig pc)
    {
        _playerConfig = pc;
        GetComponent<SpriteRenderer>().sprite = pc.PlayerSprite;

        PlayerInput pi = GetComponent<PlayerInput>();
        pi.actions = pc.Input.actions;
        pi.defaultControlScheme = pc.Input.defaultControlScheme;
        pi.neverAutoSwitchControlSchemes = pc.Input.neverAutoSwitchControlSchemes;

        // _playerConfig.Input.onActionTriggered += Input_onActionTriggered;
    }

    // private void Input_onActionTriggered(InputAction.CallbackContext obj)
    // {
    // if (obj.action.name == )
    // }
}
