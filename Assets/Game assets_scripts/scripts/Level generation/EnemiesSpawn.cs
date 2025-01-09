using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Tilemaps;


[System.Serializable]
public struct EnemyType
{
    public GameObject prefab;
    [Range(0f, 1f)] public float probability;
}

public class EnemiesSpawn : MonoBehaviour
{
    [SerializeField] private Tilemap targetTilemap;
    [SerializeField] private List<EnemyType> enemyTypes;
    [SerializeField][Range(0f, 1f)] private float overallSpawnProbability = 0.1f; // ����� ����������� ��������� �����
    [SerializeField] private int clearanceSize = 3; // ������ ������� ��� ��������


    void Awake()
    {
        if (targetTilemap == null)
        {
            Debug.LogError("Tilemap �� ����������!");
            enabled = false;
            return;
        }
        if (enemyTypes == null || enemyTypes.Count == 0)
        {
            Debug.LogError("������ �������� ������ ���� ��� �� ����������!");
            enabled = false;
            return;
        }
        foreach (EnemyType enemyType in enemyTypes)
        {
            if (enemyType.prefab == null)
            {
                Debug.LogError("���� �� �������� ������ �� ����������!");
                enabled = false;
                return;
            }
        }

    }

    public void SpawnEnemies(Vector3Int startPosition, int mapWidth, int mapHeight)
    {
        if (targetTilemap == null || enemyTypes == null || enemyTypes.Count == 0)
        {
            Debug.LogWarning("�� ������� ������� �����!");
            return;
        }

        for (int x = startPosition.x; x < startPosition.x - 1 + mapWidth; x++)
        {
            for (int y = startPosition.y; y < startPosition.y - 1 + mapHeight; y++)
            {
                Vector3Int currentPosition = new Vector3Int(x, y, 0);
                if (targetTilemap.GetTile(currentPosition) != null && IsClearSpace(currentPosition))
                {
                    float randomValue = Random.value;
                    if (randomValue < overallSpawnProbability)
                    {
                        SpawnSpecificEnemy(currentPosition);
                    }
                }
            }
        }
    }

    private void SpawnSpecificEnemy(Vector3Int position)
    {
        float randomEnemyValue = Random.value;
        float cumulativeProbability = 0f;

        foreach (EnemyType enemyType in enemyTypes)
        {
            cumulativeProbability += enemyType.probability;
            if (randomEnemyValue < cumulativeProbability)
            {
                Vector3 worldPosition = targetTilemap.GetCellCenterWorld(position);
                Instantiate(enemyType.prefab, worldPosition, Quaternion.identity);
                return; // ������� ����� ��������
            }
        }
    }
    private bool IsClearSpace(Vector3Int position)
    {
        //�������� ������� ����� � ����� ������� ������.
        if (targetTilemap.GetTile(position) == null)
        {
            return false; // ���� �� ������� ������ ��� ����� - ����� ����������.
        }

        //�������� ������� ������ �� ��� X (����� � ������ �� �������).
        if (targetTilemap.GetTile(position + Vector3Int.left) == null ||
            targetTilemap.GetTile(position + Vector3Int.right) == null)
        {
            return false;  // ���� ��� ������ ����� ��� ������ - ����� ����������.
        }

        //�������� ���������� ������������ ��� ���������� ������ (3x2, ������� ������� ���)
        for (int x = -1; x <= 1; x++)
        {
            for (int y = 1; y <= clearanceSize - 1; y++) //�������� �������� � y = 1, ����� ��������� ������ ��� ����������
            {
                Vector3Int checkPosition = position + new Vector3Int(x, y, 0);
                if (targetTilemap.GetTile(checkPosition) != null)
                {
                    return false; // ���� ���� �� ���� ���� ����, �� ������������ �� ��������
                }
            }
        }
        return true; // ���� ��� ������� ���������, �� ������������ ��������
    }
}
