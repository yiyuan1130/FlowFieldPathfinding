using System;
using UnityEngine;

public class Wall : GameEntity
{
    public Vector3 start;
    public Vector3 end;
    public Vector3 normal;
    public Wall(Vector3 start, Vector3 end) : base()
    {
        this.start = start;
        this.end = end;
        this.normal = Vector3.Cross(Vector3.up, (this.end - this.start));
    }

    public override void OnCreate()
    {
        base.OnCreate();
        RenderCommandManager.Instance.SendCommand(new CreateRenderCommand(){guid = guid, renderType = RenderType.Wall});
    }

    public void DoWander()
    {
    }
}
