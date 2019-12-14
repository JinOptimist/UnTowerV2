using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class HeroStuff : MonoBehaviour
{
    [Header("UI elements")]
    public Text CoinCountText;
    private int HeroCoin = 0;

    [Header("Skills")]
    public bool CouldMineGold = false;
    public bool CouldBrokeWall = false;

    public void HeroGetCoins(int countOfCoins = 1)
    {
        HeroCoin += countOfCoins;
        CoinCountText.text = HeroCoin.ToString();
    }
}
