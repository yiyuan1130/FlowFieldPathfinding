using System;
using UnityEngine;

public class BaseRender : IRender
{
    public Guid guid;
    public GameObject gameObject;
    public Transform transform;

    public BaseRender(Guid guid)
    {
        this.guid = guid;
    }

    public virtual void OnCreate()
    {
        
    }

    public virtual void OnUpdate(float deltaTime)
    {
        
    }

    public virtual void OnClose()
    {
        
    }
}
