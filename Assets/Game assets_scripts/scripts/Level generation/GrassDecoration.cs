using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Tilemaps;

[System.Serializable]
public struct DecorationType
{
    public GameObject prefab;
    [Range(0f, 1f)] public float probability;
}

public class GrassDecoration : MonoBehaviour
{
    [SerializeField] private Tilemap targetTilemap;
    [SerializeField] private List<DecorationType> decorationTypes;
    [SerializeField][Range(0f, 1f)] private float overallSpawnProbability = 0.1f;
    [SerializeField] private CaveTexture caveTexture;
    [SerializeField] private string parentObjectName = "DecorationsParent";

    private Transform _decorationsParent;

    void Awake()
    {
        if (targetTilemap == null)
        {
            Debug.LogError("Tilemap �� ����������!");
            enabled = false;
            return;
        }
        if (decorationTypes == null || decorationTypes.Count == 0)
        {
            Debug.LogError("������ �������� ��������� ���� ��� �� ����������!");
            enabled = false;
            return;
        }
        foreach (DecorationType decorationType in decorationTypes)
        {
            if (decorationType.prefab == null)
            {
                Debug.LogError("���� �� �������� ��������� �� ����������!");
                enabled = false;
                return;
            }
        }
        if (caveTexture == null)
        {
            Debug.LogError("CaveTexture �� ����������!");
            enabled = false;
            return;
        }

        // ���� ��� ������� ������ ��������
        _decorationsParent = transform.Find(parentObjectName);
        if (_decorationsParent == null)
        {
            GameObject parentGO = new GameObject(parentObjectName);
            _decorationsParent = parentGO.transform;
            _decorationsParent.SetParent(transform);
        }

    }

    public void SpawnDecorations(Vector3Int startPosition, int mapWidth, int mapHeight)
    {
        if (targetTilemap == null || decorationTypes == null || decorationTypes.Count == 0 || caveTexture == null)
        {
            Debug.LogWarning("�� ������� ������� ���������!");
            return;
        }

        for (int x = startPosition.x; x < startPosition.x + mapWidth - 1; x++)
        {
            for (int y = startPosition.y; y < startPosition.y + mapHeight - 1; y++)
            {
                Vector3Int currentPosition = new Vector3Int(x, y, 0);
                TileBase currentTile = targetTilemap.GetTile(currentPosition);
                if (currentTile != null && caveTexture.IsGrassTile(currentTile) && IsHorizontalSurface(currentPosition))
                {
                    float randomValue = Random.value;
                    if (randomValue < overallSpawnProbability)
                    {
                        SpawnSpecificDecoration(currentPosition);
                    }

                }
            }
        }
    }

    private void SpawnSpecificDecoration(Vector3Int position)
    {
        float randomDecorationValue = Random.value;
        float cumulativeProbability = 0f;

        foreach (DecorationType decorationType in decorationTypes)
        {
            cumulativeProbability += decorationType.probability;
            if (randomDecorationValue < cumulativeProbability)
            {
                //������ ��������, ����� ������ ��������� ��� ������
                Vector3 worldDecorationPosition = targetTilemap.GetCellCenterWorld(position);
                float tileHeight = targetTilemap.cellSize.y;
                worldDecorationPosition.y += tileHeight / 2f;


                //������ ������
                GameObject decoration = Instantiate(decorationType.prefab, worldDecorationPosition, Quaternion.identity);

                //�������� �������� �� X
                float randomFlip = Random.value;
                if (randomFlip < 0.5f) // 50% ����������� ���������
                {
                    Vector3 localScale = decoration.transform.localScale;
                    localScale.x *= -1f; // �������� �� ��� X
                    decoration.transform.localScale = localScale;
                }

                //������������� ��������
                decoration.transform.SetParent(_decorationsParent);
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
