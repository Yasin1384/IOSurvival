using System.Collections.Generic;
using UnityEngine;

public class PlayerManager : MonoBehaviour
{
    public static PlayerManager Instance { get; private set; }
    [SerializeField] private PlayerType_SO defaultPlayer;
    public Transform SoliderPosition;
    public CameraFollow cameraFollow;
    public List<PlayerType_SO> allPlayers;


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


        string savedID = PlayerPrefs.GetString("SelectedPlayer", "");

        if (!string.IsNullOrEmpty(savedID))
        {
            foreach (var player in allPlayers)
            {
                if (player.Name == savedID)
                {
                    playerToSpawn = player;
                    break;
                }
            }
        }

        if (playerToSpawn == null)
        {
            playerToSpawn = defaultPlayer;
        }

        GameObject solider = Instantiate(playerToSpawn.PlayerPrefab, SoliderPosition.position, SoliderPosition.rotation);

        cameraFollow.SetTarget(solider.transform);

        PlayerInput input = FindObjectOfType<PlayerInput>();
        PlayerMovement movement = solider.GetComponent<PlayerMovement>();

        if (input != null && movement != null)
        {
            movement.speed = playerToSpawn.Speed;
            input.SetPlayer(movement);

            Debug.Log("Player Connected");
        }
    }
    public PlayerType_SO GetDefaultPlayer()
    {
        return defaultPlayer;
    }
}
