
using UnityEngine;

public class SteeringBehaviour
{
    private MoveEntity moveEntity;

    public SteeringBehaviour(MoveEntity moveEntity)
    {
        this.moveEntity = moveEntity;
    }

    public Vector3 Calculate()
    {
        Vector3 force = Vector3.zero;
        if (this.moveEntity.IsWanderOn)
        {
            force += Wander() * 0.3f;
            force += WallAvoidance() * 0.7f;
        }
        return force;
    }

    public Vector3 Seek(Vector3 target)
    {
        Vector3 desiredVelocity = (target - moveEntity.Position).normalized * moveEntity.MaxSpeed;
        return desiredVelocity - moveEntity.Velocity;
    }

    public Vector3 Flee(Vector3 target)
    {
        Vector3 desiredVelocity = (moveEntity.Position - target).normalized * moveEntity.MaxSpeed;
        return desiredVelocity - moveEntity.Velocity;
    }

    // deceleration: 1 - slow, 2 - normal, 3 - fast
    public Vector3 Arrive(Vector3 target, int deceleration = 1)
    {
        Vector3 toTarget = moveEntity.Position - target;
        float distance = toTarget.magnitude;
        if (distance > 0)
        {
            float rate = 0.33333f;
            float speed = distance / (rate * deceleration);
            speed = Mathf.Min(speed, moveEntity.MaxSpeed);
            Vector3 desiredVelocity = toTarget.normalized * speed;
            return desiredVelocity - moveEntity.Velocity;
        }
        return Vector3.zero;
    }
    
    public Vector3 Pursuit(MoveEntity evader)
    {
        Vector3 toTarget = evader.Position - moveEntity.Position;
        float relativeHeading = Vector3.Dot(moveEntity.Forward, evader.Forward);
        if (Vector3.Dot(toTarget, moveEntity.Forward) > 0 && relativeHeading < -0.95f)
        {
            // 对向且角度再正前方18度内(cos18度约为0.95)，直接追踪目标位置
            return Seek(evader.Position);
        }
        
        // 跟距离正比，跟target速度反比
        float lookAheadTime = toTarget.magnitude / (moveEntity.MaxSpeed + evader.Velocity.magnitude);
        // 预测一下evader的位置，朝预测位置seek
        return Seek(evader.Position + evader.Velocity * lookAheadTime);
    }
    
    public Vector3 Evade(MoveEntity pursuer)
    {
        Vector3 toTarget = pursuer.Position - moveEntity.Position;
        // 跟距离正比，跟target速度反比
        float lookAheadTime = toTarget.magnitude / (moveEntity.MaxSpeed + pursuer.Velocity.magnitude);
        // 预测一下pursuer的位置，朝预测位置flee
        return Flee(pursuer.Position + pursuer.Velocity * lookAheadTime);
    }

    private Vector3 wanderTargetLocal;
    public Vector3 Wander()
    {
        wanderTargetLocal += new Vector3(Random.Range(-moveEntity.WanderJitter, moveEntity.WanderJitter), 0.0f, Random.Range(-moveEntity.WanderJitter, moveEntity.WanderJitter));
        wanderTargetLocal.Normalize();
        wanderTargetLocal *= moveEntity.WanderRadius;
        Vector3 wanderTargetWorld = VectorUtility.LocalToWorldPosition(wanderTargetLocal, moveEntity.Position, moveEntity.Forward, moveEntity.Right);
        Vector3 targetWorld = moveEntity.Forward * moveEntity.WanderDistance + wanderTargetWorld;
        return targetWorld - moveEntity.Position;
    }
    
    public Vector3 ObstacleAvoidance(Vector3[] obstacles)
    {
        return Vector3.zero;
    }

    private Vector3[] fleesDirs = new Vector3[3];
    private float[] fleesLeghths = new float[3] {2.5f, 2.5f, 2.5f};
    public Vector3 WallAvoidance()
    {
        fleesDirs[0] = Quaternion.Euler(0, -45, 0) * this.moveEntity.Forward; // 左
        fleesDirs[1] = this.moveEntity.Forward; // 前
        fleesDirs[2] = Quaternion.Euler(0, +45, 0) * this.moveEntity.Forward; // 右
        // for (int i = 0; i < fleesDirs.Length; i++)
        // {
        //     RenderCommandManager.Instance.SendCommand(new DrawAgentFleesCommand(){guid = this.moveEntity.guid, fleeDirs = fleesDirs, lengths = fleesLeghths});
        // }
        Vector3 force = Vector3.zero;
        var walls = WallManager.Instance.GetAllWall();
        foreach (var item in walls)
        {
            Wall wall = item.Value;

            for (int i = 0; i < fleesDirs.Length; i++)
            {
                Vector3 lineStart = this.moveEntity.Position;
                Vector3 lineEnd = this.moveEntity.Position + fleesDirs[i] * fleesLeghths[i];
                if (VectorUtility.LineIntersectsWall(lineStart, lineEnd, wall.start, wall.end, wall.normal, out Vector3 hitPoint, out float depth))
                {
                    force += wall.normal * depth * 50f;
                }
            }
        }
        return force;
    }
}
