using Assets.GameLogic;
using Assets.GameLogic.CellObject;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class LabGenerator : MonoBehaviour
{
    public GameObject WallTemplate;

    public Material WallBorderMaterial;
    public Material GoldmineMaterial;

    public int Width;
    public int Height;

    public float SpeedOfDrawLAb = 0.1f;

    public ILabirinthLevel LabirinthLevel;

    private bool LabirinthStillUpdating = true;

    // Start is called before the first frame update
    void Start()
    {
        GenerateBorder();
        //GenerateFullWallLevel();

        LabirinthStillUpdating = true;

        var labirinthGenerator = new LabirinthGenerator(Width, Height);
        LabirinthLevel = labirinthGenerator.GenerateLevel();
        DrawLabirinth();
    }

    // Update is called once per frame
    void Update()
    {
        
        //LabirinthStillUpdating = false;
    }

    private void DrawLabirinth()
    {
        foreach (var cell in LabirinthLevel.AllCells())
        {
            if (cell is Wall)
            {
                var wall = Instantiate(WallTemplate);
                var dropHeight = (cell.X + cell.Y) * 0.1f;
                wall.transform.position = new Vector3(cell.X, dropHeight, cell.Y);
                //wall.transform.localScale = wall.transform.localScale - (new Vector3(0, SpeedOfDrawLAb) * Time.deltaTime);
            }
            //else if (cell is Goldmine)
            //{
            //    var wall = Instantiate(WallTemplate);
            //    var x = cell.X;
            //    var z = cell.Y;
            //    wall.transform.position = new Vector3(x, 0, z);
            //    wall.GetComponent<Renderer>().material = GoldmineMaterial;
            //}
        }
    }

    //private void GenerateFullWallLevel()
    //{
    //    for (int z = 0; z < Height; z++)
    //    {
    //        var row = new List<GameObject>();
    //        for (int x = 0; x < Width; x++)
    //        {
    //            var wall = Instantiate(WallTemplate);
    //            wall.transform.position = new Vector3(x, 0, z);
    //            row.Add(wall);
    //        }

    //        AllWalls.Add(row);
    //    }
    //}

    private void GenerateBorder()
    {
        //Add values -1 and +2 to create border
        for (int i = -1; i < Height + 1; i++)
        {
            var rightBorder = GetWallBorder();
            rightBorder.transform.position = new Vector3(Width, 0, i);
            var leftBorder = GetWallBorder();
            leftBorder.transform.position = new Vector3(-1, 0, i);
        }

        for (int i = 0; i < Width; i++)
        {
            var topBorder = GetWallBorder();
            topBorder.transform.position = new Vector3(i, 0, Height);
            var bottomBorder = GetWallBorder();
            bottomBorder.transform.position = new Vector3(i, 0, -1);
        }
    }

    private GameObject GetWallBorder()
    {
        var wallBorder = Instantiate(WallTemplate);
        wallBorder.GetComponent<Renderer>().material = WallBorderMaterial;
        return wallBorder;
    }
}
