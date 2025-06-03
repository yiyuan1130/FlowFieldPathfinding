using System;
using System.Collections.Generic;
using System.Runtime.CompilerServices;

public class EventManager : BaseManager<EventManager>
{
    Dictionary<EventType, Dictionary<object, List<Action<object>>>> eventDic;

    public override void Init()
    {
        base.Init();
        eventDic = new Dictionary<EventType, Dictionary<object, List<Action<object>>>>();
    }

    public override void Update(float deltaTime)
    {
        base.Update(deltaTime);
    }

    public override void Close()
    {
        eventDic.Clear();
        base.Close();
    }

    public void AddListener(EventType eventType, object listener, Action<object> callback)
    {
        if (!eventDic.TryGetValue(eventType, out var listeners))
        {
            listeners = new Dictionary<object, List<Action<object>>>();
            eventDic[eventType] = listeners;
        }
        
        if (!listeners.TryGetValue(listener, out var actions))
        {
            actions = new List<Action<object>>();
            listeners[listener] = actions;
        }
        
        actions.Add(callback);
    }

    public void RemoveListener(EventType eventType, object listener, Action<object> callback)
    {
        if (eventDic.TryGetValue(eventType, out var listeners))
        {
            if (listeners.TryGetValue(listener, out var actions))
            {
                actions.Remove(callback);
            }
        }
    }
    
    public void RemoveListener(EventType eventType, object listener)
    {
        if (eventDic.TryGetValue(eventType, out var listeners))
        {
            if (listeners.TryGetValue(listener, out var actions))
            {
                listeners.Remove(listener);
            }
        }
    }
    
    public void RemoveListener(EventType eventType)
    {
        if (eventDic.TryGetValue(eventType, out var listeners))
        {
            eventDic.Remove(eventType);
        }
    }

    public void SendEvent(EventType eventType, object data = null)
    {
        if (eventDic.TryGetValue(eventType, out var listeners))
        {
            foreach (var listener in listeners)
            {
                foreach (var action in listener.Value)
                {
                    action.Invoke(data);
                }
            }
        }
    }
}
