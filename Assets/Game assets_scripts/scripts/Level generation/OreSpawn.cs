using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Tilemaps;

[System.Serializable]
public struct OreType
{
    public GameObject prefab;
    [Range(0f, 1f)] public float probability;
}

public class OreSpawn : MonoBehaviour
{
    [SerializeField] private Tilemap targetTilemap;
    [SerializeField] private List<OreType> oreTypes;
    [SerializeField][Range(0f, 1f)] private float overallSpawnProbability = 0.1f; // ����� ����������� ��������� ����

    [SerializeField] private CaveTexture caveTexture; // ������ �� ������ CaveTexture

    [SerializeField] private string parentObjectName = "DecorationsParent";//

    private Transform _decorationsParent;//



    void Awake()
    {
        if (targetTilemap == null)
        {
            Debug.LogError("Tilemap �� ����������!");
            enabled = false;
            return;
        }
        if (oreTypes == null || oreTypes.Count == 0)
        {
            Debug.LogError("������ �������� ���� ���� ��� �� ����������!");
            enabled = false;
            return;
        }

        if (caveTexture == null)
        {
            Debug.LogError("CaveTexture �� ����������!");
            enabled = false;
            return;
        }

        foreach (OreType oreType in oreTypes)
        {
            if (oreType.prefab == null)
            {
                Debug.LogError("���� �� �������� ���� �� ����������!");
                enabled = false;
                return;
            }
        }

        // ���� ��� ������� ������ ��������
        _decorationsParent = transform.Find(parentObjectName);//
        if (_decorationsParent == null)
        {
            GameObject parentGO = new GameObject(parentObjectName);
            _decorationsParent = parentGO.transform;
            _decorationsParent.SetParent(transform);
        }

    }

    public void SpawnOre(Vector3Int startPosition, int mapWidth, int mapHeight)
    {
        if (targetTilemap == null || oreTypes == null || oreTypes.Count == 0)
        {
            Debug.LogWarning("�� ������� ������� ����!");
            return;
        }

        for (int x = startPosition.x; x < startPosition.x + mapWidth - 1; x++)
        {
            for (int y = startPosition.y; y < startPosition.y + mapHeight - 1; y++)
            {
                Vector3Int currentPosition = new Vector3Int(x, y, 0);
                TileBase currentTile = targetTilemap.GetTile(currentPosition);

                // ���������, �������� �� ���� ������ � ���� �� ��� ��� �������������� �����������
                if (currentTile != null && caveTexture.IsStoneTile(currentTile) && IsHorizontalSurface(currentPosition))
                {
                    float randomValue = Random.value;
                    if (randomValue < overallSpawnProbability)
                    {
                        SpawnSpecificOre(currentPosition);
                    }
                }
            }
        }
    }
    private void SpawnSpecificOre(Vector3Int position)
    {
        float randomOreValue = Random.value;
        float cumulativeProbability = 0f;

        foreach (OreType oreType in oreTypes)
        {
            cumulativeProbability += oreType.probability;
            if (randomOreValue < cumulativeProbability)
            {
                Vector3Int orePosition = position + new Vector3Int(0, 1, 0);
                Vector3 worldOrePosition = targetTilemap.GetCellCenterWorld(orePosition);
                GameObject ore = Instantiate(oreType.prefab, worldOrePosition, Quaternion.identity);
                ore.transform.SetParent(_decorationsParent);
                return; // ������� ����� ��������
            }
        }
    }
    private bool IsHorizontalSurface(Vector3Int position)
    {
        // �������������� ����������� - ���� ��� ������ ��� ������� �����
        Vector3Int upPosition = position + new Vector3Int(0, 1, 0);
        return targetTilemap.GetTile(upPosition) == null;
    }
}
