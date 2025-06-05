public static class Settings
{
    public static MoveEntitySettingData MoveEntitySettingData;
    
    public static void Initialize(WorldSetting worldSetting, AgentSetting agentSetting)
    {
        // AgentSetting
        MoveEntitySettingData = new MoveEntitySettingData()
        {
            radius = agentSetting.radius,
            maxSpeed = agentSetting.maxSpeed,
            mass = agentSetting.mass,
            arriveDeceleration = agentSetting.arriveDeceleration,
            wanderRadius = agentSetting.wanderRadius,
            wanderDistance = agentSetting.wanderDistance,
            wanderJitter = agentSetting.wanderJitter,
        };
    }
}
