using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEditor;
using UnityEngine;
using UnityEngine.UI;

public class LoadWorldPropertiesWindow : EditorWindow
{
    public WorldGeneratorWindow refWin;
    Vector2 scroll = new Vector2();


    void OnGUI()
    {
        scroll = EditorGUILayout.BeginScrollView(scroll, GUILayout.ExpandHeight(true), GUILayout.ExpandWidth(true));

        WorldProperties[] props = GetAssetsAtPath<WorldProperties>("Assets/LevelDesignSettings");

       for (int i = 0; i < props.Length; i++)
        {
            if (GUILayout.Button(props[i].name))
            {
                refWin.LoadProperties(props[i]);
                this.Close();
            }
        }

        EditorGUILayout.EndScrollView();
    }


    public static T[] GetAssetsAtPath<T>(string path) where T : Object
    {
        var assets = AssetDatabase.FindAssets($"t:{typeof(T).Name}", new[] { path });
        List<T> foundAssets = new List<T>();

        foreach (var guid in assets)
        {
            foundAssets.Add(AssetDatabase.LoadAssetAtPath<T>(AssetDatabase.GUIDToAssetPath(guid)));
        }

        // if you want to skip the convertion to array, simply change method return type
        return foundAssets.ToArray();
    }
}
