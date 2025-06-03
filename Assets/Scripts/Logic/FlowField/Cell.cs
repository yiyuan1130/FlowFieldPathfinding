using System;
using UnityEngine;

public enum CellType
{
    Walkable,
    Obstacle,
}

public class Cell : GameEntity
{
    public Vector3 size;
    public Vector2Int index;
    public Vector3 position;
    public Vector3 vector;
    public int distance;
    public CellType cellType;

    public Cell(Vector2Int index, Vector3 position) : base()
    {
        this.size = new Vector3(1.0f, 0.0f, 1.0f);
        this.index = index;
        this.position = position;
        this.vector = Vector3.zero;
        this.distance = -1;
        this.cellType = CellType.Walkable;
    }

    public override void OnCreate()
    {
        base.OnCreate();
        RenderCommandManager.Instance.SendCommand(RenderCommandType.CreateRender, new CreateRenderCommand()
        {
            renderType = RenderType.Cell,
            guid = guid,
        });
    }

    public override void OnUpdate(float deltaTime)
    {
        base.OnUpdate(deltaTime);
    }

    public override void OnClose()
    {
        base.OnClose();
    }
}
