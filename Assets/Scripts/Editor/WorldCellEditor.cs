using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;
using static Unity.Burst.Intrinsics.X86.Avx;
using static UnityEditor.PlayerSettings;


[CustomEditor(typeof(WorldCell))]
[CanEditMultipleObjects]
public class WorldCellEditor : Editor
{
    private bool showCubes = false;


    public override void OnInspectorGUI()
    {
        WorldCell cell = (WorldCell)target;

        base.DrawDefaultInspector();
        showCubes = cell.debug;
    }

    private void OnSceneGUI()
    {
        WorldCell cell = (WorldCell)target;
        if (showCubes)
        {
            Vector3 startingPos = cell.transform.position + new Vector3(-2 * cell.cellSubSize, 2 * cell.cellSubSize, 0);
            Vector3 currentPos = startingPos;
            Vector3 cubeSize = new Vector3(cell.cellSubSize, cell.cellSubSize, cell.cellSubSize);

            Handles.color = Color.red;
            Handles.DrawWireCube(cell.transform.position, new Vector3(cell.cellSubSize * 5, cell.cellSubSize * 5, cell.cellSubSize));

            for (int i = 0; i < 5; i++)
            {
                currentPos.x = startingPos.x;
                for (int j = 0; j < 5; j++)
                {
                    DrawCubeHandle(currentPos, cubeSize, cell.ExportConfig()[(i * 5) + (j)]);
                    currentPos.x += cell.cellSubSize;
                }
                currentPos.y -= cell.cellSubSize;
            }
        }
    }

    private void DrawCubeHandle(Vector3 pos, Vector3 size, bool active)
    {
        if (active)
        {
            Handles.color = Color.green;
            Handles.DrawWireCube(pos, size);
        }
    }
}
