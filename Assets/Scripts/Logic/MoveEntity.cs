using System;
using UnityEngine;

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

    
    public MoveEntity() : base()
    {
        steeringBehaviour = new SteeringBehaviour(this);
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
}
