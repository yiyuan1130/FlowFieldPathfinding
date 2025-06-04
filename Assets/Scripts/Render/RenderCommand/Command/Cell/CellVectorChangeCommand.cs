using System;
using UnityEngine;

public class CellVectorChangeCommand : RenderCommand
{
    public Guid guid;
    public Vector3 vector;
    public override void Execute()
    {
        base.Execute();
        var cellRender = CellRenderManager.Instance.GetCellRender(guid);
        cellRender.OnCellVectorChange(vector);
    }
}
