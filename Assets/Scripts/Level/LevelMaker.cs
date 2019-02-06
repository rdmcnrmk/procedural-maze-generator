using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using System.Linq;

public class LevelMaker : MonoBehaviour
{
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

    Vector2Int lastFloorPosition = Vector2Int.zero;
    Vector2Int[] directions = { new Vector2Int(0, 1), new Vector2Int(0, -1), new Vector2Int(1, 0), new Vector2Int(-1, 0) };

    List<Vector2Int> floors;
    
    GameObject floorParent;
    GameObject wallParent;
    
    public static LevelMaker Initialize(GameObject parent)
    {
        GameObject levelMakerObject = new GameObject("LevelMaker");
        levelMakerObject.transform.SetParent(parent.transform);
        return levelMakerObject.AddComponent<LevelMaker>();
    }

    private void Start()
    {
        floorParent = new GameObject("Floor Parent");
        wallParent = new GameObject("Wall Parent");
        floors = new List<Vector2Int>();

        CreateFloors();
        CreateWalls();
    }

    private void CreateWalls()
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
    }

    private void CreateWall(Vector2 pos, GameObject parent)
    {
        Vector3 wallPos = new Vector3(pos.x, 0, pos.y);
        GameObject obj = Instantiate(wallPrefab, wallPos, Quaternion.identity) as GameObject;
        obj.transform.SetParent(parent.transform);
    }

    private void CreateFloors()
    {
        Vector2Int dir = new Vector2Int(0, 1);

        for (int i = 0; i < maxFloors;)
        {

            float rand = Random.Range(0.0f, 100.0f);

            if (rand < noDirectionThreshold)
            {

            }
            else if (rand >= noDirectionThreshold && rand < turnLeftThreshold)
            {
                dir = RotateDirection(dir, true);
            }
            else if (rand >= turnLeftThreshold && rand < turnRightThreshold)
            {
                dir = RotateDirection(dir, false);
            }
            else
            {
                dir = RotateDirection(dir, true);
                dir = RotateDirection(dir, true);
                Vector3 floorPos = new Vector3(lastFloorPosition.x, 0, lastFloorPosition.y);
            }

            if (!floors.Contains(lastFloorPosition))
            {
                Vector3 floorPos = new Vector3(lastFloorPosition.x, 0, lastFloorPosition.y);
                GameObject obj = Instantiate(floorPrefab, floorPos, Quaternion.identity) as GameObject;
                obj.transform.SetParent(floorParent.transform);
                floors.Add(lastFloorPosition);
                i++;
            }

            lastFloorPosition += dir;
        }
    }

    private Vector2Int RotateDirection(Vector2Int dir, bool isClockwise)
    {
        if (isClockwise)
        {
            if (dir == new Vector2Int(1, 0))
            {
                return new Vector2Int(0, -1);
            }
            else if (dir == new Vector2Int(0, -1))
            {
                return new Vector2Int(-1, 0);
            }
            else if (dir == new Vector2Int(-1, 0))
            {
                return new Vector2Int(0, 1);
            }
            else
            {
                return new Vector2Int(1, 0);
            }
        }
        else
        {
            if (dir == new Vector2Int(1, 0))
            {
                return new Vector2Int(0, 1);
            }
            else if (dir == new Vector2Int(0, 1))
            {
                return Vector2Int.left;
            }
            else if (dir == new Vector2Int(-1, 0))
            {
                return Vector2Int.down;
            }
            else
            {
                return Vector2Int.right;
            }
        }
    }
}
