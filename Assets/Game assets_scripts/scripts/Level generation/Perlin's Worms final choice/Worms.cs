using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Tilemaps;

public class Worms : MonoBehaviour
{
    [Header("Map Settings")]
    public int mapWidth = 100;
    public int mapHeight = 100;
    public int borderSize = 2;

    [Header("Map Offset")]
    public Vector3Int mapOffset = Vector3Int.zero;

    [Header("Cave Generation")]
    public Vector3Int startPosition;
    public int walkLength = 100;
    public int tunnelWidth = 1;
    [Range(0, 1)] public float perlinNoiseScale = 0.1f;
    [Range(0, 1)] public float perlinNoiseThreshold = 0.5f;

    [Header("Tile Settings")]
    public string floorTilePath;
    public string startTilePath;
    public Tilemap tilemap;

    [Header("Spawners")]
    public PlayerSpawn playerSpawn;
    public OreSpawn oreSpawn;

    private TileBase floorTile;
    private TileBase startTile;
    private List<Vector3Int> path = new List<Vector3Int>();
    private Vector3Int actualStartPosition;
    private int platformWidth = 3; // Ширина платформы

    void Start()
    {
        InitializeStartPosition();
        LoadTiles();
        GenerateMap();

        if (playerSpawn != null)
        {
            playerSpawn.SpawnPrefabs(mapWidth);
        }

        if (oreSpawn != null)
        {
            oreSpawn.SpawnOre(mapWidth);
        }
    }

    private void InitializeStartPosition()
    {
        if (startPosition == Vector3Int.zero)
        {
            startPosition = new Vector3Int(mapWidth / 2, mapHeight / 2, 0);
            Debug.LogWarning("Начальная позиция не установлена. Используется центр карты.");
        }

        startPosition = new Vector3Int(
           Mathf.Clamp(startPosition.x, borderSize, mapWidth - 1 - borderSize),
           Mathf.Clamp(startPosition.y, borderSize, mapHeight - 1 - borderSize),
           0);

        actualStartPosition = startPosition + mapOffset;
        Debug.Log($"Начальная позиция для генерации пещеры (внутри карты): {startPosition}");
        Debug.Log($"Реальная начальная позиция в мире: {actualStartPosition}");
    }

    void LoadTiles()
    {
        floorTile = Resources.Load<TileBase>(floorTilePath);
        if (floorTile == null)
        {
            Debug.LogError("Тайл пола не найден.");
        }
        startTile = Resources.Load<TileBase>(startTilePath);
        if (startTile == null)
        {
            Debug.LogError("Тайл стартовой позиции не найден.");
        }
    }

    void GenerateMap()
    {
        FillMapWithFloor();
        GeneratePathWithPerlin();
        ClearTunnelAlongPath();
        CreateStartPositionPlatform();  // Создаем платформу под стартовой позицией
        ColorStartPosition();  // Затем окрашиваем платформу
        Debug.Log("Карта сгенерирована с пещерой.");
    }

    void FillMapWithFloor()
    {
        for (int x = 0; x < mapWidth; x++)
        {
            for (int y = 0; y < mapHeight; y++)
            {
                tilemap.SetTile(new Vector3Int(x, y, 0) + mapOffset, floorTile);
            }
        }
    }

    void GeneratePathWithPerlin()
    {
        path.Clear();
        Vector3Int currentPosition = actualStartPosition;
        path.Add(currentPosition);

        for (int i = 0; i < walkLength; i++)
        {
            Vector3Int nextPosition = GetNextPositionWithPerlin(currentPosition);
            path.Add(nextPosition);
            currentPosition = nextPosition;
        }
    }

    Vector3Int GetNextPositionWithPerlin(Vector3Int currentPos)
    {
        int newX = currentPos.x;
        int newY = currentPos.y;

        float noiseValue = Mathf.PerlinNoise((currentPos.x - mapOffset.x) * perlinNoiseScale, (currentPos.y - mapOffset.y) * perlinNoiseScale);

        if (noiseValue > perlinNoiseThreshold)
        {
            newX += Random.value < 0.5f ? 1 : -1;
        }
        else
        {
            newY += Random.value < 0.5f ? 1 : -1;
        }

        int minX = mapOffset.x + borderSize;
        int maxX = mapOffset.x + mapWidth - 1 - borderSize;
        int minY = mapOffset.y + borderSize;
        int maxY = mapOffset.y + mapHeight - 1 - borderSize;

        newX = Mathf.Clamp(newX, minX, maxX);
        newY = Mathf.Clamp(newY, minY, maxY);

        return new Vector3Int(newX, newY, 0);
    }

    void ClearTunnelAlongPath()
    {
        foreach (Vector3Int position in path)
        {
            ClearTunnel(position);
        }
    }

    void ClearTunnel(Vector3Int centerTile)
    {
        for (int x = 0; x < tunnelWidth; x++)
        {
            for (int y = 0; y < tunnelWidth; y++)
            {
                Vector3Int neighbourPos = centerTile + new Vector3Int(x, y, 0);
                tilemap.SetTile(neighbourPos, null);
            }
        }
    }

    void ColorStartPosition()
    {
        for (int x = -1; x <= 1; x++)
        {
            Vector3Int platformPos = actualStartPosition + new Vector3Int(x, -1, 0);
            tilemap.SetTile(platformPos, startTile);
        }
    }
    void CreateStartPositionPlatform()
    {
        for (int x = -1; x <= 1; x++)
        {
            Vector3Int platformPosition = actualStartPosition + new Vector3Int(x, -1, 0);
            tilemap.SetTile(platformPosition, floorTile);
        }
    }
}
