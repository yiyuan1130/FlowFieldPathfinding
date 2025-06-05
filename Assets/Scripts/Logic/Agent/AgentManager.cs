using System;
using System.Collections.Generic;
using UnityEngine;

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

    public Agent CreateAgent(Vector3 initPosition, Vector3 initDirection, MoveEntitySettingData settingData)
    {
        var agent = new Agent(initPosition, initDirection, settingData);
        agents.Add(agent.guid, agent);
        agent.OnCreate();
        return agent;
    }

    public void DestroyAgent(Guid guid)
    {
        removeList.Add(guid);
    }

    void AddListeners()
    {
        EventManager.Instance.AddListener(EventType.OnSpaceKeyDown, Instance, RandomCreateAgent);
    }
    
    void RemoveListeners()
    {
        EventManager.Instance.RemoveListener(EventType.OnSpaceKeyDown, Instance);
    }
    
    # region Event Handlers

    void RandomCreateAgent(object data)
    {
        MoveEntitySettingData settingData = (MoveEntitySettingData)data;
        Vector3 position = FlowField.GetInstance().RandomGetValidPosition();
        Agent agent = CreateAgent(position, Vector3.forward, settingData);
        agent.SetWander(true);
    }

    # endregion
}
