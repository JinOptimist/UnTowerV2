using Assets.Script.Interfaces;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CoinInteracted : MonoBehaviour, ICanBeInteracted
{
    private Animator Animator;

    public bool IsAutoInteracted => true;

    // Start is called before the first frame update
    void Start()
    {
        Animator = GetComponent<Animator>();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void Interact(HeroStuff whoInteract)
    {
        //var coin = collision.gameObject;
        //After animation coin will be destroyed
        Animator.Play("CoinDestroy");
        //Remove a few components from coin object. 
        //After that, the Hero can go through the Coin
        Destroy(GetComponent<Rigidbody>());
        Destroy(GetComponent<CapsuleCollider>());
        whoInteract.HeroGetCoins();
    }

    public void SelectAsActive()
    {
        Animator.enabled = true;
    }

    public void DeselectAsActive()
    {
        Animator.enabled = false;
    }

    
}
