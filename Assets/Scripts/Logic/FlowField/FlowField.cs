
using System;
using System.Collections.Generic;
using System.Drawing;
using Unity.VisualScripting;
using UnityEngine;
using Random = UnityEngine.Random;

public class FlowField
{
    private static FlowField instance;
    public static FlowField GetInstance()
    {
        if (instance == null)
        {
            instance = new FlowField();
        }

        return instance;
    }

    public Cell[,] cells;
    public int width;
    public int height;
    public Vector3 center;
    public Vector3 offset;
    public Vector3 cellSize;
    public Cell targetCell;

    public void GenerateWorld(Vector2Int worldSize, Vector3 cellSize)
    {
        center = Vector3.zero;
        this.cellSize = cellSize;
        offset = new Vector3(-worldSize.x * 0.5f, 0, -worldSize.y * 0.5f) + this.cellSize * 0.5f;
        width = worldSize.x;
        height = worldSize.y;
        cells = new Cell[width, height];

        for (int x = 0; x < width; x++)
        {
            for (int y = 0; y < height; y++)
            {
                Vector2Int cellIndex = new Vector2Int(x, y);
                Vector3 cellPosition = new Vector3(x * this.cellSize.x, 0, y * this.cellSize.z) + offset;
                Cell cell = CellManager.Instance.CreateCell(cellIndex, cellPosition, this.cellSize);
                cells[x, y] = cell;
                // 周围一圈是墙
                if (x == 0 || x == width - 1 || y == 0 || y == height - 1)
                {
                    cell.SelectAsWall();
                }
            }
        }
        
    }

    public void SetTarget(Cell cell)
    {
        targetCell = cell;
    }

    public Cell GetTarget()
    {
        return targetCell;
    }

    public void DoSearch()
    {
        DijkstraCalculateDistance();
        CalculateVector();
        CalculateHeatMap();
    }

    
    // 计算热力图 只是debug可视化用的，实际上不需要
    void CalculateHeatMap()
    {
        int maxDistance = int.MinValue;
        for (int i = 0; i < width; i++)
        {
            for (int j = 0; j < height; j++)
            {
                var cell = cells[i, j];
                if (cell.cellType != CellType.Walkable)
                {
                    continue;
                }

                maxDistance = Mathf.Max(maxDistance, cell.distance);
            }
        }

        for (int i = 0; i < width; i++)
        {
            for (int j = 0; j < height; j++)
            {
                var cell = cells[i, j];
                if (cell.cellType != CellType.Walkable)
                {
                    continue;
                }

                float rate = (float)cell.distance / maxDistance;
                cell.UpdateDistanceRate(rate);
            }
        }
    }

    void DijkstraCalculateDistance()
    {
        Queue<Cell> queue = new Queue<Cell>();
        List<Cell> neighbors = new List<Cell>();
        targetCell.UpdateDistance(0);
        targetCell.UpdateVector(Vector3.zero);
        queue.Enqueue(targetCell);
        int counter = 0;
        int maxCounter = width * height * 2;
        while (queue.Count > 0)
        {
            var curCell = queue.Dequeue();
            GetNeighbors(curCell, ref neighbors);
            for (int i = 0; i < neighbors.Count; i++)
            {
                var neighbor = neighbors[i];
                var vector = neighbor.index - curCell.index;
                var dis = Mathf.Abs(vector.x) + Mathf.Abs(vector.y);
                var newDistance = 0;
                if (dis == 1)
                {
                    // 水平方向
                    newDistance = curCell.distance + 10;
                    
                }
                else if (dis == 2)
                {
                    // 斜对角方向
                    newDistance = curCell.distance + 14;
                }

                if (neighbor.distance == -1 || newDistance > 0 && newDistance < neighbor.distance)
                {
                    neighbor.UpdateDistance(newDistance);
                    queue.Enqueue(neighbor);
                }
            }
            counter++;
            if (maxCounter <= counter)
            {
                Debug.LogError("死循环了");
                break;
            }
        }
    }

    void CalculateVector()
    {
        List<Cell> neighbors = new List<Cell>();
        for (int i = 0; i < width; i++)
        {
            for (int j = 0; j < height; j++)
            {
                var curCell = cells[i, j];
                if (curCell.cellType != CellType.Walkable)
                {
                    continue;
                }

                GetNeighbors(curCell, ref neighbors);
                Cell minCell = null;
                int minDis = int.MaxValue;
                for (int k = 0; k < neighbors.Count; k++)
                {
                    var neighbor = neighbors[k];
                    if (neighbor.distance < minDis)
                    {
                        minDis = neighbor.distance;
                        minCell = neighbor;
                    }
                }

                if (minCell != null)
                {
                    var vector = minCell.index - curCell.index;
                    curCell.UpdateVector(new Vector3(vector.x, 0, vector.y));
                }
            }
        }
    }

    void GetNeighbors(Cell cell, ref List<Cell> neighbors)
    {
        neighbors.Clear();
        Vector2Int index = cell.index;
        for (int x = index.x - 1; x <= index.x + 1; x++)
        {
            for (int y = index.y - 1; y <= index.y + 1; y++)
            {
                if (x < 0 || x >= width || y < 0 || y >= height || (x == index.x && y == index.y))
                {
                    // 超边界 或者是 自身
                    continue;
                }
                Cell neighbor = cells[x, y];
                if (neighbor.cellType == CellType.Obstacle || neighbor.cellType == CellType.Wall)
                {
                    continue;
                }
                neighbors.Add(neighbor);
            }
        }
    }

    public Vector3 RandomGetValidPosition()
    {
        var walkableCell = RandomGetWalkableCell();
        var position = walkableCell.position + new Vector3(Random.Range(0f, 1f) * cellSize.x, 0, Random.Range(0f, 1f) * cellSize.z);
        return position;
    }

    Cell RandomGetWalkableCell()
    {
        int x = UnityEngine.Random.Range(0, width);
        int y = UnityEngine.Random.Range(0, height);
        Cell cell = cells[x, y];
        if (cell.cellType != CellType.Walkable)
        {
            return RandomGetWalkableCell();
        }
        return cell;
    }
}
