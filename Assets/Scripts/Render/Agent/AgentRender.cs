using System;
using UnityEngine;

public class AgentRender : MoveEntityRender
{
    public AgentRender(Guid guid) : base(guid)
    {
    }

    public override void OnCreate()
    {
        base.OnCreate();
        GameObject agentPrefab = AssetManager.Instance.Load<GameObject>("Assets/Prefabs/Agent.prefab");
        gameObject = GameObject.Instantiate(agentPrefab);
        transform = gameObject.transform;
    }

    public override void OnUpdate(float deltaTime)
    {
        base.OnUpdate(deltaTime);
    }

    public override void OnClose()
    {
        base.OnClose();
    }
}
