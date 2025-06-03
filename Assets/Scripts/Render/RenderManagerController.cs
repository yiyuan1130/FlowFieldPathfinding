public static class RenderManagerController
{
    public static void Init()
    {
        RenderCommandManager.Instance.Init();
        CellRenderManager.Instance.Init();
    }

    public static void Update(float deltaTime)
    {
        RenderCommandManager.Instance.Update(deltaTime);
        CellRenderManager.Instance.Update(deltaTime);
    }

    public static void Close()
    {
        RenderCommandManager.Instance.Close();
        CellRenderManager.Instance.Close();
    }
}
