public static class RenderManagerController
{
    public static void Init()
    {
        RenderCommandManager.Instance.Init();
        CellRenderManager.Instance.Init();
        MoveEntityRenderManager.Instance.Init();
        AgentRenderManager.Instance.Init();
    }

    public static void Update(float deltaTime)
    {
        RenderCommandManager.Instance.Update(deltaTime);
        CellRenderManager.Instance.Update(deltaTime);
        MoveEntityRenderManager.Instance.Update(deltaTime);
        AgentRenderManager.Instance.Update(deltaTime);
    }

    public static void Close()
    {
        RenderCommandManager.Instance.Close();
        CellRenderManager.Instance.Close();
        MoveEntityRenderManager.Instance.Close();
        AgentRenderManager.Instance.Close();
    }
}
