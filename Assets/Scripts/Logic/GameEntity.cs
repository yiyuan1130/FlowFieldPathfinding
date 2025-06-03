using System;
using UnityEngine;

public class GameEntity
{
    public Guid guid;
    private bool created = false;

    public GameEntity()
    {
        this.guid = Guid.NewGuid();
    }

    public virtual void OnCreate()
    {
        if (created)
        {
            Debug.LogError($"Entity has already been created : {guid}");
            return;
        }
        created = true;
    }

    public virtual void OnUpdate(float deltaTime)
    {
    }

    public virtual void OnClose()
    {
    }
}
