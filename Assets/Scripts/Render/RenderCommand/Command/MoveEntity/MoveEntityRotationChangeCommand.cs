using System;
using UnityEngine;

public class MoveEntityRotationChangeCommand : RenderCommand
{
    public Guid guid;
    public Vector3 forward;
    public override void Execute()
    {
        base.Execute();
        var moveEntityRender = MoveEntityRenderManager.Instance.GetRender(guid);
        moveEntityRender.OnRotationChange(forward);
    }
}
