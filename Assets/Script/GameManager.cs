using UnityEngine;

public class GameManager : MonoBehaviour
{
    public CameraFollow cameraFollow;
    public GameObject playerPrefab;
    Vector3 spawnPos = new Vector3(0, 0, -40);

    void Start() 
    {
        Spawn();
    }

    private void Spawn()
    {
        GameObject player = Instantiate(playerPrefab, spawnPos, Quaternion.identity);

        cameraFollow.SetTarget(player.transform);
    }

    private void TimerGame()
    {

    }

}
