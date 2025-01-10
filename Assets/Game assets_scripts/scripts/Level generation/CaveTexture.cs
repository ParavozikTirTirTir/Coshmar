using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Tilemaps;

[System.Serializable] // ��� �������� ����� � ����������� ��������� 
public struct WeightedTile
{
    public TileBase tile;
    [Range(0f, 1f)] public float probability;
}




public class CaveTexture : MonoBehaviour
{
    [Header("Map Settings")]
    public float noiseScale = 0.1f;
    public float stoneThreshold = 0.5f;
    public float dirtThreshold = 0.7f;
    public int seed = 0;
    public int smoothingIterations = 1;  // ���������� �������� �����������

    public WeightedTile[] stoneTiles;
    public WeightedTile[] dirtTiles;
    public WeightedTile[] grassTiles;



    public void ApplyTextures(Tilemap tilemap, int mapWidth, int mapHeight, Vector3Int startPosition)
    {
        Random.InitState(seed);
        // ��������� ��������� ��������
        TileBase[,] textureMap = ApplyInitialTextures(tilemap, mapWidth, mapHeight, startPosition);

        // ���������� �������
        for (int i = 0; i < smoothingIterations; i++)
        {
            textureMap = SmoothBoundaries(tilemap, mapWidth, mapHeight, startPosition, textureMap);
        }

        // ������������� ����� �����
        for (int x = 0; x < mapWidth; x++)
        {
            for (int y = 0; y < mapHeight; y++)
            {
                Vector3Int tilePosition = startPosition + new Vector3Int(x, y, 0);
                tilemap.SetTile(tilePosition, textureMap[x, y]);

            }
        }
    }


    //��������������� ��������
    private TileBase[,] ApplyInitialTextures(Tilemap tilemap, int mapWidth, int mapHeight, Vector3Int startPosition)
    {
        TileBase[,] textureMap = new TileBase[mapWidth, mapHeight];

        for (int x = 0; x < mapWidth; x++)
        {
            for (int y = 0; y < mapHeight; y++)
            {
                Vector3Int tilePosition = startPosition + new Vector3Int(x, y, 0);
                float perlinValue = Mathf.PerlinNoise((x * noiseScale) + Random.value, (y * noiseScale) + Random.value);
                TileBase newTile = null;

                if (perlinValue < stoneThreshold)
                {
                    newTile = GetRandomTile(stoneTiles);
                }
                else if (perlinValue < dirtThreshold)
                {
                    newTile = GetRandomTile(dirtTiles);
                }
                else
                {
                    newTile = GetRandomTile(grassTiles);
                }
                textureMap[x, y] = newTile;
            }
        }
        return textureMap;
    }

    //���������� ��������
    private TileBase[,] SmoothBoundaries(Tilemap tilemap, int mapWidth, int mapHeight, Vector3Int startPosition, TileBase[,] textureMap)
    {
        TileBase[,] smoothedMap = new TileBase[mapWidth, mapHeight]; // ������ ������ ��� ����� ���������� �����

        for (int x = 0; x < mapWidth; x++)
        {
            for (int y = 0; y < mapHeight; y++)
            {
                smoothedMap[x, y] = GetSmoothedTile(tilemap, textureMap, x, y, mapWidth, mapHeight, startPosition); // �������� �������
            }
        }
        return smoothedMap;
    }
    private TileBase GetSmoothedTile(Tilemap tilemap, TileBase[,] textureMap, int x, int y, int mapWidth, int mapHeight, Vector3Int startPosition)
    {
        // ��� �������� �������
        int stoneCount = 0; 
        int dirtCount = 0;
        int grassCount = 0;


        // ��������� ������� (������� ���� � ������ ����)
        for (int nx = -1; nx <= 1; nx++)
        {
            for (int ny = -1; ny <= 1; ny++)
            {
                int neighborX = x + nx;
                int neighborY = y + ny;

                // ������ ���� � �������� �����
                if (neighborX >= 0 && neighborX < mapWidth && neighborY >= 0 && neighborY < mapHeight)
                {
                    // �������� �������������� ������ � ���� ��������
                    if (IsTileType(textureMap[neighborX, neighborY], stoneTiles))
                        stoneCount++;
                    else if (IsTileType(textureMap[neighborX, neighborY], dirtTiles))
                        dirtCount++;
                    else
                        grassCount++;
                }
            }
        }

        // �������������, ����� ������ ���� ���� � ����������� �� �������
        if (stoneCount > dirtCount && stoneCount > grassCount)
            return GetRandomTile(stoneTiles);
        else if (dirtCount > stoneCount && dirtCount > grassCount)
            return GetRandomTile(dirtTiles);
        else
            return GetRandomTile(grassTiles);
    }

    // ��������� ��������� �� ���� � ������ ���� ��������
    private bool IsTileType(TileBase tile, WeightedTile[] typeTiles)
    {
        if (typeTiles == null)
            return false;
        foreach (WeightedTile typeTile in typeTiles)
            if (tile == typeTile.tile)
                return true;
        return false;

    }

    // ������� ��������� �����
    private TileBase GetRandomTile(WeightedTile[] tiles)
    {
        if (tiles == null || tiles.Length == 0)
            return null;

        // ��������� ��� �����������
        float totalProbability = 0f;
        foreach (var weightedTile in tiles)
        {
            totalProbability += weightedTile.probability;
        }

        // ������������ ������������ (��������� �����������)
        if (Mathf.Abs(totalProbability - 1f) > 0.0001f)
        {
            Debug.LogWarning("��������� ����������� ������ �� ����� 1. ������������.");
            for (int i = 0; i < tiles.Length; i++)
            {
                tiles[i].probability /= totalProbability; 
            }
            totalProbability = 1f;
        }

        float randomValue = Random.value;
        float cumulativeProbability = 0f; // ����������� �����������
        for (int i = 0; i < tiles.Length; i++)
        {
            cumulativeProbability += tiles[i].probability;
            if (randomValue <= cumulativeProbability) 
                return tiles[i].tile;
        }
        return null;
    }


    // ��� ������ ����
    public bool IsStoneTile(TileBase tile)
    {
        return IsTileType(tile, stoneTiles);
    }

    public bool IsGrassTile(TileBase tile)
    {
        return IsTileType(tile, grassTiles);
    }

}
