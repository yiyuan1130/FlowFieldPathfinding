
using System;
using System.Drawing;
using UnityEngine;

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
            }
        }
    }
}
