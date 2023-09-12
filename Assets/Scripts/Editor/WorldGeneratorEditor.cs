using System.Collections;
using UnityEngine;
using UnityEditor;

[CustomEditor(typeof(WorldGenerator))]
public class WorldGeneratorEditor : Editor
{
    public override void OnInspectorGUI()
    {
        WorldGenerator worldGen = (WorldGenerator)target;

        if (DrawDefaultInspector())
        {
            if (worldGen.autoUpdate)
            {
                worldGen.GenerateMap();
            }
        }

        if (GUILayout.Button("Generate"))
        {
            worldGen.GenerateMap();
        }
    }
}
