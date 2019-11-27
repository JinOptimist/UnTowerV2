using Assets.GameLogic;
using Assets.GameLogic.CellObject;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LabGenerator : MonoBehaviour
{
    public GameObject WallTemplate;
    public ILabirinthLevel labirinthLevel;
    // Start is called before the first frame update
    void Start()
    {
        var labirinthGenerator = new LabirinthGenerator(10, 10);
        labirinthLevel = labirinthGenerator.GenerateLevel();
        foreach (var cell in labirinthLevel.AllCells())
        {
            if (cell is Wall
                || cell is Goldmine)
            {
                var wall = Instantiate(WallTemplate);
                var x = cell.X;
                var z = cell.Y;
                wall.transform.position = new Vector3(x, 0, z);
            }
        }

        for (int i = -1; i < labirinthLevel.Width + 1; i++)
        {
            var wall = Instantiate(WallTemplate);
            wall.transform.position = new Vector3(labirinthLevel.Height + 1, 0, i);
            wall = Instantiate(WallTemplate);
            wall.transform.position = new Vector3(-1, 0, i);
        }

        for (int i = -1; i < labirinthLevel.Height + 1; i++)
        {
            var wall = Instantiate(WallTemplate);
            wall.transform.position = new Vector3(i, 0, labirinthLevel.Width + 1);
            wall = Instantiate(WallTemplate);
            wall.transform.position = new Vector3(i, 0, -1);
        }
    }

    // Update is called once per frame
    void Update()
    {

    }
}
