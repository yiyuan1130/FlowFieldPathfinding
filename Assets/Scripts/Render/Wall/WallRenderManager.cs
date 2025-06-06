using System;
using System.Collections.Generic;
using UnityEngine;

public class WallRenderManager : BaseManager<WallRenderManager>
{
    private Dictionary<Guid, WallRender> wallRenders;
    public Transform rootTransform;
    public override void Init()
    {
        base.Init();
        rootTransform = new GameObject("WallRenderRoot").transform;
        wallRenders = new Dictionary<Guid, WallRender>();
    }

    public override void Update(float deltaTime)
    {
        base.Update(deltaTime);
    }

    public IRender CreateRender(Guid guid)
    {
        WallRender wallRender = new WallRender(guid);
        wallRender.OnCreate();
        wallRender.transform.SetParent(rootTransform);
        wallRenders.Add(guid, wallRender);
        return wallRender;
    }

    public WallRender GetWallRender(Guid guid)
    {
        return wallRenders[guid];
    }

    public void Close()
    {
        foreach (var item in wallRenders)
        {
            item.Value.OnClose();
        }
        wallRenders.Clear();
        base.Close();
    }
}
