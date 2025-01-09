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
    [SerializeField][Range(0f, 1f)] private float overallSpawnProbability = 0.1f; // Общая вероятность появления врага
    [SerializeField] private int clearanceSize = 3; // Размер области для проверки


    void Awake()
    {
        if (targetTilemap == null)
        {
            Debug.LogError("Tilemap не установлен!");
            enabled = false;
            return;
        }
        if (enemyTypes == null || enemyTypes.Count == 0)
        {
            Debug.LogError("Список префабов врагов пуст или не установлен!");
            enabled = false;
            return;
        }
        foreach (EnemyType enemyType in enemyTypes)
        {
            if (enemyType.prefab == null)
            {
                Debug.LogError("Один из префабов врагов не установлен!");
                enabled = false;
                return;
            }
        }

    }

    public void SpawnEnemies(Vector3Int startPosition, int mapWidth, int mapHeight)
    {
        if (targetTilemap == null || enemyTypes == null || enemyTypes.Count == 0)
        {
            Debug.LogWarning("Не удалось создать врага!");
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
                return; // Выходим после создания
            }
        }
    }
    private bool IsClearSpace(Vector3Int position)
    {
        //Проверка наличия тайла в самой позиции спавна.
        if (targetTilemap.GetTile(position) == null)
        {
            return false; // Если на позиции спавна нет тайла - спавн невозможен.
        }

        //Проверка наличия тайлов по оси X (слева и справа от позиции).
        if (targetTilemap.GetTile(position + Vector3Int.left) == null ||
            targetTilemap.GetTile(position + Vector3Int.right) == null)
        {
            return false;  // Если нет тайлов слева или справа - спавн невозможен.
        }

        //Проверка свободного пространства над платформой спавна (3x2, включая верхний ряд)
        for (int x = -1; x <= 1; x++)
        {
            for (int y = 1; y <= clearanceSize - 1; y++) //начинаем проверку с y = 1, чтобы проверять именно над платформой
            {
                Vector3Int checkPosition = position + new Vector3Int(x, y, 0);
                if (targetTilemap.GetTile(checkPosition) != null)
                {
                    return false; // Если хотя бы один тайл есть, то пространство не свободно
                }
            }
        }
        return true; // Если все условия выполнены, то пространство свободно
    }
}
