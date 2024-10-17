using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class PlayerSetupMenuController : MonoBehaviour
{
    private int _playerIndex;

    [SerializeField] private TextMeshProUGUI _titleText;
    [SerializeField] private GameObject _readyPanel;
    [SerializeField] private GameObject _menuPanel;
    [SerializeField] private Button _readyButton;
    [SerializeField] private GameObject _readyText;

    private float _ignoreInputTime = 1.5f;
    private bool _inputEnabled = false;



    public void SetPlayerIndex(int pi)
    {
        _playerIndex = pi;
        _titleText.SetText("Player " + (_playerIndex + 1));
        _ignoreInputTime += Time.time;
    }

    void Update()
    {
        if (Time.time > _ignoreInputTime)
        {
            _inputEnabled = true;
        }
    }

    public void SetSprite(int spriteIndex)
    {
        if (!_inputEnabled) return;

        PlayerConfigManager.Instance.SetPlayerSprite(_playerIndex, spriteIndex);

        _readyPanel.SetActive(true);
        _readyButton.Select();
        _menuPanel.SetActive(false);
    }

    public void SetPlayerReady()
    {
        if (!_inputEnabled) return;

        PlayerConfigManager.Instance.SetPlayerReady(_playerIndex, true);

        _readyButton.gameObject.SetActive(false);
        _readyText.gameObject.SetActive(true);
    }
}
