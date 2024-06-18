using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEditor.SceneManagement;
using UnityEngine;
using UnityEngine.SceneManagement;

public class ShortcutButton : MonoBehaviour
{
    [ToolbarLeft]
    public static void OpenSceneByName(string sceneName)
    {
        Scene targetScene = SceneManager.GetSceneByName(sceneName);

        int sceneCount = SceneManager.sceneCountInBuildSettings;
        string scenePath = "";
        for (int i = 0; i < sceneCount; i++)
        {
            scenePath = SceneUtility.GetScenePathByBuildIndex(i);
            string _sceneName = System.IO.Path.GetFileNameWithoutExtension(scenePath);
            if (_sceneName == sceneName)
                break;
        }
        if (!targetScene.isLoaded)
        {
            EditorSceneManager.SaveScene(SceneManager.GetActiveScene());
            EditorSceneManager.OpenScene(scenePath, OpenSceneMode.Single);
        }
    }

    [ToolbarRight]
    public static void OpenScript()
    {
        AssetDatabase.OpenAsset(AssetDatabase.LoadAssetAtPath<DefaultAsset>("Assets/Game Folder/Scripts"));
    }
    [ToolbarRight]
    public static void OpenAssets()
    {
        AssetDatabase.OpenAsset(AssetDatabase.LoadAssetAtPath<DefaultAsset>("Assets/Game Folder/Assets"));
    }
    [ToolbarRight]
    public static void OpenPrefab()
    {
        AssetDatabase.OpenAsset(AssetDatabase.LoadAssetAtPath<DefaultAsset>("Assets/Game Folder/Prefabs"));
    }
    [ToolbarRight]
    public static void OpenScriptableObject()
    {
        AssetDatabase.OpenAsset(AssetDatabase.LoadAssetAtPath<DefaultAsset>("Assets/Game Folder/ScriptableObject"));
    }
}
