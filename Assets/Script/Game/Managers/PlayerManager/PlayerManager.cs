using System.Collections.Generic;
using UnityEngine;
using static UnityEngine.LowLevelPhysics2D.PhysicsShape;

public class PlayerManager : MonoBehaviour
{
    public static PlayerManager Instance { get; private set; }
    [SerializeField] private PlayerType_SO defaultPlayer;
    public Transform SoliderPosition;
    public CameraFollow cameraFollow;
    
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
        PlayerType_SO playerToSpawn = SelectedCardsHolder.SelectedPlayer;

        if (playerToSpawn == null)
        {
            Debug.Log("No player selected, using default.");
            playerToSpawn = defaultPlayer;
        }

        if (playerToSpawn == null || playerToSpawn.PlayerPrefab == null)
        {
            Debug.LogError("Player prefab is missing!");
            return;
        }

        GameObject solider = Instantiate(playerToSpawn.PlayerPrefab, SoliderPosition.position, SoliderPosition.rotation);

        cameraFollow.SetTarget(solider.transform);
    }
}
