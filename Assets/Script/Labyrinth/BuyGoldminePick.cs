using Assets.Script.Interfaces;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BuyGoldminePick : MonoBehaviour, ICanBeInteracted
{
    public int PriceOfPick = 1;

    public bool IsAutoInteracted => false;

    public void Interact(HeroStuff whoInteract)
    {
        if (whoInteract.HeroSpendCoins(PriceOfPick))
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
