using UnityEngine;

public class ShopManager : MonoBehaviour
{
    public static ShopManager instance;

    public int Coins = 10;

    // [Header()]

    private void Awake()
    {
        instance = this;
    }

    public void UpgradeStat(int statIndex)
    {
        Coins -= 10;
        StatsManager.instance.UpgradeStat(statIndex);
    }
}
