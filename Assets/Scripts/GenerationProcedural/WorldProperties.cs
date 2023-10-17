using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WorldProperties : ScriptableObject
{
    public int mapWidth;
    public int mapHeight;
    public float noiseScale;
    public int octaves;
    public float persistance;
    public float lacunarity;
    public int seed;
    public Vector2 offset;
    public float groundStep;
    public float oceanStep;
    public float threshold;

    public WorldProperties()
    {
        mapWidth = 100;
        mapHeight = 100;
        noiseScale = 1;
        octaves = 1;
        persistance = 0;
        lacunarity = 1;
        seed = 0;
        offset = Vector2.zero;
        groundStep = 1;
        oceanStep = 1;
        threshold = 0.5f;
    }
}
