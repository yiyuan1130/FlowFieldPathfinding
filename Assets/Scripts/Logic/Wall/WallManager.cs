using System;
using System.Collections.Generic;
using UnityEngine;

public class WallManager : BaseManager<WallManager>
{
    private Dictionary<Guid, Wall> walls;
    private List<Guid> removeList;

    public override void Init()
    {
        base.Init();
        walls = new Dictionary<Guid, Wall>();
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
                if (walls.ContainsKey(guid))
                {
                    walls[guid].OnClose();
                    walls.Remove(guid);
                }
            }
            removeList.Clear();
        }

        foreach (var item in walls)
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
        return walls[guid];
    }

    public Wall CreateWall(Vector3 start, Vector3 end)
    {
        var agent = new Wall(start, end);
        walls.Add(agent.guid, agent);
        agent.OnCreate();
        return agent;
    }

    public void DestroyWall(Guid guid)
    {
        removeList.Add(guid);
    }

    public Dictionary<Guid, Wall> GetAllWall()
    {
        return walls;
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
