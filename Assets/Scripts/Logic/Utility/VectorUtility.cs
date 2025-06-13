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
    
    public static bool LineIntersectsWall(Vector3 lineStart, Vector3 lineEnd, Vector3 wallStart, Vector3 wallEnd, Vector3 wallNormal, out Vector3 intersection, out float depth)
    {
        intersection = Vector3.zero;
        depth = 0f;

        // 投影到XZ平面
        Vector2 p1 = new Vector2(lineStart.x, lineStart.z);
        Vector2 p2 = new Vector2(lineEnd.x, lineEnd.z);
        Vector2 q1 = new Vector2(wallStart.x, wallStart.z);
        Vector2 q2 = new Vector2(wallEnd.x, wallEnd.z);

        Vector2 r = p2 - p1;
        Vector2 s = q2 - q1;

        float rxs = r.x * s.y - r.y * s.x;
        if (Mathf.Approximately(rxs, 0f)) return false; // 平行或共线，不能相交

        Vector2 qp = q1 - p1;
        float t = (qp.x * s.y - qp.y * s.x) / rxs;
        float u = (qp.x * r.y - qp.y * r.x) / rxs;

        if (t < 0 || t > 1 || u < 0 || u > 1) return false; // 没有在线段上相交

        // 得到交点（XZ）
        Vector2 ip = p1 + t * r;
        intersection = new Vector3(ip.x, 0, ip.y);

        // 计算穿透深度
        Vector3 lineDir = (lineEnd - lineStart).normalized;
        float distanceToIntersection = Vector3.Distance(lineStart, intersection);
        depth = -Vector3.Dot(lineDir, wallNormal.normalized) * distanceToIntersection;

        return true;
    }



}
