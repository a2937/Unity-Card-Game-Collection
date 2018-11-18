using UnityEngine;
using System.Collections;
using UnityEditor;

public static class CreateNewGameRules
{
    [MenuItem("Assets/Create/Game Rules")]
    public static void CreateAsset()
    {
        GameRules asset = GameRules.CreateInstance<GameRules>();

        AssetDatabase.CreateAsset(asset, "Assets/NewScripableObject.asset");
        AssetDatabase.SaveAssets();

        EditorUtility.FocusProjectWindow();

        Selection.activeObject = asset;
    }
  
}

