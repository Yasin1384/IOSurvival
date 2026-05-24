using System.Collections.Generic;
using UnityEngine;
using static UnityEngine.LowLevelPhysics2D.PhysicsShape;

public class SupportSoliderManager : MonoBehaviour
{
    public List<SpawnSupportSoliderData> spawnSuportPlayerDatas;

    private void Start()
    {
        SpawnSuportPlayer();
    }
    private void SpawnSuportPlayer()
    {
        for (int i = 0; i < spawnSuportPlayerDatas.Count; i++)
        {
            Instantiate(
                SelectedCardsHolder.SelectedSupportSolider.SoliderSupportPrefab,
                spawnSuportPlayerDatas[i].PlayersPosition.position,
                spawnSuportPlayerDatas[i].PlayersPosition.rotation
            );
        }

    }
}
