using System;
using System.Collections;
using System.Collections.Generic;
using System.Text;
using UnityEditor;
using UnityEngine;
using UnityEngine.Rendering;

public class WorldGeneratorWindow : EditorWindow
{
    SerializedObject serializedObject;

    [MenuItem("Procedural/WorldEditor")]
    public static void ShowWindow()
    {
        var window = GetWindow<WorldGeneratorWindow>();
        window.titleContent = new GUIContent("World Editor");
        window.minSize = new Vector2(700, 500);
        window.maxSize = new Vector2(700, 500);

        WorldProperties _properties = ScriptableObject.CreateInstance<WorldProperties>();

        window.serializedObject = new SerializedObject(_properties);
    }

    private void OnGUI()
    {
        var labelStyle = new GUIStyle(GUI.skin.label) { alignment = TextAnchor.MiddleCenter };

        EditorGUILayout.BeginHorizontal("Box");

        EditorGUILayout.BeginVertical("Box", GUILayout.Width(350));

        EditorGUILayout.BeginHorizontal("Box");
        EditorGUILayout.LabelField("Noise Properties", labelStyle);
        EditorGUILayout.EndHorizontal();
        if (GUILayout.Button("Load Properties"))
        {
            var window = GetWindow<LoadWorldPropertiesWindow>();
            window.titleContent = new GUIContent("Saved Properties");
            window.maxSize = new Vector2(500, 225);
            window.minSize = window.maxSize;
            window.refWin = this;
            window.Show();
        }
        DrawNoiseProperties();

        EditorGUILayout.EndVertical();

        EditorGUILayout.BeginVertical("Box", GUILayout.Width(350));

        EditorGUILayout.BeginHorizontal("Box");
        EditorGUILayout.LabelField("Noise Visualizer", labelStyle);
        EditorGUILayout.EndHorizontal();
        DrawNoiseVisualizer(GetWorldProperties(serializedObject));

        EditorGUILayout.EndVertical();

        EditorGUILayout.EndHorizontal();
        if (GUILayout.Button("Generate Properties", GUILayout.ExpandWidth(true), GUILayout.ExpandHeight(true)))
        {
            WorldProperties asset = GetWorldProperties(serializedObject);

            StringBuilder sb = new StringBuilder("", 50);
            sb.AppendFormat("{0}x{1}_Seed{2}_Scale{3}_O{4}_P{5}_L{6}_T{7}", asset.mapWidth, asset.mapHeight, asset.seed, asset.noiseScale, asset.octaves,
                                                                                                                                asset.persistance, asset.lacunarity, asset.threshold);

            AssetDatabase.CreateAsset(asset, "Assets/LevelDesignSettings/" + sb.ToString() + ".asset");
            AssetDatabase.SaveAssets();
        }
    }

    private void DrawNoiseVisualizer(WorldProperties prop)
    {
        int mapHeight = prop.mapHeight;
        int mapWidth = prop.mapWidth;
        Texture2D texture = new Texture2D(mapWidth, mapHeight);
        Color[] colourMap = new Color[mapWidth * mapHeight];

        for (int y = 0; y < mapHeight; y++)
        {
            for (int x = 0; x < mapWidth; x++)
            {
                float noiseValue = Noise.GenerateNoiseAtLocation(prop, x, y);
                Color colorValue = Color.Lerp(Color.black, Color.white, noiseValue);
                colourMap[y * mapWidth + x] = colorValue;
            }
        }

        texture.wrapMode = TextureWrapMode.Clamp;
        texture.SetPixels(colourMap);
        texture.Apply();
        EditorGUI.DrawPreviewTexture(new Rect(375, 100, 300, 300), texture);
    }

    private void DrawNoiseProperties()
    {
        GUILayout.Space(10);
        DrawField("mapWidth");
        GUILayout.Space(10);
        DrawField("mapHeight");
        GUILayout.Space(10);
        DrawField("noiseScale");
        GUILayout.Space(10);
        DrawField("octaves");
        GUILayout.Space(10);
        DrawField("persistance");
        GUILayout.Space(10);
        DrawField("lacunarity");
        GUILayout.Space(10);
        DrawField("groundStep");
        GUILayout.Space(10);
        DrawField("oceanStep");
        GUILayout.Space(50);
        DrawField("seed");
        GUILayout.Space(10);
        DrawField("threshold");
        GUILayout.Space(10);
        DrawField("offset");

    }

    private void DrawField(string propName, bool relative = false)
    {
        EditorGUILayout.PropertyField(serializedObject.FindProperty(propName), relative);
    }

    private WorldProperties GetWorldProperties(SerializedObject obj)
    {
        WorldProperties worldProperties = ScriptableObject.CreateInstance<WorldProperties>();

        worldProperties.mapWidth = obj.FindProperty("mapWidth").intValue;
        worldProperties.mapHeight = obj.FindProperty("mapHeight").intValue;
        worldProperties.noiseScale = obj.FindProperty("noiseScale").floatValue;
        worldProperties.octaves = obj.FindProperty("octaves").intValue;
        worldProperties.persistance = obj.FindProperty("persistance").floatValue;
        worldProperties.lacunarity = obj.FindProperty("lacunarity").floatValue;
        worldProperties.seed = obj.FindProperty("seed").intValue;
        worldProperties.offset = obj.FindProperty("offset").vector2Value;
        worldProperties.groundStep = obj.FindProperty("groundStep").floatValue;
        worldProperties.oceanStep = obj.FindProperty("oceanStep").floatValue;
        worldProperties.threshold = obj.FindProperty("threshold").floatValue;

        return worldProperties;
    }

    public void LoadProperties(WorldProperties prop)
    {
        serializedObject = new SerializedObject(prop);
    }
}

