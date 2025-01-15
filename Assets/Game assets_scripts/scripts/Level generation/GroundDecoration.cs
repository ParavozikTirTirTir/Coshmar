using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Tilemaps;


public class GroundDecoration : MonoBehaviour
{
    [SerializeField] private Tilemap targetTilemap;

    [SerializeField] private List<DecorationType> decorationTypes;

    [SerializeField] private List<DecorationType> decorationTypesBottom;

    [SerializeField] private List<DecorationType> decorationTypesLarge;

    [SerializeField][Range(0f, 1f)] private float overallSpawnProbability = 0.1f;
    [SerializeField][Range(0f, 1f)] private float overallSpawnProbabilityBottom = 0.1f;
    [SerializeField][Range(0f, 1f)] private float overallSpawnProbabilityLarge = 0.1f;


    [SerializeField] private CaveTexture caveTexture;
    [SerializeField] private string parentObjectName = "DecorationsParent";//

    private Transform _decorationsParent;//

    void Awake()
    {

        // Ищем или создаем объект родителя
        _decorationsParent = transform.Find(parentObjectName);//
        if (_decorationsParent == null)
        {
            GameObject parentGO = new GameObject(parentObjectName);
            _decorationsParent = parentGO.transform;
            _decorationsParent.SetParent(transform);
        }

        if (targetTilemap == null)
        {
            Debug.LogError("Tilemap не установлен!");
            enabled = false;
            return;
        }
        //if (decorationTypes == null || decorationTypes.Count == 0)
        //{
        //    Debug.LogError("Список префабов декораций пуст или не установлен!");
        //    enabled = false;
        //    return;
        //}
        //foreach (DecorationType decorationType in decorationTypes)
        //{
        //    if (decorationType.prefab == null)
        //    {
        //        Debug.LogError("Один из префабов декораций не установлен!");
        //        enabled = false;
        //        return;
        //    }
        //}

        //if (decorationTypesBottom == null || decorationTypesBottom.Count == 0)
        //{
        //    Debug.LogError("Список префабов декораций пуст или не установлен!");
        //    enabled = false;
        //    return;
        //}
        //foreach (DecorationType decorationType in decorationTypesBottom)
        //{
        //    if (decorationType.prefab == null)
        //    {
        //        Debug.LogError("Один из префабов декораций не установлен!");
        //        enabled = false;
        //        return;
        //    }
        //}

        //if (decorationTypesLarge == null || decorationTypesLarge.Count == 0)
        //{
        //    Debug.LogError("Список префабов декораций пуст или не установлен!");
        //    enabled = false;
        //    return;
        //}
        //foreach (DecorationType decorationType in decorationTypesLarge)
        //{
        //    if (decorationType.prefab == null)
        //    {
        //        Debug.LogError("Один из префабов декораций не установлен!");
        //        enabled = false;
        //        return;
        //    }
        //}

        if (caveTexture == null)
        {
            Debug.LogError("CaveTexture не установлен!");
            enabled = false;
            return;
        }

        

    }

