using System.Collections.Generic;

public static class RenderManagerController
{
    private static List<IManager> managers = new List<IManager>()
    {
        RenderCommandManager.Instance,
        CellRenderManager.Instance,
        MoveEntityRenderManager.Instance,
        AgentRenderManager.Instance,
        WallRenderManager.Instance,
    };
    public static void Init()
    {
        for (int i = 0; i < managers.Count; i++)
        {
            managers[i].Init();
        }
    }

    public static void Update(float deltaTime)
    {
        for (int i = 0; i < managers.Count; i++)
        {
            managers[i].Update(deltaTime);
        }
    }

    public static void Close()
    {
        for (int i = 0; i < managers.Count; i++)
        {
            managers[i].Close();
        }
    }
}
