using System;
using System.Collections.Generic;
using UnityEngine;

public class CellManager : BaseManager<CellManager>
{
    private Dictionary<Guid, Cell> cells;
    public override void Init()
    {
        base.Init();
        cells = new Dictionary<Guid, Cell>();
        AddListeners();
    }

    public override void Update(float deltaTime)
    {
        base.Update(deltaTime);
    }

    public Cell CreateCell(Vector2Int index, Vector3 position, Vector3 size)
    {
        var cell = new Cell(index, position, size);
        cells.Add(cell.guid, cell);
        cell.OnCreate();
        return cell;
    }

    public Cell GetCell(Guid guid)
    {
        return cells[guid];
    }
    
    public Cell GetCellByPosition(Vector3 position)
    {
        var localPosition = position - FlowField.GetInstance().offset;
        int x = Mathf.RoundToInt(localPosition.x);
        int y = Mathf.RoundToInt(localPosition.z);
        if (x >= 0 && x < FlowField.GetInstance().width && y >= 0 && y < FlowField.GetInstance().height)
        {
            return FlowField.GetInstance().cells[x, y];
        }

        return null;
    }

    public override void Close()
    {
        RemoveListeners();
        foreach (var item in cells)
        {
            var cell = item.Value;
            cell.OnClose();
        }
        cells.Clear();
        base.Close();
    }
    
    public void AddListeners()
    {
        EventManager.Instance.AddListener(EventType.OnClickCell, Instance, OnClickCell);
    }

    public void RemoveListeners()
    {
        EventManager.Instance.RemoveListener(EventType.OnClickCell, Instance, OnClickCell);
    }
    
    # region events

    void OnClickCell(object data)
    {
        var tuple = data as Tuple<Guid, bool, bool>;
        Guid guid = tuple.Item1;
        bool leftClick = tuple.Item2;
        bool rightClick = tuple.Item3;
        Cell cell = GetCell(guid);
        if (leftClick)
        {
            cell.SelectAsObstacle();
        }
        else if (rightClick)
        {
            cell.SelectAsTarget();
        }
    }

    # endregion
}
