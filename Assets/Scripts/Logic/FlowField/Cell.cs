using System;
using UnityEngine;

public enum CellType
{
    Walkable,
    Obstacle,
    Target,
    Wall
}

public class Cell : GameEntity
{
    public Vector3 size = new Vector3(1.0f, 0.0f, 1.0f);
    public Vector2Int index;
    public Vector3 position;
    public Vector3 vector;
    public int distance;
    public CellType cellType;
    public float distanceRate; // 热力图的距离比率 [0-1]

    public Cell(Vector2Int index, Vector3 position, Vector3 size) : base()
    {
        this.size = size;
        this.index = index;
        this.position = position;
        this.vector = Vector3.zero;
        this.distance = -1;
        this.cellType = CellType.Walkable;
    }

    public override void OnCreate()
    {
        base.OnCreate();
        RenderCommandManager.Instance.SendCommand(new CreateRenderCommand() { guid = this.guid, renderType = RenderType.Cell });
    }

    public override void OnUpdate(float deltaTime)
    {
        base.OnUpdate(deltaTime);
    }

    public override void OnClose()
    {
        base.OnClose();
    }

    public void SelectAsWall()
    {
        if (this.cellType != CellType.Target && this.cellType != CellType.Obstacle)
        {
            this.cellType = this.cellType == CellType.Wall ? CellType.Walkable : CellType.Wall;
            RenderCommandManager.Instance.SendCommand(new CellTypeChangeCommand(){guid = this.guid, cellType = this.cellType});
        }
    }

    public void SelectAsObstacle()
    {
        if (this.cellType != CellType.Target && this.cellType != CellType.Wall)
        {
            this.cellType = this.cellType == CellType.Obstacle ? CellType.Walkable : CellType.Obstacle;
            RenderCommandManager.Instance.SendCommand(new CellTypeChangeCommand(){guid = this.guid, cellType = this.cellType});
        }
    }

    public bool SelectAsTarget()
    {
        if (this.cellType != CellType.Obstacle && this.cellType != CellType.Wall)
        {
            this.cellType = this.cellType == CellType.Target ? CellType.Walkable : CellType.Target;
            RenderCommandManager.Instance.SendCommand(new CellTypeChangeCommand(){guid = this.guid, cellType = this.cellType});
            return this.cellType == CellType.Target;
        }

        return false;
    }

    public void UpdateVector(Vector3 vector)
    {
        this.vector = vector;
        RenderCommandManager.Instance.SendCommand(new CellVectorChangeCommand(){guid = this.guid, vector = this.vector});
    }

    public void UpdateDistance(int distance)
    {
        this.distance = distance;
        RenderCommandManager.Instance.SendCommand(new CellDistanceChangeCommand(){guid = this.guid, distance = this.distance});
    }

    public void UpdateDistanceRate(float rate)
    {
        this.distanceRate = rate;
        RenderCommandManager.Instance.SendCommand(new CellDistanceRateChangeCommand(){guid = this.guid, distanceRate = this.distanceRate});
    }

    public void Reset()
    {
        UpdateVector(Vector3.zero);
        UpdateDistance(-1);
    }
}
