using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public static class Noise
{
    public static float[,] GenerateNoiseMap(int mapWidth, int mapHeight, int seed, float scale, int octaves, float persistance, float lacunarity, float threshold, Vector2 offset, 
        bool addOcean, float oceanStep, bool addGround, float groundStep)
    {
        float[,] noiseMap = new float[mapWidth, mapHeight];

        System.Random prng = new System.Random(seed);
        Vector2[] octaveOffsets = new Vector2[octaves];
        for (int i = 0; i < octaves; i++)
        {
            float offsetX = prng.Next(-100000, 100000) + offset.x;
            float offsetY = prng.Next(-100000, 100000) + offset.y;
            octaveOffsets[i] = new Vector2(offsetX, offsetY);
        }

        if (scale <= 0)
        {
            scale = 0.0001f;
        }

        float maxNoiseHeight = float.MinValue;
        float minNoiseHeight = float.MaxValue;

        float halfWidth = mapWidth / 2f;
        float halfHeight = mapHeight / 2f;

        for (int y = 0; y < mapHeight; y++)
        {
            float oceanValue = (1 - (y * oceanStep));
            oceanValue = Mathf.Clamp01(oceanValue);

            float groundValue = 1 - ((mapHeight - (y + 1)) * groundStep);
            groundValue = Mathf.Clamp01(groundValue);

            for (int x = 0; x < mapWidth; x++)
            {

                float amplitude = 1;
                float frequency = 1;
                float noiseHeight = 0;

                for (int i = 0; i < octaves; i++)
                {
                    float sampleX = (x - halfWidth) / scale * frequency + octaveOffsets[i].x;
                    float sampleY = (y - halfHeight) / scale * frequency + octaveOffsets[i].y;

                    float perlinValue = Mathf.PerlinNoise(sampleX, sampleY) * 2 - 1;
                    noiseHeight += perlinValue * amplitude;

                    amplitude *= persistance;
                    frequency *= lacunarity;
                }

                if (noiseHeight > maxNoiseHeight)
                {
                    maxNoiseHeight = noiseHeight;
                }
                else if (noiseHeight < minNoiseHeight)
                {
                    minNoiseHeight = noiseHeight;
                }
                noiseMap[x, y] = noiseHeight;

                if (addOcean) noiseMap[x, y] -= oceanValue;
                if (addGround) noiseMap[x,y] += groundValue;
            }
        }

        for (int y = 0; y < mapHeight; y++)
        {
            for (int x = 0; x < mapWidth; x++)
            {
                noiseMap[x, y] = Mathf.InverseLerp(minNoiseHeight, maxNoiseHeight, noiseMap[x, y]);
                noiseMap[x, y] = Mathf.Clamp01(noiseMap[x, y]);
            }
        }


        return ApplyThreshold(noiseMap, threshold, mapHeight);
    }

    private static float[,] ApplyThreshold(float[,] noiseMap, float threshold, int mapHeight)
    {
        for (int y = 0; y < mapHeight; y++)
        {
            for (int x = 0; x < mapHeight; x++)
            {
                if (y == 0)
                    noiseMap[x, y] = 0;
                else if (y == mapHeight - 1)
                    noiseMap[x, y] = 1;
                else if (noiseMap[x, y] <= threshold)
                    noiseMap[x, y] = 0;
            }
        }
        
        return noiseMap;
    }


    private static float[,] AddGround(float[,] noiseMap, float groundStep, int mapHeight)
    {
        for (int y = mapHeight - 1 ; y >= 0; y--)
        {
            float value = 1 - ((mapHeight - (y+1)) * groundStep);
            value = Mathf.Clamp01(value);
            for (int x = 0; x < mapHeight; x++)
            {
                noiseMap[x, y] = value;
            }
        }

        return noiseMap;
    }

    private static float[,] AddOcean(float[,] noiseMap, float oceanStep, int mapHeight)
    {
        for (int y = 0; y < mapHeight; y++)
        {
            float value = (1 - (y * oceanStep));
            value = Mathf.Clamp01(value);
            for (int x = 0; x < mapHeight; x++)
            {
                noiseMap[x, y] = value;
            }
        }
        return noiseMap;
    }
}
