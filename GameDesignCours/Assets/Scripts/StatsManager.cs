using System;
using System.Collections.Generic;
using UnityEngine;

public class StatsManager : MonoBehaviour
{
    public static StatsManager instance;

    private void Awake()
    {
        instance = this;
    }


    [Serializable]
    public class BotStats
    {
        public int UpgradePoints;

        public int MaxMotorLvl;
        public int CurrentMotorLvl;

        public int MaxFrameLvl;
        public int CurrentFrameLvl;

        public int MaxSteeringLvl;
        public int CurrentSteeringLvl;

        public int MaxPistonLvl;
        public int CurrentPistonLvl;
    }

    public List<BotStats> bots;

    [Header("Lvl 1 stats")]
    [SerializeField] public float BotMovSpeed = 5.0f;
    [SerializeField] public float BotRotSpeed = 20.0f;
    [SerializeField] public float BotPushForce = 0.1f;
    [SerializeField] public float BotPushDist = 2.0f;

    public int ChosenBot = 0;


    public void UpgradeStat(int statIndex)
    {
        bots[ChosenBot].UpgradePoints++;

        switch (statIndex)
        {
            case 0:
                bots[ChosenBot].CurrentFrameLvl++;
                break;
            case 1:
                bots[ChosenBot].CurrentMotorLvl++;
                break;
            case 2:
                bots[ChosenBot].CurrentSteeringLvl++;
                break;
            case 3:
                bots[ChosenBot].CurrentPistonLvl++;
                break;
            default:
                break;
        }

        GameManager.instance.UpdateUI();
    }
}
