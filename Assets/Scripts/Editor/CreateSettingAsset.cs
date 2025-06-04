using UnityEditor;
using UnityEngine;

public static class CreateSettingsAsset
{
    [MenuItem("Assets/Create/FlowFieldPathfindingSetting/WorldSetting", false, 100)]
    public static void CreateWorldSettingAsset()
    {
        CreateAsset<WorldSetting>("WorldSetting");
    }
    
    [MenuItem("Assets/Create/FlowFieldPathfindingSetting/AgentSetting", false, 100)]
    public static void CreateAgentSettingAsset()
    {
        CreateAsset<AgentSetting>("AgentSetting");
    }

    static void CreateAsset<T>(string defaultName) where T : ScriptableObject
    {
        T settings = ScriptableObject.CreateInstance<T>();
        
        string path = AssetDatabase.GenerateUniqueAssetPath($"Assets/settings/{defaultName}_new.asset");
        AssetDatabase.CreateAsset(settings, path);
        AssetDatabase.SaveAssets();
        
        EditorUtility.FocusProjectWindow();
        Selection.activeObject = settings;
    }
}