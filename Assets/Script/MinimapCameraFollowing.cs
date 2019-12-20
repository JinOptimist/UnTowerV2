using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MinimapCameraFollowing : MonoBehaviour
{
    public GameObject Hero;
    public float Height = 5;

    private bool revertFogState = false;

    // Update is called once per frame
    void Update()
    {
        var heroPosition = Hero.transform.position;
        heroPosition.y = Height;
        transform.position = heroPosition;
    }

    //Disable fog for minimap
    private void OnPreRender()
    {
        revertFogState = RenderSettings.fog;
        RenderSettings.fog = false;
    }

    private void OnPostRender()
    {
        RenderSettings.fog = revertFogState;
    }
}
