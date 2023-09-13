using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEditor.TerrainTools;
using UnityEngine;

public class MapDisplay : MonoBehaviour
{
    public Renderer textureRender;

    public bool zoneDebug = false;
    [Header("Littorale Properties")]
    public float littoralZone;
    public Color littoraleColor;
    [Header("Mésale Properties")]
    public float mesaleZone;
    public Color mesaleColor;
    [Header("Bathyale Properties")]
    public float bathyaleZone;
    public Color bathyaleColor;
    [Header("Abyssale Properties")]
    public float abyssaleZone;
    public Color abyssaleColor;
    [Header("Hadale Properties")]
    public float hadaleZone;
    public Color hadaleColor;

    private float[,] map;

    public void DrawNoiseMap(float[,] noiseMap)
    {
        map = noiseMap;

        int width = noiseMap.GetLength(0);
        int height = noiseMap.GetLength(1);

        Texture2D texture = new Texture2D(width, height);

        Color[] colourMap = new Color[width * height];
        for (int y = 0; y < height; y++)
        {
            for (int x = 0; x < width; x++)
            {
                Color value = Color.Lerp(Color.black, Color.white, noiseMap[x, y]);

                if (zoneDebug)
                {
                    if (y <= height * (littoralZone / 100))
                        value *= littoraleColor;
                    else if (y <= height * (mesaleZone / 100))
                        value *= mesaleColor;
                    else if (y <= height * (bathyaleZone / 100))
                        value *= bathyaleColor;
                    else if (y <= height * (abyssaleZone / 100))
                        value *= abyssaleColor;
                    else if (y > height * (abyssaleZone / 100))
                        value *= hadaleColor;
                }
                colourMap[y * width + x] = value;
            }
        }
        texture.wrapMode = TextureWrapMode.Clamp;
        texture.SetPixels(colourMap);
        texture.Apply();

        textureRender.sharedMaterial.mainTexture = texture;
        //textureRender.transform.localScale = new Vector3(width, 1, height);
    }


    private void OnValidate()
    {
        if (littoralZone == 0) { littoralZone = 1; }
        if (mesaleZone == 0) { mesaleZone = 1; }
        if (bathyaleZone == 0) { bathyaleZone = 1; }
        if (abyssaleZone == 0) { abyssaleZone = 1; }
        if (hadaleZone == 0) { hadaleZone = 1; }
    }

}
