using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;

[CustomEditor(typeof(WorldChunk))]
public class WorldChunkEditor : Editor
{
    public override void OnInspectorGUI()
    {
        WorldChunk chunk = (WorldChunk)target;

        DrawDefaultInspector();

        if (GUILayout.Button("Display Coordinates"))
        {
            chunk.DisplayCoordinates();
        }
    }
}
