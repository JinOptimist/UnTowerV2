using Assets.GameLogic;
using Assets.GameLogic.CellObject;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.UI;

public class LabGenerator : MonoBehaviour
{
    public GameObject Hero;

    [Header("Templates")]
    public GameObject WallTemplate;
    public GameObject CoinTemplate;
    public GameObject DownTemplate;

    [Header("Type of wall")]
    public Material WallBorderMaterial;
    public Material GoldmineMaterial;

    [Header("Labyrinth parametrs")]
    public int Width;
    public int Height;
    public int DepthOfCurrentLevel = 0;
    [Header("UI")]
    public Text LevelCountText;

    //public float SpeedOfDrawLab = 0.1f;

    private List<GameObject> ElementsOfLabyrinth = new List<GameObject>();
    //private bool LabyrinthIsGenerating = false;


    // Start is called before the first frame update
    void Start()
    {
        //GenerateFullWallLevel();
        GenerateLabyrinth();
    }

    // Update is called once per frame
    void Update()
    {
    }

    /// <summary>
    /// Generate new labyrinth level. You could set coordinate starting point of labyrinth
    /// </summary>
    /// <param name="startingPointX"></param>
    /// <param name="startingPointY"></param>
    /// <param name="labyrinthWidth"></param>
    /// <param name="labyrinthHeight"></param>
    public void GenerateLabyrinth(int startingPointX = 0, int startingPointY = 0, int? labyrinthWidth = null, int? labyrinthHeight = null)
    {
        ElementsOfLabyrinth?.ForEach(Destroy);

        Width = labyrinthWidth ?? Width;
        Height = labyrinthHeight ?? Height;
        Width++;
        Height++;
        DepthOfCurrentLevel++;
        LevelCountText.text = (DepthOfCurrentLevel).ToString();

        var labyrinthGenerator = new LabyrinthGenerator(Width, Height);
        var labyrinthLevel = labyrinthGenerator.GenerateLevel(startingPointX, startingPointY, DepthOfCurrentLevel);
        foreach (var cell in labyrinthLevel.AllCells())
        {
            GameObject cellGameObject = null;
            var dropHeight = 0f;
            if (cell is Wall)
            {
                cellGameObject = Instantiate(WallTemplate);
            }
            else if (cell is Goldmine)
            {
                cellGameObject = GenerateGoldmine();
            }
            else if (cell is Coin)
            {
                cellGameObject = GenerateCoin();
                dropHeight = (cell.X + cell.Y) * 0.2f;
            }
            else if (cell is StairsDown)
            {
                cellGameObject = GenerateDown();
                dropHeight = (cell.X + cell.Y) * 0.2f;

                Hero.GetComponent<HeroMovement>().StairsDown = cellGameObject;
            }

            if (cellGameObject != null)
            {
                cellGameObject.transform.position = new Vector3(cell.X, dropHeight, cell.Y);
                ElementsOfLabyrinth.Add(cellGameObject);
            }
            //var dropHeight = (cell.X + cell.Y) * 0.1f;
            //wall.transform.position = new Vector3(cell.X, dropHeight, cell.Y);
            //wall.transform.localScale = wall.transform.localScale - (new Vector3(0, SpeedOfDrawLAb) * Time.deltaTime);
        }

        GenerateBorder();
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
        //Add values -1 and +1 to border creating
        for (int i = -1; i < Height + 1; i++)
        {
            var rightBorder = GenerateWallBorder();
            rightBorder.transform.position = new Vector3(Width, 0, i);
            var leftBorder = GenerateWallBorder();
            leftBorder.transform.position = new Vector3(-1, 0, i);
        }

        for (int i = 0; i < Width; i++)
        {
            var topBorder = GenerateWallBorder();
            topBorder.transform.position = new Vector3(i, 0, Height);
            var bottomBorder = GenerateWallBorder();
            bottomBorder.transform.position = new Vector3(i, 0, -1);
        }
    }

    private GameObject GenerateWallBorder()
    {
        var wallBorder = Instantiate(WallTemplate);
        wallBorder.GetComponent<Renderer>().material = WallBorderMaterial;
        wallBorder.GetComponent<WallMining>().WallType = WallType.Border;
        ElementsOfLabyrinth.Add(wallBorder);
        return wallBorder;
    }

    private GameObject GenerateGoldmine()
    {
        var goldmine = Instantiate(WallTemplate);
        goldmine.GetComponent<Renderer>().material = GoldmineMaterial;
        goldmine.GetComponent<WallMining>().WallType = WallType.Goldmine;
        return goldmine;
    }

    private GameObject GenerateCoin()
    {
        var coin = Instantiate(CoinTemplate);
        return coin;
    }

    private GameObject GenerateDown()
    {
        var down = Instantiate(DownTemplate);
        return down;
    }

}
