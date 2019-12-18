using Assets.GameLogic;
using Assets.GameLogic.CellObject;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
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

    /// <summary>
    /// Count of second which we need to full grow a wall
    /// </summary>
    public float SpeedOfDrawLab = 0.5f;
    private bool GrowingWallDone = false;
    private const int CountOfStepForGrowWall = 100;

    [Header("UI")]
    public Text LevelCountText;

    private List<GameObject> ElementsOfLabyrinth = new List<GameObject>();
    private List<GameObject> Walls = new List<GameObject>();
    private ILabyrinthLevel LabyrinthLevel;
    //private bool LabyrinthIsGenerating = false;

    // Start is called before the first frame update
    void Start()
    {
        DepthOfCurrentLevel = PlayerPrefs.GetInt("DepthOfCurrentLevel", 0);
        if (DepthOfCurrentLevel > 0)
        {
            Width += DepthOfCurrentLevel;
            Height += DepthOfCurrentLevel;
        }

        //GenerateFullWallLevel();
        GenerateLabyrinth();
    }

    // Update is called once per frame
    void Update()
    {
        if (GrowingWallDone)
        {
            return;
        }

        IEnumerable<BaseCellObject> walls = LabyrinthLevel.AllCells().OfType<Wall>().ToList();
        walls = walls.Concat(LabyrinthLevel.AllCells().OfType<Goldmine>()).ToList();
        
        foreach (var cell in walls)
        {
            var wall = Walls.FirstOrDefault(x => x.transform.position.x == cell.X && x.transform.position.z == cell.Y);

            var localScale = wall.transform.localScale;
            localScale.y = cell.Height;
            wall.transform.localScale = localScale;

            var position = wall.transform.position;
            position.y = -0.5f + cell.Height / 2;
            wall.transform.position = position;
        }

        GrowingWallDone = !walls.Any(x => x.Height < 1);
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
        //Remove element of old labyrinth
        ElementsOfLabyrinth?.ForEach(Destroy);
        ElementsOfLabyrinth = new List<GameObject>();
        Walls = new List<GameObject>();
        GrowingWallDone = false;

        Width = labyrinthWidth ?? Width;
        Height = labyrinthHeight ?? Height;
        Width++;
        Height++;
        DepthOfCurrentLevel++;
        LevelCountText.text = (DepthOfCurrentLevel).ToString();

        var labyrinthGenerator = new LabyrinthGenerator(Width, Height);
        LabyrinthLevel = labyrinthGenerator.GenerateLevel(startingPointX, startingPointY, DepthOfCurrentLevel);

        var wallCells = new List<BaseCellObject>();
        foreach (var cell in LabyrinthLevel.AllCells())
        {
            GameObject cellGameObject = null;
            var dropHeight = 0f;
            if (cell is Wall)
            {
                cellGameObject = Instantiate(WallTemplate);
                Walls.Add(cellGameObject);
                wallCells.Add(cell);
            }
            else if (cell is Goldmine)
            {
                cellGameObject = GenerateGoldmine();
                Walls.Add(cellGameObject);
                wallCells.Add(cell);
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
        }

        var heroPosition = Hero.transform.position;
        wallCells
            .OrderBy(cell => Vector3.Distance(new Vector3(cell.X, 0, cell.Y), heroPosition)).ToList()
            .ForEach(cell => new Task(() => GrowWall(cell, heroPosition)).Start());

        GenerateBorder();
    }

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

    private void GrowWall(BaseCellObject wall, Vector3 heroPosition)
    {
        wall.Height = 0.01f;

        var distance = Mathf.RoundToInt(Vector3.Distance(heroPosition, new Vector3(wall.X, 0, wall.Y)));

        Thread.Sleep(100 * distance);
        while (wall.Height < 1)
        {
            wall.Height += 1 / (float)CountOfStepForGrowWall;

            var waitSecond = Mathf.RoundToInt(1000 * SpeedOfDrawLab / CountOfStepForGrowWall);
            Thread.Sleep(waitSecond);
        }

        wall.Height = 1;
    }
}
