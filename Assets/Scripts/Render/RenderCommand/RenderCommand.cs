
public interface IRenderCommand
{
    public void Execute();
}

public class RenderCommand : IRenderCommand
{
    public virtual void Execute()
    {
        
    }
}