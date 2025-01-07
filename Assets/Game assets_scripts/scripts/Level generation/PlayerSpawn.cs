using Cinemachine;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Tilemaps;
using UnityEngine.UIElements;

public class PlayerSpawn : MonoBehaviour
{
    [SerializeField] private Tilemap targetTilemap;
    [SerializeField] private GameObject playerPrefab;
    [SerializeField] private GameObject cameraPrefab;

    void Awake()
    {
        if (targetTilemap == null)
        {
            Debug.LogError("Tilemap не установлен!");
            enabled = false;
            return;
        }
        if (playerPrefab == null)
        {
            Debug.LogError("Player Prefab не установлен!");
            enabled = false;
            return;
        }
        if (cameraPrefab == null)
        {
            Debug.LogError("Camera Prefab не установлен!");
            enabled = false;
            return;
        }
    }

    public void SpawnPrefabs(int length)
    {
        if (targetTilemap == null || playerPrefab == null || cameraPrefab == null)
        {
            Debug.LogWarning("Ќе удалось создать префабы!");
            return;
        }

        for (int x = 0; x < length; x++)
        {
            for (int y = 0; y < length; y++)
            {
                Vector3Int startPosition = new Vector3Int(x, y, 0);

                if (CheckEmptyArea(startPosition))
                {
                    // Calculate center position
                    Vector3 centerPosition = targetTilemap.GetCellCenterWorld(startPosition + new Vector3Int(1, 1, 0));

                    // Create Player
                    GameObject player = Instantiate(playerPrefab, centerPosition, Quaternion.identity);

                    // Create Camera
                    GameObject camera = Instantiate(cameraPrefab, centerPosition, Quaternion.identity);

                    // Set camera to be a child
                    camera.transform.SetParent(transform);
                    // Set camera target

                    SetCameraTarget(camera, player);

                    // Stop searching
                    return;
                }
            }
        }
    }
    // New method to find Camera Follow
    private void SetCameraTarget(GameObject camera, GameObject player)
    {
        if (camera == null || player == null)
        {
            Debug.LogWarning("Camera or player are null, cant set camera target!");
            return;
        }

        CinemachineVirtualCamera playerCameraComponent = camera.GetComponentInChildren<CinemachineVirtualCamera>();
        if (playerCameraComponent == null)
        {
            Debug.LogWarning("PlayerCamera component не найден!");
            return;
        }
        playerCameraComponent.Follow = player.transform;

    }

    private bool CheckEmptyArea(Vector3Int startPos)
    {
        for (int x = 0; x < 4; x++)
        {
            for (int y = 0; y < 4; y++)
            {
                Vector3Int currentPos = startPos + new Vector3Int(x, y, 0);
                if (targetTilemap.GetTile(currentPos) != null)
                {
                    return false; // Found an occupied tile
                }
            }
        }
        return true; // All tiles in the 4x4 area are empty
    }
}
