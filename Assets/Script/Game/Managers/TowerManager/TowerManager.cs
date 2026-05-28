using System.Collections.Generic;
using UnityEngine;

public class TowerManager : MonoBehaviour
{
    public List<SpawnTowerData> TowerData;

    private void Start()
    {
        Debug.Log("Start TowerManager");

        Debug.Log("Selected Towers: " + SelectedCardsHolder.SelectedTowers.Count);
        Debug.Log("Spawn Points: " + TowerData.Count);

        if (SelectedCardsHolder.SelectedTowers.Count == 0)
        {
            Debug.LogError("No towers selected!");
            return;
        }
        SpawnTowers();
    }
    private void SpawnTowers()
    {
        int count = Mathf.Min(SelectedCardsHolder.SelectedTowers.Count, TowerData.Count);

        for (int i = 0; i < count; i++)
        {
            var tower = SelectedCardsHolder.SelectedTowers[i];

            if (tower == null || tower.TowerPrefab == null)
            {
                Debug.LogError("Tower or Prefab is NULL at index " + i);
                continue;
            }
            Instantiate(
                SelectedCardsHolder.SelectedTowers[i].TowerPrefab,
                TowerData[i].TowerPosition.position,
                TowerData[i].TowerPosition.rotation
            );
        }
    }
}
