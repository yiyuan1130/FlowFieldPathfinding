using UnityEditor;

public class AssetManager : BaseManager<AssetManager>
{
    public T Load<T>(string path) where T : UnityEngine.Object
    {
        return AssetDatabase.LoadAssetAtPath<T>(path);
    }
}
