﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum WallType { Border, BaseWall, Goldmine };

public class WallMining : MonoBehaviour
{
    public HeroStuff HeroStuff;

    public WallType WallType;

    public float DistandOfMining;

    // Start is called before the first frame update
    void Start()
    {
    }

    // Update is called once per frame
    void Update()
    {
       
        
    }

    void OnMouseDown()
    {
        var distanceBetweenHeroAndWall = Vector3.Distance(
            HeroStuff.gameObject.transform.position,
            transform.position);

        if (distanceBetweenHeroAndWall > DistandOfMining)
        {
            return;
        }

        var step = new Vector3(0, 0, 0);
        switch (WallType)
        {
            case WallType.Goldmine:
                HeroStuff.HeroGetCoin(3);
                step = new Vector3(0, 0, 0.35f);
                break;
            case WallType.BaseWall:
                step = new Vector3(0, 0, 0.11f);
                break;
        }

        transform.localScale -= step;
        transform.position += step / 2;

        if (transform.localScale.z <= 0)
        {
            Destroy(transform.gameObject);
        }
    }
}
