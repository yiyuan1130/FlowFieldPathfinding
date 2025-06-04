using System;
using UnityEngine;

public class CellDistanceRateChangeCommand : RenderCommand
{
    public Guid guid;
    public float distanceRate;
    public override void Execute()
    {
        base.Execute();
        var cellRender = CellRenderManager.Instance.GetCellRender(guid);
        cellRender.OnCellDistanceRateChange(distanceRate);
    }
}
