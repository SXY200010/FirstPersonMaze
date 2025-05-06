using UnityEngine;
using System.Collections.Generic;

public class MazeGenerator : MonoBehaviour
{
    [Header("Maze Size")]
    public int rows = 5;
    public int columns = 5;
    public float cellSize = 5f;

    [Header("Wall Settings")]
    public GameObject floorPrefab;
    public GameObject wallPrefab;
    public GameObject pillarPrefab;
    public GameObject coinPrefab;
    public GameObject goalPrefab;
    public int coinCount = 10;

    // assume your wallPrefab is a cube scaled to (cellSize, wallHeight, wallThickness)
    public float wallHeight = 0f;
    public float wallThickness = 0.2f;

    private RecursiveMazeGenerator mazeGen;
    private List<Vector3> floorCenters = new List<Vector3>();

    void Start()
    {
        mazeGen = new RecursiveMazeGenerator(rows, columns);
        mazeGen.GenerateMaze();

        BuildMaze();
        SpawnCoins();
    }

    void BuildMaze()
    {
        Transform parentT = transform;

        // for each cell, place floor & walls
        for (int r = 0; r < rows; r++)
        {
            for (int c = 0; c < columns; c++)
            {
                Vector3 center = new Vector3(c * cellSize, 0f, r * cellSize);
                MazeCell cell = mazeGen.GetMazeCell(r, c);

                // floor
                if (floorPrefab != null)
                    Instantiate(floorPrefab, center, Quaternion.identity, parentT);

                // record for coin spawning
                floorCenters.Add(center);

                // walls
                if (wallPrefab != null)
                {
                    // front wall (+Z)
                    if (cell.WallFront)
                    {
                        Vector3 pos = center + new Vector3(0f, wallHeight / 2f, cellSize / 2f);
                        Instantiate(wallPrefab, pos, Quaternion.identity, parentT);
                    }
                    // back wall (-Z)
                    if (cell.WallBack)
                    {
                        Vector3 pos = center + new Vector3(0f, wallHeight / 2f, -cellSize / 2f);
                        Instantiate(wallPrefab, pos, Quaternion.Euler(0, 180, 0), parentT);
                    }
                    // right wall (+X)
                    if (cell.WallRight)
                    {
                        Vector3 pos = center + new Vector3(cellSize / 2f, wallHeight / 2f, 0f);
                        Instantiate(wallPrefab, pos, Quaternion.Euler(0, 90, 0), parentT);
                    }
                    // left wall (-X)
                    if (cell.WallLeft)
                    {
                        Vector3 pos = center + new Vector3(-cellSize / 2f, wallHeight / 2f, 0f);
                        Instantiate(wallPrefab, pos, Quaternion.Euler(0, 270, 0), parentT);
                    }
                }
            }
        }

        // pillars at every intersection
        if (pillarPrefab != null)
        {
            for (int r = 0; r <= rows; r++)
                for (int c = 0; c <= columns; c++)
                {
                    Vector3 p = new Vector3(c * cellSize - cellSize / 2f,
                                            wallHeight / 2f,
                                            r * cellSize - cellSize / 2f);
                    Instantiate(pillarPrefab, p, Quaternion.identity, parentT);
                }
        }

        // goal at last cell
        if (goalPrefab != null)
        {
            Vector3 goalPos = new Vector3((columns - 1) * cellSize,
                                          0f,
                                          (rows - 1) * cellSize);
            Instantiate(goalPrefab, goalPos, Quaternion.identity, parentT);
        }
    }

    void SpawnCoins()
    {
        if (coinPrefab == null || coinCount <= 0) return;

        var spots = new List<Vector3>(floorCenters);
        Vector3 start = new Vector3(0, 0, 0);
        Vector3 end = new Vector3((columns - 1) * cellSize, 0, (rows - 1) * cellSize);
        spots.Remove(start);
        spots.Remove(end);

        for (int i = 0; i < coinCount && spots.Count > 0; i++)
        {
            int idx = Random.Range(0, spots.Count);
            Vector3 pos = spots[idx] + Vector3.up * 0.5f;
            spots.RemoveAt(idx);
            Instantiate(coinPrefab, pos, Quaternion.identity, transform);
        }
    }
}
