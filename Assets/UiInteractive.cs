using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UiInteractive : MonoBehaviour
{
    public void ExitFromTheGame()
    {
        Debug.Log("Exit");
        Application.Quit();
    }
}
