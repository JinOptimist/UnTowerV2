using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class HeroStuff : MonoBehaviour
{
    [Header("UI elements")]
    public Text CoinCountText;
    private int HeroCoin = 0;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void HeroGetCoin(int countOfCoin = 1)
    {
        HeroCoin += countOfCoin;
        CoinCountText.text = HeroCoin.ToString();
    }
}
