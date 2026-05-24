using System.Collections.Generic;
using UnityEngine;
using static UnityEngine.LowLevelPhysics2D.PhysicsShape;

public class PlayerManager : MonoBehaviour
{
    public static PlayerManager Instance { get; private set; }

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

        GameObject solider = Instantiate(SelectedCardsHolder.SelectedPlayer.PlayerPrefab, SoliderPosition);

        cameraFollow.SetTarget(solider.transform);
    }
}
