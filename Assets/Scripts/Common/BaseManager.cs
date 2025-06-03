public class BaseManager<T> : IManager where T : new()
{
    private static T instance;

    public static T Instance
    {
        get
        {
            if (instance == null)
            {
                instance = new T();
            }

            return instance;
        }
    }

    public virtual void Init()
    {
        
    }

    public virtual void Update(float deltaTime)
    {
        
    }

    public virtual void Close()
    {
        
    }
}
