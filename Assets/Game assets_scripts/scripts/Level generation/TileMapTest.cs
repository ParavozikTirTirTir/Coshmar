using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Tilemaps;

//Алгоритм случайного блуждания

public class TileMapTest : MonoBehaviour
{
    public Tilemap tilemap; // Ссылка на Tilemap в сцене
    public string floorTilePath; // Путь к тайлу пола (текстура)
    public Vector3Int startPosition; // Начальная позиция для блуждания
    public int walkLength;   // Длина случайного блуждания
    public int mapWidth;       // Ширина карты
    public int mapHeight;      // Высота карты
    public int borderSize;     // Размер границы от края карты
    public int tunnelWidth = 1;      // Ширина туннеля (в данном случае получается 3)
    public float chanceToContinue = 0.8f; // Вероятность продолжить в том же направлении

    private TileBase floorTile;    // Тайл пола
    private List<Vector3Int> path = new List<Vector3Int>();// Путь случайного блуждания (список координат)

    void Start() 
    {
        startPosition = new Vector3Int(mapWidth / 2, mapHeight / 2, 0); // находим начальную позицию для блуждания
        LoadTile();   // Загружаем тайл из Resources
        GenerateMap(); // Генерируем карту с пещерой
    }

    void LoadTile()
    {
        // Загрузка тайла пола из папки Resources
        floorTile = Resources.Load<TileBase>(floorTilePath);

        // Проверка, был ли тайл загружен
        if (floorTile == null)
        {
            Debug.LogError("Тайл пола не найден по пути: " + floorTilePath + ". Убедитесь, что путь правильный и тайл находится в папке Resources.");
        }
    }


    void GenerateMap()
    {
        //Заполняем карту тайлами пола
        for (int x = 0; x < mapWidth; x++)
        {
            for (int y = 0; y < mapHeight; y++)
            {
                tilemap.SetTile(new Vector3Int(x, y, 0), floorTile); // устанавливаем тайл в координаты карты
            }
        }
        //Генерируем путь случайного блуждания
        GenerateRandomWalk();

        //Удаляем тайлы пола в соответствии с путем блуждания:
        foreach (Vector3Int position in path) // перебираем элементы коллекции
        {
            ClearTunnel(position); //Метод для удаления окна тайлов
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
                Vector3Int neighbourPos = centerTile + new Vector3Int(x, y, 0); // прибавляем вектор с координатами к исходной позиции, чтобы удалить соседей
                tilemap.SetTile(neighbourPos, null); // удаляем тайл из позиции
            }
        }
    }

    // генерация пути 
    void GenerateRandomWalk()
    {
        path.Clear(); // отчищаем список path
        Vector3Int currentPosition = startPosition; // начало блуждания 
        Vector3Int previousDir = Vector3Int.zero; // начальное направление = 0
        path.Add(currentPosition); // добавляем в список начальную позицию

        for (int i = 0; i < walkLength; i++) // проходим колличество установленных шагов
        {
            Vector3Int nextPosition = GetNextPosition(currentPosition, previousDir); //Получаем следующую позицию
            previousDir = nextPosition - currentPosition; // Из разници позиций находим направление
            path.Add(nextPosition); // добавляем в список позицию
            currentPosition = nextPosition; // сдвигаем настояющую позицию 
        }
    }

    //получаем следующую позицию пути в пределах карты
    Vector3Int GetNextPosition(Vector3Int currentPos, Vector3Int previousDir)
    {
        Vector3Int nextPos = GetRandomDirection(currentPos, previousDir); // получаем случайное направление

        // Проверяем, не выходит ли nextPos за границу, отступая borderSize тайлов
        int minX = borderSize;
        int maxX = mapWidth - 1 - borderSize;
        int minY = borderSize;
        int maxY = mapHeight - 1 - borderSize;


        nextPos.x = Mathf.Clamp(nextPos.x, minX, maxX); //Функция, которая ограничивает значение в пределах
        nextPos.y = Mathf.Clamp(nextPos.y, minY, maxY);
        return nextPos;
    }

    // Случайно генерируем направление пути
    Vector3Int GetRandomDirection(Vector3Int currentPos, Vector3Int previousDir)
    {
        if (Random.value >= chanceToContinue && previousDir != Vector3Int.zero) // зависимость от шанса продолжнить направление
        {//(предыдущее направление не = 0)
            return currentPos + previousDir; // Продолжаем движение в том же направлении
        }

        int direction = Random.Range(0, 4); // генерируем случайное направление
        switch (direction) 
        {
            case 0: return currentPos + Vector3Int.right; //(1, 0, 0) сдвигаем текущий вектор вправо
            case 1: return currentPos + Vector3Int.left;
            case 2: return currentPos + Vector3Int.up;
            case 3: return currentPos + Vector3Int.down;
        }
        return currentPos;
    }
}