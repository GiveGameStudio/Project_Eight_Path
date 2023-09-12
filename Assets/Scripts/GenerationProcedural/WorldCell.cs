using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WorldCell : MonoBehaviour
{
    [Header("DebugCells")]
    public float cellSubSize = 2;
    public bool debug = false;

    private bool[] borders = new bool[25];


    public void SetBorders(bool[] config)
    {
        for (int i = 0; i < config.Length; i++)
        {
            borders[i] = config[i];
        }
    }

    public bool[] ExportConfig()
    {
           return borders;
    }

  
}
