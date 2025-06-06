using System;
using System.Collections.Generic;
using UnityEngine;

public class WallManager : BaseManager<WallManager>
{
    private Dictionary<Guid, Wall> agents;
    private List<Guid> removeList;

    public override void Init()
    {
        base.Init();
        agents = new Dictionary<Guid, Wall>();
        removeList = new List<Guid>();
        AddListeners();
    }

    public override void Update(float deltaTime)
    {
        base.Update(deltaTime);
        if (removeList.Count > 0)
        {
            for (int i = 0; i < removeList.Count; i++)
            {
                var guid = removeList[i];
                if (agents.ContainsKey(guid))
                {
                    agents[guid].OnClose();
                    agents.Remove(guid);
                }
            }
            removeList.Clear();
        }

        foreach (var item in agents)
        {
            item.Value.OnUpdate(deltaTime);
        }
    }

    public override void Close()
    {
        base.Close();
        RemoveListeners();
    }

    public Wall GetWall(Guid guid)
    {
        return agents[guid];
    }

    public Wall CreateWall(Vector3 start, Vector3 end)
    {
        var agent = new Wall(start, end);
        agents.Add(agent.guid, agent);
        agent.OnCreate();
        return agent;
    }

    public void DestroyWall(Guid guid)
    {
        removeList.Add(guid);
    }

    void AddListeners()
    {
    }
    
    void RemoveListeners()
    {
    }
    
    # region Event Handlers


    # endregion
}
