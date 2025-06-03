using System;

public enum RenderType
{
    Cell,
    Agent,
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
        }
    }
}
