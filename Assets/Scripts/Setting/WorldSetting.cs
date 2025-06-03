using UnityEngine;

public class WorldSetting : ScriptableObject
{
    public Vector2Int worldSize = new Vector2Int(20, 20);
    public Vector3 cellSize = new Vector3(1.0f, 0.0f, 1.0f);
}
