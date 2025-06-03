public static class LogicManagerController
{
    public static void Init()
    {
        EventManager.Instance.Init();
        CellManager.Instance.Init();
    }

    public static void Update(float deltaTime)
    {
        CellManager.Instance.Update(deltaTime);
        EventManager.Instance.Update(deltaTime);
    }

    public static void Close()
    {
        CellManager.Instance.Close();
        EventManager.Instance.Close();
    }
    
}
