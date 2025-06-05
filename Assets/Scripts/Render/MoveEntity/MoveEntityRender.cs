using System;
using UnityEngine;

public class MoveEntityRender : BaseRender
{
    public MoveEntityRender(Guid guid) : base(guid)
    {
    }

    public override void OnCreate()
    {
        base.OnCreate();
    }

    public override void OnUpdate(float deltaTime)
    {
        base.OnUpdate(deltaTime);
    }

    public override void OnClose()
    {
        base.OnClose();
    }
    
    public void OnPositionChange(Vector3 position)
    {
        transform.position = position;
    }

    public void OnRotationChange(Vector3 forward)
    {
        transform.forward = forward;
    }
}
