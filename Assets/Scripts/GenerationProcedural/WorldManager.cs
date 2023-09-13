using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class WorldManager : MonoBehaviour
{
    public int dicoCount = 0;
    public bool cleanBehind = true;

    [Header("Player Properties")]
    public Transform viewer;
    public int instantiationRadius = 100;
    private Vector3 playerNoiseCoord = Vector3.zero;

    public GameObject chunk;
    public Transform worldParent;

    public Dictionary<Vector2, WorldChunk> existingChunk = new Dictionary<Vector2, WorldChunk>();
    private List<WorldChunk> currentChunks = new List<WorldChunk>();
    private List<WorldChunk> activeChunks = new List<WorldChunk>();

    private Vector3 gridOffSet = new Vector2(500, 500);
    private WorldGenerator worldGen;
    //private float[,] map;

    private void Awake()
    {
        worldGen = GetComponent<WorldGenerator>();
    }

    private void Start()
    {

    }

    private void Update()
    {
        currentChunks.Clear();

        playerNoiseCoord = Vector3.zero;

        playerNoiseCoord.x = Mathf.Round(viewer.position.x / 10) * 10;
        playerNoiseCoord.y = Mathf.Round(viewer.position.y / 10) * 10;

        for (float y = playerNoiseCoord.y - instantiationRadius; y <= playerNoiseCoord.y + instantiationRadius; y += 10)
        {
            for (float x = playerNoiseCoord.x - instantiationRadius; x <= playerNoiseCoord.x + instantiationRadius; x += 10)
            {
                Vector2 currentPos = new Vector2(x, y);
                int NoiseX = (500 + (int)x) / 10;
                int NoiseY = (500 + (int)y) / 10;
                NoiseY = Mathf.Clamp(NoiseY, 0, 100);

                WorldChunk currentChunk;

                if (existingChunk.ContainsKey(currentPos))
                {
                    currentChunk = existingChunk[currentPos];
                    currentChunk.gameObject.SetActive(true);
                    currentChunks.Add(currentChunk);
                    if (!activeChunks.Contains(currentChunk))
                        activeChunks.Add(currentChunk);
                }
                else if (worldGen.GenerateRandomAtPosition(new Vector2(NoiseX, NoiseY)) != 0)
                {
                    var inst = Instantiate(chunk, currentPos, Quaternion.identity, worldParent);
                    currentChunk = inst.GetComponent<WorldChunk>();
                    currentChunk.SetCoordinates(new Vector2(NoiseX, NoiseY), currentPos);
                    existingChunk.Add(currentPos, currentChunk);
                    currentChunks.Add(currentChunk);
                    if (!activeChunks.Contains(currentChunk))
                        activeChunks.Add(currentChunk);
                }
            }
        }

        // Remove far Chunks
        if (cleanBehind)
        {
            foreach (var chunk in activeChunks)
            {
                if (!currentChunks.Contains(chunk))
                    chunk.gameObject.SetActive(false);
            }
        }

        dicoCount = existingChunk.Count;
    }

    private void OnValidate()
    {
        int check = instantiationRadius % 10;
        if (check != 0)
        {
            if (check < 5)
                instantiationRadius -= check;
            else
            {
                instantiationRadius -= check;
                instantiationRadius += 10;
            }
        }
    }

}