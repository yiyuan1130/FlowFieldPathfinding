using System;
using UnityEngine;

public class Agent : MoveEntity
{
    public Agent(Vector3 initPosition, Vector3 initDirection, MoveEntitySettingData settingData) : base(initPosition, initDirection, settingData)
    {
        
    }

    public override void OnCreate()
    {
        base.OnCreate();
        RenderCommandManager.Instance.SendCommand(new CreateRenderCommand(){guid = guid, renderType = RenderType.Agent});
        RenderCommandManager.Instance.SendCommand(new MoveEntityPositionChangeCommand(){guid = guid, position = this.Position});
        RenderCommandManager.Instance.SendCommand(new MoveEntityRotationChangeCommand(){guid = guid, forward = this.Forward});
    }

    public void DoWander()
    {
    }
}
