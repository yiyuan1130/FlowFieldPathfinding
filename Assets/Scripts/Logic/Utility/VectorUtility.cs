using UnityEngine;

public static class VectorUtility
{
    // 根据worldPoint和从这点上的forward、right转换localPoint为世界坐标
    public static Vector3 LocalToWorldPosition(Vector3 localPoint, Vector3 basePoint, Vector3 forward, Vector3 right)
    {
        Vector3 x = forward;
        Vector3 z = right;
        Vector3 y = Vector3.Cross(x, z).normalized;
        return basePoint + x * localPoint.x + y * localPoint.y + z * localPoint.z;
    }

    public static Vector3 WorldToLocalPosition(Vector3 worldPoint, Vector3 basePoint, Vector3 forward, Vector3 right)
    {
        Vector3 vector = worldPoint - basePoint;
        Vector3 x = forward;
        Vector3 z = right;
        Vector3 y = Vector3.Cross(x, z).normalized;
        float xLocal = Vector3.Dot(vector, x);
        float yLocal = Vector3.Dot(vector, y);
        float zLocal = Vector3.Dot(vector, z);
        return new Vector3(xLocal, yLocal, zLocal);
    }
}
