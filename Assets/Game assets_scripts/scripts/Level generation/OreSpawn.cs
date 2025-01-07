using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Tilemaps;

public class OreSpawn : MonoBehaviour
{
    [SerializeField] private Tilemap targetTilemap;
    [SerializeField] private GameObject orePrefab;
    [SerializeField][Range(0f, 1f)] private float spawnProbability = 0.1f; // ����������� ��������� ����

    void Awake()
    {
        if (targetTilemap == null)
        {
            Debug.LogError("Tilemap �� ����������!");
            enabled = false;
            return;
        }
        if (orePrefab == null)
        {
            Debug.LogError("Ore Prefab �� ����������!");
            enabled = false;
            return;
        }
    }

    public void SpawnOre(int length)
    {
        if (targetTilemap == null || orePrefab == null)
        {
            Debug.LogWarning("�� ������� ������� ����!");
            return;
        }

        for (int x = 0; x < length; x++)
        {
            for (int y = 0; y < length; y++)
            {
                Vector3Int currentPosition = new Vector3Int(x, y, 0);
                if (targetTilemap.GetTile(currentPosition) != null && IsHorizontalSurface(currentPosition))
                {
                    float randomValue = Random.value;
                    if (randomValue < spawnProbability)
                    {
                        Vector3 orePosition = targetTilemap.GetCellCenterWorld(currentPosition);
                        Instantiate(orePrefab, orePosition, Quaternion.identity, transform);
                    }
                }

            }
        }

    }

    private bool IsHorizontalSurface(Vector3Int position)
    {
        // �������������� ����������� - ���� ���� ���� ������, � ��� ��� ��� ��� ��������
        Vector3Int upPosition = position + new Vector3Int(0, 1, 0);
        Vector3Int downPosition = position + new Vector3Int(0, -1, 0);
        bool isUpEmpty = targetTilemap.GetTile(upPosition) == null;
        bool isDownEmpty = targetTilemap.GetTile(downPosition) == null;

        return (isUpEmpty != isDownEmpty);
    }
}
