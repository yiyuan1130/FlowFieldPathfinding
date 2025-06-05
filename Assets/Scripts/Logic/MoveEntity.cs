using System;
using UnityEngine;

public struct MoveEntitySettingData
{
    public float radius;
    public float maxSpeed;
    public float mass;
    public int arriveDeceleration;
    public float wanderRadius;
    public float wanderDistance;
    public float wanderJitter;
}

public class MoveEntity : GameEntity
{
    private SteeringBehaviour steeringBehaviour;
    private Vector3 forward;
    public Vector3 Forward { get => forward; }
    private Vector3 right;
    public Vector3 Right { get => right; }
    private Vector3 position;
    public Vector3 Position { get => position; }
    private Vector3 velocity;
    public Vector3 Velocity { get => velocity; }
    private float maxSpeedSqr;
    private float radius;
    public float Radius { get => radius; }
    private float maxForce;
    private float mass;
    private float maxSpeed;
    public float MaxSpeed { get => maxSpeed; }
    
    int arriveDeceleration;
    public int ArriveDeceleration { get => arriveDeceleration; }
    float wanderRadius;
    public float WanderRadius { get => wanderRadius; }
    float wanderDistance;
    public float WanderDistance { get => wanderDistance; }
    float wanderJitter;
    public float WanderJitter { get => wanderJitter; }
    
    bool isWanderOn = false;
    public bool IsWanderOn { get => isWanderOn; }
    bool isPursuitOn = false;
    public bool IsPursuitOn { get => isPursuitOn; }
    bool isWallAvoidanceOn = false;
    public bool IsWallAvoidanceOn { get => isWallAvoidanceOn; }
    bool isObstacleAvoidanceOn = false;
    public bool IsObstacleAvoidanceOn { get => isObstacleAvoidanceOn; }

    
    public MoveEntity(Vector3 initPosition, Vector3 initDirection, MoveEntitySettingData settingData) : base()
    {
        steeringBehaviour = new SteeringBehaviour(this);
        this.position = initPosition;
        this.forward = initDirection.normalized;
        this.right = Vector3.Cross(Vector3.up, this.forward).normalized;
        this.velocity = Vector3.zero;
        ApplySetting(settingData);
    }

    void ApplySetting(MoveEntitySettingData settingData = default)
    {
        this.radius = settingData.radius;
        this.maxSpeed = settingData.maxSpeed;
        this.mass = settingData.mass;
        this.arriveDeceleration = settingData.arriveDeceleration;
        this.wanderRadius = settingData.wanderRadius;
        this.wanderDistance = settingData.wanderDistance;
        this.wanderJitter = settingData.wanderJitter;
    }

    void Init()
    {
        maxSpeedSqr = maxSpeed * maxSpeed;
    }

    public virtual void OnCreate()
    {
        base.OnCreate();
    }

    public virtual void OnUpdate(float deltaTime)
    {
        base.OnUpdate(deltaTime);
        var force = steeringBehaviour.Calculate();
        var acceleration = force / mass;
        velocity += acceleration * deltaTime;
        TruncateMaxVelocity();
        position += velocity * deltaTime;
        if (velocity.sqrMagnitude > 0.0001f)
        {
            this.forward = velocity.normalized;
            this.right = Vector3.Cross(Vector3.up, this.forward).normalized;
            RenderCommandManager.Instance.SendCommand(new MoveEntityPositionChangeCommand{guid = this.guid, position = this.position});
            RenderCommandManager.Instance.SendCommand(new MoveEntityRotationChangeCommand{guid = this.guid, forward = this.forward});
        }
    }

    void TruncateMaxVelocity()
    {
        if (velocity.sqrMagnitude > maxSpeedSqr)
        {
            velocity = velocity.normalized * maxSpeed;
        }
    }

    public virtual void OnClose()
    {
    }

    public void SetWander(bool isOn)
    {
        isWanderOn = isOn;
    }
}
