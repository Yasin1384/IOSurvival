using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    public static GameManager Instance { get; private set; }



    public CameraFollow cameraFollow;
    public GameObject playerPrefab;
    Vector3 spawnPos = new Vector3(0, 0, -40);

    private void Awake()
    {
        if (Instance == null)
        {
            Instance = this;

        }
        else
        {
            Destroy(gameObject);
        }
    }
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
