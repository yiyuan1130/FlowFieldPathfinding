using System;
using System.Collections.Generic;
using UnityEngine;

public class AgentRenderManager : BaseManager<AgentRenderManager>
{
    private Dictionary<Guid, AgentRender> moveRenders;
    private List<Guid> destroyList;
    public Transform rootTransform;
    public override void Init()
    {
        base.Init();
        rootTransform = new GameObject("AgentRenderRoot").transform;
        moveRenders = new Dictionary<Guid, AgentRender>();
        destroyList = new List<Guid>();
    }

    public override void Update(float deltaTime)
    {
        base.Update(deltaTime);
        if (destroyList.Count > 0)
        {
            for (int i = destroyList.Count - 1; i >= 0; i--)
            {
                AgentRender moveEntityRender = GetAgentRender(destroyList[i]);
                moveEntityRender.OnClose();
                Guid guid = destroyList[i];
                moveRenders.Remove(guid);
                MoveEntityRenderManager.Instance.RemoveRender(guid);
            }
            destroyList.Clear();
        }
        
        foreach (var item in moveRenders)
        {
            item.Value.OnUpdate(deltaTime);
        }
    }

    public IRender CreateRender(Guid guid)
    {
        AgentRender moveEntityRender = new AgentRender(guid);
        moveEntityRender.OnCreate();
        moveEntityRender.transform.SetParent(rootTransform);
        moveRenders.Add(guid, moveEntityRender);
        MoveEntityRenderManager.Instance.AddRender(guid, moveEntityRender);
        return moveEntityRender;
    }

    public void DestroyRender(Guid guid)
    {
        destroyList.Add(guid);
    }

    public AgentRender GetAgentRender(Guid guid)
    {
        return moveRenders[guid];
    }

    public void Close()
    {
        foreach (var item in moveRenders)
        {
            item.Value.OnClose();
        }
        moveRenders.Clear();
        base.Close();
    }
}
