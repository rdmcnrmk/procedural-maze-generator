using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LevelCreator
{
    public enum GridType
    {
        EMPTY = 0,
        FLOOR,
        WALL,
        WALLL,
        WALLR,
        WALLU,
        WALLD,
        WALLUL,
        WALLUR,
        WALLDL,
        WALLDR,
        CORNERUL,
        CORNERDL,
        CORNERUR,
        CORNERDR
    };

    private GridType[,] grid;
    private int maxFloors;
    private float noDirectionThreshold;
    private float turnLeftThreshold;
    private float turnRightThreshold;
    private Vector2Int gridSize;


    public LevelCreator(Vector2Int gridSize, int maxFloors, float noDirectionThreshold, float turnLeftThreshold, float turnRightThreshold)
    {
        this.gridSize = gridSize;
        this.grid = new GridType[gridSize.x, gridSize.y];
        this.maxFloors = maxFloors;
        this.noDirectionThreshold = noDirectionThreshold;
        this.turnLeftThreshold = turnLeftThreshold;
        this.turnRightThreshold = turnRightThreshold;

        CreateFloors();
        ScaleUpGrid();
        CreateWalls();
        DefineWalls();
    }

    public GridType[,] GetGrid()
    {
        return grid;
    }

    private void ScaleUpGrid()
    {
        GridType[,] resultGrid = new GridType[grid.GetLength(0) * 2, grid.GetLength(1) * 2];

        for (int i = 0; i < grid.GetLength(0); i++)
        {
            for (int j = 0; j < grid.GetLength(1); j++)
            {
                if(grid[i,j] == GridType.FLOOR)
                {
                    resultGrid[2 * i, 2 * j] = GridType.FLOOR;
                    resultGrid[2 * i, 2 * j + 1] = GridType.FLOOR;
                    resultGrid[2 * i + 1, 2 * j] = GridType.FLOOR;
                    resultGrid[2 * i + 1, 2 * j + 1] = GridType.FLOOR;
                }
            }
        }

        grid = resultGrid;
    }

    private void CreateFloors()
    {
        int counter = 0;
        int maxCount = 100000;

        Vector2Int lastFloorPosition = new Vector2Int(5, 5);
        Vector2Int dir = new Vector2Int(0, 1);

        for (int i = 0; i < maxFloors;)
        {
            if (counter > maxCount) break;
            counter++;

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
            }

            if (grid[lastFloorPosition.x, lastFloorPosition.y] == GridType.EMPTY)
            {
                grid[lastFloorPosition.x, lastFloorPosition.y] = GridType.FLOOR;
                i++;
            }

            Vector2Int temp = lastFloorPosition + dir;
            if (temp.x < 1 || temp.y < 1 || temp.x >= gridSize.x-1 || temp.y >= gridSize.y-1) dir = Vector2Int.zero;

            lastFloorPosition += dir;
        }
    }

    private void CreateWalls()
    {
        for (int i = 1; i < gridSize.x-1; i++)
        {
            for (int j = 1; j < gridSize.y-1; j++)
            {
                if(grid[i, j] == GridType.FLOOR)
                {
                    if (grid[i - 1, j - 1] != GridType.FLOOR) grid[i - 1,j - 1] = GridType.WALL;
                    if (grid[i - 1, j] != GridType.FLOOR) grid[i - 1, j] = GridType.WALL;
                    if (grid[i - 1, j + 1] != GridType.FLOOR) grid[i - 1, j + 1] = GridType.WALL;
                    if (grid[i, j - 1] != GridType.FLOOR) grid[i, j - 1] = GridType.WALL;
                    if (grid[i, j + 1] != GridType.FLOOR) grid[i, j + 1] = GridType.WALL;
                    if (grid[i + 1, j - 1] != GridType.FLOOR) grid[i + 1, j - 1] = GridType.WALL;
                    if (grid[i + 1, j] != GridType.FLOOR) grid[i + 1, j] = GridType.WALL;
                    if (grid[i + 1, j + 1] != GridType.FLOOR) grid[i + 1, j + 1] = GridType.WALL;
                }
            }
        }
    }

    private void DefineWalls()
    {
        GridType[,] result = new GridType[grid.GetLength(0), grid.GetLength(1)];
        for (int i = 1; i < gridSize.x - 1; i++)
        {
            for (int j = 1; j < gridSize.y - 1; j++)
            {
                if (grid[i, j] == GridType.WALL)
                {
                    if (grid[i, j - 1] == GridType.WALL && grid[i, j + 1] == GridType.WALL && grid[i + 1, j] == GridType.FLOOR) result[i, j] = GridType.WALLL;
                    else if (grid[i, j - 1] == GridType.WALL && grid[i, j + 1] == GridType.WALL && grid[i - 1, j] == GridType.FLOOR) result[i, j] = GridType.WALLR;
                    else if (grid[i + 1, j] == GridType.WALL && grid[i - 1, j] == GridType.WALL && grid[i, j + 1] == GridType.FLOOR) result[i, j] = GridType.WALLD;
                    else if (grid[i + 1, j] == GridType.WALL && grid[i - 1, j] == GridType.WALL && grid[i, j - 1] == GridType.FLOOR) result[i, j] = GridType.WALLU;

                    else if (grid[i + 1, j] == GridType.WALL && grid[i, j + 1] == GridType.WALL && grid[i + 1, j + 1] == GridType.FLOOR) result[i, j] = GridType.WALLDL;
                    else if (grid[i + 1, j] == GridType.WALL && grid[i, j - 1] == GridType.WALL && grid[i + 1, j - 1] == GridType.FLOOR) result[i, j] = GridType.WALLUL;
                    else if (grid[i - 1, j] == GridType.WALL && grid[i, j + 1] == GridType.WALL && grid[i - 1, j + 1] == GridType.FLOOR) result[i, j] = GridType.WALLDR;
                    else if (grid[i - 1, j] == GridType.WALL && grid[i, j - 1] == GridType.WALL && grid[i - 1, j - 1] == GridType.FLOOR) result[i, j] = GridType.WALLUR;

                    else if (grid[i + 1, j] == GridType.WALL && grid[i, j + 1] == GridType.WALL) result[i, j] = GridType.CORNERDR;
                    else if (grid[i + 1, j] == GridType.WALL && grid[i, j - 1] == GridType.WALL) result[i, j] = GridType.CORNERUR;
                    else if (grid[i - 1, j] == GridType.WALL && grid[i, j + 1] == GridType.WALL) result[i, j] = GridType.CORNERUL;
                    else if (grid[i - 1, j] == GridType.WALL && grid[i, j - 1] == GridType.WALL) result[i, j] = GridType.CORNERDL;
                    else result[i, j] = GridType.WALL;
                }
                else if(grid[i, j] == GridType.FLOOR)
                {
                    result[i, j] = GridType.FLOOR;
                }
            }
        }
        grid = result;
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
