using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WorldChunk : MonoBehaviour
{
    private Vector2 _noiseCoord;
    private Vector2 _worldCoord;
    
    public void SetCoordinates(Vector2 noiseCoord, Vector2 worldCoord)
    {
        _noiseCoord = noiseCoord;
        _worldCoord = worldCoord;
    }

    public void DisplayCoordinates()
    {
        Debug.Log("World Coordinates : " + _worldCoord + "\nNoise Coordinates : " + _noiseCoord);
    }
}
