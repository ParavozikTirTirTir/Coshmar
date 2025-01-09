using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Tilemaps;

//Черви Перлина
public class TileMapTest4 : MonoBehaviour
{
    //public PlayerSpawn playerSpawn;
    //public OreSpawn oreSpawn;

    public Tilemap tilemap;
    public string floorTilePath;
    public Vector3Int startPosition;
    public int walkLength;
    public int mapWidth;
    public int mapHeight;
    public int borderSize;
    public int tunnelWidth = 1;
    public float perlinNoiseScale = 0.1f; // Новая переменная для масштаба Перлина
    public float perlinNoiseThreshold = 0.5f; // Новая переменная для порога Перлина
    //алгоритм совмещает элементы случайного блуждания и шума Перлина

    private TileBase floorTile;
    private List<Vector3Int> path = new List<Vector3Int>();

    void Start()
    {
        startPosition = new Vector3Int(mapWidth / 2, mapHeight / 2, 0);
        LoadTile();
        GenerateMap();
        //playerSpawn = GetComponent<PlayerSpawn>();
        //oreSpawn = GetComponent<OreSpawn>();

        //if (playerSpawn != null)
        //{

        //    playerSpawn.SpawnPrefabs(mapWidth);
        //}

        //if (oreSpawn != null)
        //{

        //    oreSpawn.SpawnOre(mapWidth);
        //}

        void LoadTile()
        {
            floorTile = Resources.Load<TileBase>(floorTilePath);
            if (floorTile == null)
            {
                Debug.LogError("Тайл пола не найден.");
            }
        }

        void GenerateMap()
        {
            // Инициализация карты пустым полом
            for (int x = 0; x < mapWidth; x++)
            {
                for (int y = 0; y < mapHeight; y++)
                {
                    tilemap.SetTile(new Vector3Int(x, y, 0), floorTile);
                }
            }

            //Генерация пути с использованием Perlin Noise
            GeneratePathWithPerlin();


            foreach (Vector3Int position in path)
            {
                ClearTunnel(position);
            }

            Debug.Log("Карта сгенерирована с пещерой.");
        }
        void GeneratePathWithPerlin() // добавление пути в список
        {
            path.Clear();
            Vector3Int currentPosition = startPosition;
            path.Add(currentPosition);

            for (int i = 0; i < walkLength; i++)
            {
                Vector3Int nextPosition = GetNextPositionWithPerlin(currentPosition);
                path.Add(nextPosition);
                currentPosition = nextPosition;
            }
        }

        Vector3Int GetNextPositionWithPerlin(Vector3Int currentPos) // генерируем следующую позицию пути
        {
            // Используем Perlin Noise для выбора направления
            int newX = currentPos.x;
            int newY = currentPos.y;

            float noiseValue = Mathf.PerlinNoise(currentPos.x * perlinNoiseScale, currentPos.y * perlinNoiseScale); // получаем значение тайла, с помощью шума Перлина
            // Учитываем мастшаб шума 

            if (noiseValue > perlinNoiseThreshold) // учитываем порог
            {// больше порога движение по оси X
                if (Random.value < 0.5f) // выбираем направление
                    newX += 1;
                else
                    newX -= 1;

            }
            else
            {// меньше порога движение по оси Y
                if (Random.value < 0.5f) // выбираем направление
                    newY += 1;
                else
                    newY -= 1;
            }

            // Ограничение границ
            int minX = borderSize;
            int maxX = mapWidth - 1 - borderSize;
            int minY = borderSize;
            int maxY = mapHeight - 1 - borderSize;

            newX = Mathf.Clamp(newX, minX, maxX);
            newY = Mathf.Clamp(newY, minY, maxY);

            return new Vector3Int(newX, newY, 0);
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


    }
}
