using System;
using System.Collections.Generic;
using UnityEngine;

public class CellRenderManager : BaseManager<CellRenderManager>
{
    private Dictionary<Guid, CellRender> cellRenders;
    private List<Guid> destroyList;
    public Transform rootTransform;
    public override void Init()
    {
        base.Init();
        rootTransform = new GameObject("CellRenderRoot").transform;
        cellRenders = new Dictionary<Guid, CellRender>();
        destroyList = new List<Guid>();
    }

    public override void Update(float deltaTime)
    {
        base.Update(deltaTime);
        if (destroyList.Count > 0)
        {
            for (int i = destroyList.Count - 1; i >= 0; i--)
            {
                CellRender cellRender = GetCellRender(destroyList[i]);
                cellRender.OnClose();
                cellRenders.Remove(destroyList[i]);
            }
            destroyList.Clear();
        }
        
        foreach (var item in cellRenders)
        {
            item.Value.OnUpdate(deltaTime);
        }
    }

    public IRender CreateRender(Guid guid)
    {
        CellRender cellRender = new CellRender(guid);
        cellRender.OnCreate();
        cellRender.transform.SetParent(rootTransform);
        cellRenders.Add(guid, cellRender);
        return cellRender;
    }

    public CellRender GetCellRender(Guid guid)
    {
        return cellRenders[guid];
    }

    public void Close()
    {
        foreach (var item in cellRenders)
        {
            item.Value.OnClose();
        }
        cellRenders.Clear();
        base.Close();
    }
}
