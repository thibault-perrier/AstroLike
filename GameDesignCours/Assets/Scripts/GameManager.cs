using UnityEngine;
using TMPro;
using System.Collections.Generic;
using UnityEngine.UI;

public class GameManager : MonoBehaviour
{
    public static GameManager instance;

    [Header("Game Properties")]
    [SerializeField] private int _nbOfRoundForWinCondition = 10;
    private int _currentWin = 0;
    [SerializeField] private int _coinPerWin = 5;
    [SerializeField] private int _statUpgradeCost = 10;


    [Header("Setup")]
    public Transform _playerSpawn;
    public Transform _enemySpawn;

    public GameObject player;
    public GameObject enemy;

    [Header("UI")]
    [SerializeField] private TMP_Text _botsDefeated;
    [SerializeField] private TMP_Text _botText;


    [SerializeField] private TMP_Text _frameLvlText;
    [SerializeField] private TMP_Text _motorLvlText;
    [SerializeField] private TMP_Text _steeringLvlText;
    [SerializeField] private TMP_Text _pistonLvlText;

    [SerializeField] private TMP_Text _coinsText;


    [SerializeField] private GameObject _UIToDisplay;


    [Header("Upgrade buttons")]
    [SerializeField] private List<Button> upgradeButtons; // 0 is frame, 1 is motor, 2 is steering, 3 is piston

    private void Awake()
    {
        instance = this;
    }

    void Start()
    {
        SetupGame(false);
    }

    public void SetupGame(bool RoundIsWon)
    {
        if (RoundIsWon)
        {
            _currentWin++;
            ShopManager.instance.Coins += _coinPerWin;
        }

        if (_currentWin >= _nbOfRoundForWinCondition) Debug.Log("YOU WON THE GAME");

        PauseGame(true);

        UpdateUI();
        SetBotPositions();

        _UIToDisplay.gameObject.SetActive(true);
    }


    public void PauseGame(bool pauseGame)
    {
        Time.timeScale = pauseGame ? 0.0f : 1.0f;
    }

    public void UpdateUI()
    {
        UpdateWinConUI();

        UpdateUpgradeButtons();

        UpdateStatUI();
    }

    private void UpdateUpgradeButtons()
    {
        for (int statIndex = 0; statIndex < upgradeButtons.Count; statIndex++)
        {
            StatsManager.BotStats chosenBot = StatsManager.instance.bots[StatsManager.instance.ChosenBot];
            bool isStatUpgradable = false;

            // I am sorry for the war crime that is this code
            switch (statIndex)
            {
                case 0:
                    isStatUpgradable = chosenBot.MaxFrameLvl - chosenBot.CurrentFrameLvl > 0;
                    break;
                case 1:
                    isStatUpgradable = chosenBot.MaxMotorLvl - chosenBot.CurrentMotorLvl > 0;
                    break;
                case 2:
                    isStatUpgradable = chosenBot.MaxSteeringLvl - chosenBot.CurrentSteeringLvl > 0;
                    break;
                case 3:
                    isStatUpgradable = chosenBot.MaxPistonLvl - chosenBot.CurrentPistonLvl > 0;
                    break;
                default:
                    break;
            }

            upgradeButtons[statIndex].interactable = ShopManager.instance.Coins >= _statUpgradeCost && isStatUpgradable;
        }
    }

    private void UpdateStatUI()
    {
        _coinsText.text = ShopManager.instance.Coins + " coins";

        StatsManager.BotStats chosenBot = StatsManager.instance.bots[StatsManager.instance.ChosenBot];

        _frameLvlText.text = "Frame level: " + chosenBot.CurrentFrameLvl;
        _motorLvlText.text = "Motor level: " + chosenBot.CurrentMotorLvl;
        _steeringLvlText.text = "Steering level: " + chosenBot.CurrentSteeringLvl;
        _pistonLvlText.text = "Piston level: " + chosenBot.CurrentPistonLvl;
    }

    private void UpdateWinConUI()
    {
        if (_currentWin < _nbOfRoundForWinCondition)
            _botsDefeated.text = _currentWin + "/" + _nbOfRoundForWinCondition;
        else
        {
            _botsDefeated.text = default;
            _botText.text = "YOU WON";
        }
    }

    private void SetBotPositions()
    {
        player.GetComponent<Rigidbody>().velocity = Vector3.zero;
        enemy.GetComponent<Rigidbody>().velocity = Vector3.zero;

        player.transform.position = _playerSpawn.position;
        player.transform.rotation = _playerSpawn.rotation;

        enemy.transform.position = _enemySpawn.position;
        enemy.transform.rotation = _enemySpawn.rotation;

        player.GetComponent<Rigidbody>().mass = StatsManager.instance.bots[StatsManager.instance.ChosenBot].CurrentFrameLvl;
        UpgradeEnemyRandStatsBasedOnPlayerLvl();
    }

    private void UpgradeEnemyRandStatsBasedOnPlayerLvl()
    {
        StatsManager.BotStats chosenBot = StatsManager.instance.bots[StatsManager.instance.ChosenBot];

        EnemyController enemyController = enemy.GetComponent<EnemyController>();

        enemyController.EnemyFrameLvl = 1;
        enemyController.EnemyMotorLvl = 1;
        enemyController.EnemySteeringLvl = 1;
        enemyController.EnemyPistonLvl = 1;

        for (int upgradeIndex = 0; upgradeIndex < chosenBot.UpgradePoints; upgradeIndex++)
        {
            int statIndex = Random.Range(0, 4);

            switch (statIndex)
            {
                case 0:
                    enemyController.EnemyFrameLvl++;
                    break;
                case 1:
                    enemyController.EnemyMotorLvl++;
                    break;
                case 2:
                    enemyController.EnemySteeringLvl++;
                    break;
                case 3:
                    enemyController.EnemyPistonLvl++;
                    break;
                default:
                    break;
            }
        }
    }
}
