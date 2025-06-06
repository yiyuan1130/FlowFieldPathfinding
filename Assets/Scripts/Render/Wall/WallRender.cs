using System;
using System.Runtime.CompilerServices;
using TMPro;
using UnityEngine;

public class WallRender : BaseRender
{
    public WallRender(Guid guid) : base(guid)
    {
    }

    private Transform model;
    private Transform arrowTransform;
    
    public override void OnCreate()
    {
        base.OnCreate();
        GameObject wallPrefab = AssetManager.Instance.Load<GameObject>("Assets/Prefabs/Wall.prefab");
        gameObject = GameObject.Instantiate(wallPrefab);
        transform = gameObject.transform;
        model = transform.Find("model");
        arrowTransform = transform.Find("arrow");
        ShowWall();
    }

    void ShowWall()
    {
        Wall wall = WallManager.Instance.GetWall(this.guid);
        Vector3 center = (wall.start + wall.end) * 0.5f;
        transform.position = center;
        transform.forward = wall.normal;
        float dis2Scale = Vector3.Distance(wall.start, wall.end) + 1f;
        model.localScale = new Vector3(dis2Scale, 0.5f, 1f);
    }
}