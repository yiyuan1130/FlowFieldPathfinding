
public interface IRenderCommand
{
    public void Execute();
}

public class RenderCommand : IRenderCommand
{
    public RenderCommandType renderCommandType;
    public virtual void Execute()
    {
        
    }
}