using System;
using System.Runtime.CompilerServices;
using TMPro;
using UnityEngine;

public class CellRender : BaseRender
{
    static Color wallColor = Color.black;
    static Color targetColor = Color.green;
    static Color walkableColor = new Color(1, 0f, 0f, 0.8f); // 红色半透明
    private static Color nearTargetColor = new Color(1, 0f, 0f, 0.8f);
    private static Color farTargetColor = new Color(0.3f, 0f, 0f, 0.5f);
    private float distanceRate = 0f;
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
        distanceText.gameObject.SetActive(false);
    }

    public void OnCellTypeChange(CellType cellType)
    {
        bg.color = cellType switch
        {
            CellType.Obstacle => wallColor,
            CellType.Target => targetColor,
            CellType.Walkable => Color.Lerp(nearTargetColor, farTargetColor, this.distanceRate)
        };
    }

    public void OnCellVectorChange(Vector3 vector)
    {
        if (vector.x == 0 && vector.z == 0)
        {
            arrowTransform.gameObject.SetActive(false);
        }
        else
        { 
            arrowTransform.gameObject.SetActive(true);
            arrowTransform.forward = vector;
        }
    }

    public void OnCellDistanceChange(int distance)
    {
        if (distance > 0)
        {
            distanceText.gameObject.SetActive(true);
            distanceText.text = distance.ToString();
        }
        else
        {
            distanceText.gameObject.SetActive(false);
            // distanceText.text = "-1";
        }
    }
    
    public void OnCellDistanceRateChange(float distanceRate)
    {
        this.distanceRate = distanceRate;
        bg.color = Color.Lerp(nearTargetColor, farTargetColor, this.distanceRate);
    }
}