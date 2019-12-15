using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class HeroStuff : MonoBehaviour
{
    [Header("UI elements")]
    public Text CoinCountText;

    [Header("Skills")]
    public bool CouldMineGold = false;
    public bool CouldBrokeWall = false;

    [Header("Inventory")]
    public int InitialHeroCoin = 10;
    private int heroCoin = 0;
    private int HeroCoin
    {
        get
        {
            return heroCoin;
        }
        set
        {
            CoinCountText.text = value.ToString();
            heroCoin = value;
        }
    }

    void Start()
    {
        HeroCoin = PlayerPrefs.GetInt("HeroCoin", InitialHeroCoin);
        CouldMineGold = PlayerPrefs.GetInt("CouldMineGold", 0) == 1;
        CouldBrokeWall = PlayerPrefs.GetInt("CouldBrokeWall", 0) == 1;
    }

    public void HeroGetCoins(int countOfCoins = 1)
    {
        HeroCoin += countOfCoins;
    }

    public bool HeroSpendCoins(int countOfCoins)
    {
        if (HeroCoin < countOfCoins)
        {
            return false;
        }

        HeroCoin -= countOfCoins;
        return true;
    }

    public void Save()
    {
        PlayerPrefs.SetInt("HeroCoin", HeroCoin);
        PlayerPrefs.SetInt("CouldMineGold", CouldMineGold ? 1 : 0);
        PlayerPrefs.SetInt("CouldBrokeWall", CouldBrokeWall ? 1 : 0);
    }
}
