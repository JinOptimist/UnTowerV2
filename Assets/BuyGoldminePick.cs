using Assets.Script.Interfaces;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BuyGoldminePick : MonoBehaviour, ICanBeInteracted
{
    public int priceOfPick = 1;

    public void Interact(HeroStuff whoInteract)
    {
        if (whoInteract.HeroSpendCoins(priceOfPick))
        {
            whoInteract.CouldMineGold = true;
        }

        gameObject.SetActive(false);
    }

    public void SelectAsActive()
    {
        Debug.Log("Select gold pick as Active");
    }

    public void DeselectAsActive()
    {
        Debug.Log("Gold pick lose Active state");
    }
}
