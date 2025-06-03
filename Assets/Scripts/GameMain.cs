using System;
using UnityEngine;

public class GameMain : MonoBehaviour
{
    public WorldSetting worldSetting;
    private void Awake()
    {
        RenderManagerController.Init();
        LogicManagerController.Init();
    }

    private void Start()
    {
        var instance = FlowField.GetInstance(); 
        instance.GenerateWorld(worldSetting.worldSize);
    }

    private void Update()
    {
        RenderManagerController.Update(Time.deltaTime);
        LogicManagerController.Update(Time.deltaTime);
    }

    private void OnDestroy()
    {
        RenderManagerController.Close();
        LogicManagerController.Close();
    }
}