    public void SpawnDecorations(Vector3Int startPosition, int mapWidth, int mapHeight)
    {
        if (targetTilemap == null || caveTexture == null)
        {
            Debug.LogWarning("Не удалось создать декорации!");
            return;
        }

        for (int x = startPosition.x + 1; x < startPosition.x + mapWidth - 1; x++)
        {
            for (int y = startPosition.y + 1; y < startPosition.y + mapHeight - 1; y++)
            {
                Vector3Int currentPosition = new Vector3Int(x, y, 0);

                TileBase currentTile = targetTilemap.GetTile(currentPosition);


                if (currentTile != null && caveTexture.IsGroundTile(currentTile) && decorationTypes.Count != 0 && IsHorizontalSurface(currentPosition))
                {
                    float randomValue = Random.value;
                    if (randomValue < overallSpawnProbability)
                    {
                        SpawnSpecificDecoration(currentPosition);
                    }

                }

                if (currentTile != null && caveTexture.IsGroundTile(currentTile) && decorationTypesBottom.Count != 0 && IsHorizontalSurfaceBottom(currentPosition))
                {
                    float randomValue = Random.value;
                    if (randomValue < overallSpawnProbabilityBottom)
                    {
                        SpawnSpecificDecorationBottom(currentPosition);
                    }

                }

                if (currentTile != null && caveTexture.IsGroundTile(currentTile) && decorationTypesLarge.Count != 0 && IsHorizontalSurfaceLarge(currentPosition))
                {
                    float randomValue = Random.value;
                    if (randomValue < overallSpawnProbabilityLarge)
                    {
                        SpawnSpecificDecorationLarge(currentPosition);
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
                //создаём смещение, чтобы объект появлялся над тайлом
                Vector3 worldDecorationPosition = targetTilemap.GetCellCenterWorld(position);
                float tileHeight = targetTilemap.cellSize.y;
                worldDecorationPosition.y += tileHeight / 2f;


                //Создаём префаб
                GameObject decoration = Instantiate(decorationType.prefab, worldDecorationPosition, Quaternion.identity);

                //Случайно отражаем по X
                float randomFlip = Random.value;
                if (randomFlip < 0.5f) // 50% вероятность отражения
                {
                    Vector3 localScale = decoration.transform.localScale;
                    localScale.x *= -1f; // Отражаем по оси X
                    decoration.transform.localScale = localScale;
                }

                //Устанавливаем родителя
                decoration.transform.SetParent(_decorationsParent);//
                return; // Выходим после создания
            }
        }
    }


    private void SpawnSpecificDecorationBottom(Vector3Int position)
    {
        float randomDecorationValue = Random.value;
        float cumulativeProbability = 0f;

        foreach (DecorationType decorationType in decorationTypesBottom)
        {
            cumulativeProbability += decorationType.probability;
            if (randomDecorationValue < cumulativeProbability)
            {
                //создаём смещение, чтобы объект появлялся под тайлом
                Vector3 worldDecorationPosition = targetTilemap.GetCellCenterWorld(position);
                float tileHeight = targetTilemap.cellSize.y;
                worldDecorationPosition.y -= tileHeight / 2f;


                //Создаём префаб
                GameObject decoration = Instantiate(decorationType.prefab, worldDecorationPosition, Quaternion.identity);

                //Случайно отражаем по X
                float randomFlip = Random.value;
                if (randomFlip < 0.7f) // 50% вероятность отражения
                {
                    Vector3 localScale = decoration.transform.localScale;
                    localScale.x *= -1f; // Отражаем по оси X
                    decoration.transform.localScale = localScale;
                }

                //Устанавливаем родителя
                decoration.transform.SetParent(_decorationsParent);//
                return; // Выходим после создания
            }
        }
    }

    private void SpawnSpecificDecorationLarge(Vector3Int position)
    {
        float randomDecorationValue = Random.value;
        float cumulativeProbability = 0f;

        foreach (DecorationType decorationType in decorationTypesLarge)
        {
            cumulativeProbability += decorationType.probability;
            if (randomDecorationValue < cumulativeProbability)
            {
                //создаём смещение, чтобы объект появлялся под тайлом
                Vector3 worldDecorationPosition = targetTilemap.GetCellCenterWorld(position);
                float tileHeight = targetTilemap.cellSize.y;
                worldDecorationPosition.y += tileHeight / 2f;


                //Создаём префаб
                GameObject decoration = Instantiate(decorationType.prefab, worldDecorationPosition, Quaternion.identity);

                //Случайно отражаем по X
                float randomFlip = Random.value;
                if (randomFlip < 0.7f) // 50% вероятность отражения
                {
                    Vector3 localScale = decoration.transform.localScale;
                    localScale.x *= -1f; // Отражаем по оси X
                    decoration.transform.localScale = localScale;
                }

                //Устанавливаем родителя
                decoration.transform.SetParent(_decorationsParent);//
                return; // Выходим после создания
            }
        }
    }


    private bool IsHorizontalSurface(Vector3Int position)
    {
        // Горизонтальная поверхность - если над тайлом нет другого тайла
        Vector3Int upPosition = position + new Vector3Int(0, 1, 0);
        return targetTilemap.GetTile(upPosition) == null;
    }

    private bool IsHorizontalSurfaceBottom(Vector3Int position)
    {
        // Горизонтальная поверхность - если над тайлом нет другого тайла
        Vector3Int upPosition = position + new Vector3Int(0, -1, 0);
        return targetTilemap.GetTile(upPosition) == null;
    }

    private bool IsHorizontalSurfaceLarge(Vector3Int position)
    {
        // Горизонтальная поверхность - если над тайлом нет другого тайла
        for (int x = -1; x <= 1; x++)
        {
            for (int y = 1; y <= 5; y++)
            {
                Vector3Int upPosition = position + new Vector3Int(x, y, 0);
                if (targetTilemap.GetTile(upPosition) != null) return false;
            }
        }

        for (int x = -1; x <= 1; x++)
        {
            Vector3Int upPosition = position + new Vector3Int(x, 0, 0);
            if (targetTilemap.GetTile(upPosition) == null) return false;
        }
        return true;
    }
}
