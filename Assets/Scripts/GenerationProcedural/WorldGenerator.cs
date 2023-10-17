using System.Collections;
using System.Collections.Generic;
using Unity.Mathematics;
using UnityEngine;

public class WorldGenerator : MonoBehaviour
{
    public MapDisplay display;
    public int mapWidth;
    public int mapHeight;
    [Range(1, 50)]
    public float noiseScale;

    [Tooltip("Define the number of pass you add to your random, each pass being less impactfull")]
    [Range(0, 10)]
    public int octaves;

    [Tooltip("Define the strength of the additional pass")]
    [Range(0, 1)]
    public float persistance;

    [Range(1, 10)]
    public float lacunarity;

    public int seed;
    public Vector2 offset;

    [Header("Ocean Properties")]
    public bool addOcean;
    [Range(.005f, 1)]
    public float oceanStep;

    [Header("Bottom Ground")]
    public bool addGround;
    [Range(.005f, 1)]
    public float groundStep;

    [Header("World Threshold")]
    [Range(0, 1)]
    public float threshold;

    public bool autoUpdate;

    private void Start()
    {
        display.gameObject.SetActive(false);
    }

    public float[,] GenerateMap()
    {
        float[,] noiseMap = Noise.GenerateNoiseMap(mapWidth, mapHeight, seed, noiseScale, octaves, persistance, lacunarity, threshold, offset, addOcean, oceanStep, addGround, groundStep);

        display.DrawNoiseMap(noiseMap);
        return noiseMap;
    }

    void OnValidate()
    {
        if (mapWidth < 1)
        {
            mapWidth = 1;
        }
        if (mapHeight < 1)
        {
            mapHeight = 1;
        }
        if (lacunarity < 1)
        {
            lacunarity = 1;
        }
        if (octaves < 0)
        {
            octaves = 0;
        }
    }
}
