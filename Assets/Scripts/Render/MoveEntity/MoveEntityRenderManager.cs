using System;
using System.Collections.Generic;
using UnityEngine;

public class MoveEntityRenderManager : BaseManager<MoveEntityRenderManager>
{
    private Dictionary<Guid, MoveEntityRender> moveEntityRenders;
    public override void Init()
    {
        base.Init();
        moveEntityRenders = new Dictionary<Guid, MoveEntityRender>();
    }

    public override void Update(float deltaTime)
    {
        base.Update(deltaTime);
    }

    public void AddRender(Guid guid, MoveEntityRender moveEntityRender)
    {
        moveEntityRenders.Add(guid, moveEntityRender);
    }
    
    public void RemoveRender(Guid guid)
    {
        moveEntityRenders.Remove(guid);
    }

    public MoveEntityRender GetRender(Guid guid)
    {
        return moveEntityRenders[guid];
    }

    public void Close()
    {
        moveEntityRenders.Clear();
        base.Close();
    }
}
