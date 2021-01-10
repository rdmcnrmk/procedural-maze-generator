using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using System.Linq;

public class LevelMaker : MonoBehaviour
{
    [SerializeField]
    private Sprite[] dirts;

    [SerializeField]
    private Sprite leftWall;

    [SerializeField]
    private Sprite rightWall;

    [SerializeField]
    private Sprite topWall;

    [SerializeField]
    private Sprite bottomWall;

    [SerializeField]
    private Sprite bottomLeftWall;

    [SerializeField]
    private Sprite topLeftWall;

    [SerializeField]
    private Sprite bottomRightWall;

    [SerializeField]
    private Sprite topRightWall;

    [SerializeField]
    private Sprite bottomLeftCorner;

    [SerializeField]
    private Sprite topLeftCorner;

    [SerializeField]
    private Sprite bottomRightCorner;

    [SerializeField]
    private Sprite topRightCorner;

    [SerializeField]
    GameObject floorPrefab;

    [SerializeField]
    GameObject wallPrefab;

    [SerializeField]
    int maxFloors = 600;

    [SerializeField]
    float noDirectionThreshold = 40;

    [SerializeField]
    float turnLeftThreshold = 60;

    [SerializeField]
    float turnRightThreshold = 80;

    [SerializeField]
    Vector2Int gridSize = new Vector2Int(128, 128);

    Vector2Int lastFloorPosition = Vector2Int.zero;
    Vector2Int[] directions = { new Vector2Int(0, 1), new Vector2Int(0, -1), new Vector2Int(1, 0), new Vector2Int(-1, 0) };

    List<Vector2Int> floors;
    
    GameObject floorParent;
    GameObject wallParent;

    private int width = 64;
    private int height = 64;

    [SerializeField]
    private Texture2D tempTex;

    private LevelCreator levelCreator;
    
    public static LevelMaker Initialize(GameObject parent)
    {
        GameObject levelMakerObject = new GameObject("LevelMaker");
        levelMakerObject.transform.SetParent(parent.transform);
        return levelMakerObject.AddComponent<LevelMaker>();
    }

    private void Start()
    {
        levelCreator = new LevelCreator(gridSize, maxFloors, noDirectionThreshold, turnLeftThreshold, turnRightThreshold);
        floorParent = new GameObject("Floor Parent");
        wallParent = new GameObject("Wall Parent");
        floors = new List<Vector2Int>();

        //CreateFloors();
        //CreateWalls();
        CreateFloorObjects(levelCreator.GetGrid());
    
        Texture2D texture = new Texture2D(64, 64);
        Color[] colors = texture.GetPixels();
        /*for(int i = 0; i< width; i++)
        {
            for(int j = 0; j < height; j++)
            {
                if (result[i, j])
                {
                    Debug.Log("Black");
                    colors[i * height + j] = Color.black;
                }
                else
                {
                    Debug.Log("White");
                    colors[i * height + j] = Color.white;
                }
            }
        }
        texture.SetPixels(colors);
        texture.Apply();
        tempTex = texture;*/
    }

    /*private void CreateWalls()
    {
        for (int i = 0; i < floors.Count; i++)
        {
            for (int j = 0; j < directions.Length; j++)
            {
                Vector2Int top = floors[i] + directions[j];
                if (!floors.Contains(top))
                {
                    CreateWall(top, wallParent);
                }
            }
        }
    }*/

    private void CreateFloorObjects(LevelCreator.GridType[,] grid)
    {
        for (int i = 0; i < grid.GetLength(0); i++)
        {
            for (int j = 0; j < grid.GetLength(1); j++)
            {
                if (grid[i, j] == LevelCreator.GridType.WALL) CreateWall(new Vector2(i, j), wallParent, null);
                else if (grid[i, j] == LevelCreator.GridType.WALLL) CreateWall(new Vector2(i, j), wallParent, leftWall);
                else if (grid[i, j] == LevelCreator.GridType.WALLR) CreateWall(new Vector2(i, j), wallParent, rightWall);
                else if (grid[i, j] == LevelCreator.GridType.WALLU) CreateWall(new Vector2(i, j), wallParent, topWall);
                else if (grid[i, j] == LevelCreator.GridType.WALLD) CreateWall(new Vector2(i, j), wallParent, bottomWall);
                else if (grid[i, j] == LevelCreator.GridType.WALLDL) CreateWall(new Vector2(i, j), wallParent, bottomLeftWall);
                else if (grid[i, j] == LevelCreator.GridType.WALLDR) CreateWall(new Vector2(i, j), wallParent, bottomRightWall);
                else if (grid[i, j] == LevelCreator.GridType.WALLUL) CreateWall(new Vector2(i, j), wallParent, topLeftWall);
                else if (grid[i, j] == LevelCreator.GridType.WALLUR) CreateWall(new Vector2(i, j), wallParent, topRightWall);
                else if (grid[i, j] == LevelCreator.GridType.CORNERDL) CreateWall(new Vector2(i, j), wallParent, bottomLeftCorner);
                else if (grid[i, j] == LevelCreator.GridType.CORNERDR) CreateWall(new Vector2(i, j), wallParent, bottomRightCorner);
                else if (grid[i, j] == LevelCreator.GridType.CORNERUL) CreateWall(new Vector2(i, j), wallParent, topLeftCorner);
                else if (grid[i, j] == LevelCreator.GridType.CORNERUR) CreateWall(new Vector2(i, j), wallParent, topRightCorner);
                else if (grid[i, j] == LevelCreator.GridType.WALL) CreateWall(new Vector2(i, j), wallParent, null);
                else if (grid[i, j] == LevelCreator.GridType.FLOOR) CreateFloor(new Vector2(i, j), floorParent);
            }
        }
    }

    private void CreateWall(Vector2 pos, GameObject parent, Sprite sprite)
    {
        Vector3 wallPos = new Vector3(pos.x, pos.y, 0);
        GameObject obj = Instantiate(wallPrefab, wallPos, Quaternion.identity) as GameObject;
        if(sprite != null)
            obj.GetComponent<SpriteRenderer>().sprite = sprite;
        obj.transform.SetParent(parent.transform);
    }

    private void CreateFloor(Vector2 pos, GameObject parent)
    {
        Vector3 wallPos = new Vector3(pos.x, pos.y, 0);
        GameObject obj = Instantiate(floorPrefab, wallPos, Quaternion.identity) as GameObject;
        obj.GetComponent<SpriteRenderer>().sprite = dirts[Random.Range(0, dirts.Length)];
        obj.transform.SetParent(parent.transform);
    }
}
