public static class LogicManagerController
{
    public static void Init()
    {
        EventManager.Instance.Init();
        InputManager.Instance.Init();
        CellManager.Instance.Init();
    }

    public static void Update(float deltaTime)
    {
        CellManager.Instance.Update(deltaTime);
        InputManager.Instance.Update(deltaTime);
        EventManager.Instance.Update(deltaTime);
    }

    public static void Close()
    {
        InputManager.Instance.Close();
        CellManager.Instance.Close();
        EventManager.Instance.Close();
    }
    
}
