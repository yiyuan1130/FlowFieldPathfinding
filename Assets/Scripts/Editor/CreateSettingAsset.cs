using UnityEditor;
using UnityEngine;

public static class CreateSettingsAsset
{
    [MenuItem("Assets/Create/FlowFieldPathfindingSetting/WorldSetting", false, 100)]
    public static void CreateAsset()
    {
        WorldSetting settings = ScriptableObject.CreateInstance<WorldSetting>();
        
        string path = AssetDatabase.GenerateUniqueAssetPath("Assets/settings/WorldSetting.asset");
        AssetDatabase.CreateAsset(settings, path);
        AssetDatabase.SaveAssets();
        
        EditorUtility.FocusProjectWindow();
        Selection.activeObject = settings;
    }
}