using System;
using UnityEditor;
using UnityEngine;

public class AgentRender : MoveEntityRender
{
    private LineRenderer[] fleeLineRenders;
    public AgentRender(Guid guid) : base(guid)
    {
    }

    public override void OnCreate()
    {
        base.OnCreate();
        GameObject agentPrefab = AssetManager.Instance.Load<GameObject>("Assets/Prefabs/Agent.prefab");
        gameObject = GameObject.Instantiate(agentPrefab);
        transform = gameObject.transform;
        // InitFleeLineRender();
    }

    void InitFleeLineRender()
    {
        fleeLineRenders = new LineRenderer[3];
        for (int i = 0; i < 3; i++)
        {
            var lr = transform.Find($"flees/flee_{i}").GetComponent<LineRenderer>();
            lr.gameObject.SetActive(true);
            lr.positionCount = 2;
            lr.startWidth = 0.05f;
            lr.endWidth = 0.05f;
            lr.startColor = Color.red;
            lr.endColor = Color.red;
            fleeLineRenders[i] = lr;
        }
    }

    public override void OnUpdate(float deltaTime)
    {
        base.OnUpdate(deltaTime);
    }

    public void DrawFlees(Vector3[] fleeDirs, float[] lengths)
    {
        for (int i = 0; i < fleeDirs.Length; i++)
        {
            fleeLineRenders[i].SetPosition(0, transform.position);
            fleeLineRenders[i].SetPosition(1, transform.position + fleeDirs[i] * lengths[i]);
        }
    }

    public override void OnClose()
    {
        base.OnClose();
    }
}
