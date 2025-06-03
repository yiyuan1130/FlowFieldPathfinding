using System;
using UnityEngine;

public class CellRender : BaseRender
{
    public CellRender(Guid guid) : base(guid)
    {
    }

    public override void OnCreate()
    {
        base.OnCreate();
        GameObject cellPrefab = AssetManager.Instance.Load<GameObject>("Assets/Prefabs/Cell.prefab");
        gameObject = GameObject.Instantiate(cellPrefab);
        transform = gameObject.transform;
        var cellData = CellManager.Instance.GetCell(guid);
        gameObject.name = $"Cell [{cellData.index}]";
        transform.position = cellData.position;
    }
}