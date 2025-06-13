using System;
using UnityEngine;

public class DrawAgentFleesCommand : RenderCommand
{
    public Guid guid;
    public Vector3[] fleeDirs;
    public float[] lengths;
    public override void Execute()
    {
        base.Execute();
        var agentRender = AgentRenderManager.Instance.GetAgentRender(guid);
        agentRender.DrawFlees(fleeDirs, lengths);
    }
}
