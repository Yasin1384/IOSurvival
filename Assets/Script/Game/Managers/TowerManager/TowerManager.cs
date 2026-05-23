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
            Instantiate(
                SelectedTowerHolder.SelectedTower.TowerPrefab,
                TowerData[i].TowerPosition.position,
                TowerData[i].TowerPosition.rotation
            );
        }
    }
}
