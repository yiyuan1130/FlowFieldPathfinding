using System;
using UnityEngine;

public struct InputData
{
    public Vector3 clickPosition;
    public bool leftMouseClick;
    public bool rightMouseClick;
    public bool spaceKeyDown;

    public void Reset()
    {
        clickPosition = Vector3.one * -99999;
        leftMouseClick = false;
        rightMouseClick = false;
        spaceKeyDown = false;
    }
}

public class InputManager : BaseManager<InputManager>
{
    private InputData inputData;
    
    public override void Init()
    {
        base.Init();
        inputData = new InputData();
    }
    
    public override void Update(float deltaTime)
    {
        base.Update(deltaTime);
        PollInput();
    }

    public override void Close()
    {
        base.Close();
    }
    
    public void OnRightMouseClick(Vector3 position)
    {
        inputData.rightMouseClick = true;
        inputData.clickPosition = position;
    }
    
    public void OnLeftMouseClick(Vector3 position)
    {
        inputData.leftMouseClick = true;
        inputData.clickPosition = position;
    }
    
    public void OnSpaceKeyDown()
    {
        inputData.spaceKeyDown = true;
    }

    void PollInput()
    {
        if (inputData.leftMouseClick || inputData.rightMouseClick)
        {
            Cell cell = CellManager.Instance.GetCellByPosition(inputData.clickPosition);
            if (cell != null)
            {
                Tuple<Guid, bool, bool> data = new Tuple<Guid, bool, bool>(cell.guid, inputData.leftMouseClick, inputData.rightMouseClick);
                EventManager.Instance.SendEvent(EventType.OnClickCell, data);
            }
        }

        inputData.Reset();
    }
}
