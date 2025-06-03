using System.Collections.Generic;
public class RenderCommandManager : BaseManager<RenderCommandManager>
{
    private Queue<IRenderCommand> commandQueue;
    public override void Init()
    {
        base.Init();
        commandQueue = new Queue<IRenderCommand>();
    }

    public override void Update(float deltaTime)
    {
        base.Update(deltaTime);

        while (commandQueue.Count > 0)
        {
            var command = commandQueue.Dequeue();
            command.Execute();
        }
    }

    public override void Close()
    {
        commandQueue.Clear();
        base.Close();
    }

    public void SendCommand(RenderCommandType renderCommandType, RenderCommand command)
    {
        command.renderCommandType = renderCommandType;
        commandQueue.Enqueue(command);
    }
}
