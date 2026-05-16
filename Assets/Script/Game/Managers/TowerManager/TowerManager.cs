using System.Collections.Generic;
using UnityEngine;

public class TowerManager : MonoBehaviour
{
    public List<SpawnTowerData> TowerData;

    private void Start()
    {
        SpawnTowers();
    }

    private void SpawnTowers()
    {
        for (int i = 0; i < TowerData.Count; i++)
        {
            Instantiate(TowerData[i].TowerData.TowerPrefab, TowerData[i].TowerPosition);
        }
    }
}
