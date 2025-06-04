using System;
using System.Collections.Generic;

public class AgentManager : BaseManager<AgentManager>
{
    private Dictionary<Guid, Agent> agents;
    private List<Guid> removeList;

    public override void Init()
    {
        base.Init();
        agents = new Dictionary<Guid, Agent>();
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

    public Agent GetAgent(Guid guid)
    {
        return agents[guid];
    }

    public void CreateAgent()
    {
    }

    public void DestroyAgent(Guid guid)
    {
        removeList.Add(guid);
    }

    void AddListeners()
    {
        // Add event listeners here if needed
    }
    
    void RemoveListeners()
    {
        // Remove event listeners here if needed
    }
}
