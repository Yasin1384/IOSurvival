using System.Collections.Generic;
using UnityEngine;
using static UnityEngine.LowLevelPhysics2D.PhysicsShape;

public class PlayerManager : MonoBehaviour
{
    public static PlayerManager Instance { get; private set; }

    public GameObject SoliderPlayer;
    public Transform SoliderPosition;
    public CameraFollow cameraFollow;
    
    public List<SpawnSuportPlayerData> spawnSuportPlayerDatas;
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
    private void Start()
    {
        GameObject solider = Instantiate(SoliderPlayer, SoliderPosition);
        cameraFollow.SetTarget(solider.transform);

        SpawnSuportPlayer();
    }

    private void SpawnSuportPlayer()
    {
        for (int i = 0; i < spawnSuportPlayerDatas.Count; i++)
        {
            Instantiate(spawnSuportPlayerDatas[i].PlayerTypes.PlayerPrefab, spawnSuportPlayerDatas[i].PlayersPosition);
        }

    }
}
