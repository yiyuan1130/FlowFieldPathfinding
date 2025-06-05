using System;
using UnityEngine;

public class MoveEntityPositionChangeCommand : RenderCommand
{
    public Guid guid;
    public Vector3 position;
    public override void Execute()
    {
        base.Execute();
        var moveEntityRender = MoveEntityRenderManager.Instance.GetRender(guid);
        moveEntityRender.OnPositionChange(position);
    }
}
