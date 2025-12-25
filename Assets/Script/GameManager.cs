using UnityEngine;

public class GameManager : MonoBehaviour
{
    public CameraFollow cameraFollow;
    public GameObject playerPrefab;
    Vector3 spawnPos = new Vector3(0, 1, 0);

    void Start()
    {
        GameObject player = Instantiate(playerPrefab, spawnPos, Quaternion.identity);

        cameraFollow.SetTarget(player.transform);
    }
}
