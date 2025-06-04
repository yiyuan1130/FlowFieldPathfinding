using UnityEngine;

public class AgentSetting : ScriptableObject
{
    [Header("Normal Setting")]
    public float radius = 0.5f; // 代理的半径
    public float maxSpeed = 3.0f; // 代理的最大速度
    public float mass = 1.0f; // 代理的质量
    
    [Header("Steering Behaviour : Arrive")]
    [Range(1, 3)]
    [Header("到达目标时的减速系数")]
    public int arriveDeceleration = 2;
    
    [Header("Steering Behaviour : Wander")]
    [Header("圆半径")]
    public float wanderRadius = 1.0f;
    [Header("圆距离")]
    public float wanderDistance = 2.0f;
    [Header("随机最大值")]
    public float wanderJitter = 0.2f;
}
