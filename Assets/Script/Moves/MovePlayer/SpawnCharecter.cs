using UnityEngine;

public class SpawnCharecter : MonoBehaviour
{
    public GameObject playerPrefab;
    public Transform spawnPoint;
    void Start()
    {
        SpawnPlayer();
    }
    void SpawnPlayer()
    {
        if (playerPrefab != null && spawnPoint != null)
        {
            Instantiate(playerPrefab,new Vector3(0, 1, 0), spawnPoint.rotation);
        }
        else
        {
            Debug.LogError("Player Prefab or Spawn Point is not assigned.");
        }
    }
}
