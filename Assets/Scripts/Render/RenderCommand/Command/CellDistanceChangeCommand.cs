using System;
using UnityEngine;

public class CellDistanceChangeCommand : RenderCommand
{
    public Guid guid;
    public int distance;
    public override void Execute()
    {
        base.Execute();
        var cellRender = CellRenderManager.Instance.GetCellRender(guid);
        cellRender.OnCellDistanceChange(distance);
    }
}
