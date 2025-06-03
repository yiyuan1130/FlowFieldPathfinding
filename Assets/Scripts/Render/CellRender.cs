using System;
using System.Runtime.CompilerServices;
using TMPro;
using UnityEngine;

public class CellRender : BaseRender
{
    public CellRender(Guid guid) : base(guid)
    {
    }

    private SpriteRenderer bg;
    private Transform arrowTransform;
    private TMP_Text distanceText;
    

    public override void OnCreate()
    {
        base.OnCreate();
        GameObject cellPrefab = AssetManager.Instance.Load<GameObject>("Assets/Prefabs/Cell.prefab");
        gameObject = GameObject.Instantiate(cellPrefab);
        transform = gameObject.transform;
        var cellData = CellManager.Instance.GetCell(guid);
        gameObject.name = $"Cell [{cellData.index}]";
        transform.position = cellData.position;

        bg = transform.Find("bg").GetComponent<SpriteRenderer>();
        
        OnCellTypeChange(CellType.Walkable);
        arrowTransform = transform.Find("arrow");
        arrowTransform.gameObject.SetActive(false);
        distanceText = transform.Find("distance_text").GetComponent<TMP_Text>();
        distanceText.text = "-1";
    }

    public void OnCellTypeChange(CellType cellType)
    {
        bg.color = cellType switch
        {
            CellType.Obstacle => Color.black,
            CellType.Target => Color.green,
            CellType.Walkable => Color.red
        };
    }
}