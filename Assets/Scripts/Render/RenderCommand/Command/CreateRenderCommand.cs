using System;

public enum RenderType
{
    Cell,
    Agent,
    Wall,
}

public class CreateRenderCommand : RenderCommand
{
    public RenderType renderType;
    public Guid guid;
    public override void Execute()
    {
        base.Execute();
        switch (renderType)
        {
            case RenderType.Cell:
                CellRenderManager.Instance.CreateRender(guid);
                break;
            case RenderType.Agent:
                AgentRenderManager.Instance.CreateRender(guid);
                break;
            case RenderType.Wall:
                WallRenderManager.Instance.CreateRender(guid);
                break;
        }
    }
}
