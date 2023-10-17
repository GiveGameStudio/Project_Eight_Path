using System.Collections;
using System.Collections.Generic;
using System.Diagnostics;
using Unity.VisualScripting;
using UnityEditor;
using UnityEditor.TerrainTools;
using UnityEngine;
using static UnityEditor.PlayerSettings;

public class WorldCellSetUpWindow : EditorWindow
{
    WorldCell selectedCell = null;
    bool[] config = new bool[25];
    //int cellsubSize = 2;


    public static void ShowWindow()
    {
        var window = GetWindow<WorldCellSetUpWindow>();
        window.titleContent = new GUIContent("World Cell Editor");
        window.minSize = new Vector2(375, 500);
        window.maxSize = new Vector2(375, 500);
    }

    private void OnGUI()
    {
        var labelStyle = new GUIStyle(GUI.skin.label) { alignment = TextAnchor.MiddleCenter};
        EditorGUILayout.LabelField("Selected Cell", labelStyle);
        EditorGUILayout.Space();

        #region Cell Selection
        EditorGUILayout.BeginVertical("Box");
        if (Selection.gameObjects.Length != 0)
        {
            if (Selection.gameObjects[0].TryGetComponent<WorldCell>(out WorldCell worldCell))
            {
                selectedCell = worldCell;
                config = selectedCell.ExportConfig();
            }
            else
            {
                selectedCell = null;
                EditorGUILayout.LabelField("The selected GameObject doesn't countains WorldCell script", labelStyle, GUILayout.Height(40));
            }
        }
        else
        {
            selectedCell = null;
            EditorGUILayout.LabelField("Select a GameObject", labelStyle, GUILayout.Height(40));
        }
        EditorGUILayout.EndVertical();
        #endregion

        #region SetUpCell
        EditorGUILayout.BeginVertical("Box");
        if (selectedCell != null)
        {
            EditorGUILayout.LabelField(selectedCell.gameObject.name, labelStyle, GUILayout.Height(40));
            EditorGUILayout.BeginVertical("Box");
            for (int i = 0; i < 5; i++)
            {
                EditorGUILayout.BeginHorizontal();
                GUILayout.FlexibleSpace();
                for (int j = 0; j < 5; j++)
                {
                    EditorGUILayout.Space(10);
                    EditorGUI.BeginChangeCheck();
                    bool tmp = EditorGUILayout.Toggle(config[(i * 5) + (j)], GUILayout.Width(10));
                    if (EditorGUI.EndChangeCheck())
                    {
                        config[(i * 5) + (j)] = tmp;
                    }
                    EditorGUILayout.Space(10);
                }
                GUILayout.FlexibleSpace();
                EditorGUILayout.EndHorizontal();
                EditorGUILayout.Space(20);
            }
            EditorGUILayout.EndVertical();
            EditorGUILayout.BeginVertical("Box");
            EditorGUILayout.BeginVertical("Box");
            if (GUILayout.Button("Set Up Cell", labelStyle, GUILayout.Height(50)))
            {
                selectedCell.SetBorders(config);
                SceneView.RepaintAll();
            }
            EditorGUILayout.EndHorizontal();
            EditorGUILayout.EndHorizontal();
        }
        EditorGUILayout.EndVertical();
        #endregion
    }

    /// <summary>
    /// Update the windows when Selection Change
    /// </summary>
    void OnSelectionChange()
    {
        Repaint();
    }

   
}
