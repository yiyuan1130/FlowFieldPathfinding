using System.Collections.Generic;

public static class LogicManagerController
{
    private static List<IManager> managers = new List<IManager>()
    {
        EventManager.Instance,
        InputManager.Instance,
        CellManager.Instance,
        AgentManager.Instance,
        WallManager.Instance,
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
