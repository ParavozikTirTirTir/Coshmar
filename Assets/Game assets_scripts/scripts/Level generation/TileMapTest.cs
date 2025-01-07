using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Tilemaps;

//Алгоритм случайного блуждания

public class TileMapTest : MonoBehaviour
{
    public Tilemap tilemap; // Ссылка на Tilemap в сцене
    public string floorTilePath; // Путь к тайлу пола
    public Vector3Int startPosition; // Начальная позиция для блуждания
    public int walkLength;   // Длина случайного блуждания
    public int mapWidth;       // Ширина карты
    public int mapHeight;      // Высота карты
    public int borderSize;     // Размер границы от края карты
    public int tunnelWidth = 1;      // Ширина туннеля
    public float chanceToContinue = 0.8f; // Вероятность продолжить в том же направлении

    private TileBase floorTile;    // Тайл пола
    private List<Vector3Int> path = new List<Vector3Int>();// Путь случайного блуждания

    void Start()
    {
        startPosition = new Vector3Int(mapWidth / 2, mapHeight / 2, 0); // находим начальную позицию для блуждания
        LoadTile();   // Загружаем тайл из Resources
        GenerateMap(); // Генерируем карту с пещерой
    }

    void LoadTile()
    {
        // Загрузка тайла пола из Resources
        floorTile = Resources.Load<TileBase>(floorTilePath);

        // Проверка, был ли тайл загружен
        if (floorTile == null)
        {
            Debug.LogError("Тайл пола не найден по пути: " + floorTilePath + ". Убедитесь, что путь правильный и тайл находится в папке Resources.");
        }
    }


    void GenerateMap()
    {
        // 1. Заполняем карту тайлами пола
        for (int x = 0; x < mapWidth; x++)
        {
            for (int y = 0; y < mapHeight; y++)
            {
                tilemap.SetTile(new Vector3Int(x, y, 0), floorTile);
            }
        }
        // 2. Генерируем путь случайного блуждания
        GenerateRandomWalk();

        // 3. Удаляем тайлы пола в соответствии с путем блуждания:
        foreach (Vector3Int position in path) // перебираем элементы коллекции
        {
            ClearTunnel(position);
        }


        // Сообщение об успехе:
        Debug.Log("Карта сгенерирована с пещерой по алгоритму случайного блуждания.");
    }

    //вырезаем окно из тайлов в зависимости от tunnelWidth для увеличения ширины пути
    void ClearTunnel(Vector3Int centerTile)
    {
        for (int x = -tunnelWidth; x <= tunnelWidth; x++)
        {
            for (int y = -tunnelWidth; y <= tunnelWidth; y++)
            {
                Vector3Int neighbourPos = centerTile + new Vector3Int(x, y, 0);
                tilemap.SetTile(neighbourPos, null);
            }
        }
    }

    void GenerateRandomWalk()
    {
        path.Clear();
        Vector3Int currentPosition = startPosition;
        Vector3Int previousDir = Vector3Int.zero;
        path.Add(currentPosition);

        for (int i = 0; i < walkLength; i++)
        {
            Vector3Int nextPosition = GetNextPosition(currentPosition, previousDir);
            previousDir = nextPosition - currentPosition;
            path.Add(nextPosition);
            currentPosition = nextPosition;
        }
    }

    Vector3Int GetNextPosition(Vector3Int currentPos, Vector3Int previousDir)
    {
        Vector3Int nextPos = GetRandomDirection(currentPos, previousDir);

        // Проверяем, не выходит ли nextPos за границу, отступая borderSize тайлов
        int minX = borderSize;
        int maxX = mapWidth - 1 - borderSize;
        int minY = borderSize;
        int maxY = mapHeight - 1 - borderSize;


        nextPos.x = Mathf.Clamp(nextPos.x, minX, maxX);
        nextPos.y = Mathf.Clamp(nextPos.y, minY, maxY);
        return nextPos;
    }

    Vector3Int GetRandomDirection(Vector3Int currentPos, Vector3Int previousDir)
    {
        if (Random.value <= chanceToContinue && previousDir != Vector3Int.zero)
        {
            return currentPos + previousDir;
        }

        int direction = Random.Range(0, 4);
        switch (direction)
        {
            case 0: return currentPos + Vector3Int.right;
            case 1: return currentPos + Vector3Int.left;
            case 2: return currentPos + Vector3Int.up;
            case 3: return currentPos + Vector3Int.down;
        }
        return currentPos;
    }
}