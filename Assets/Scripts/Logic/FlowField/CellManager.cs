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
    }

    public override void Update(float deltaTime)
    {
        base.Update(deltaTime);
    }

    public Cell CreateCell(Vector2Int index, Vector3 position)
    {
        var cell = new Cell(index, position);
        cells.Add(cell.guid, cell);
        cell.OnCreate();
        return cell;
    }

    public Cell GetCell(Guid guid)
    {
        return cells[guid];
    }

    public override void Close()
    {
        foreach (var item in cells)
        {
            var cell = item.Value;
            cell.OnClose();
        }
        cells.Clear();
        base.Close();
    }
}
