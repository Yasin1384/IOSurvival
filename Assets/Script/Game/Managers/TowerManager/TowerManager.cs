using System.Collections.Generic;
using UnityEngine;

public class TowerManager : MonoBehaviour
{
    public List<SpawnTowerData> TowerData;

    public List<TowerDataTypes_SO> towerDataTypes;
    private void Start()
    {
        LoadTowers();
        if (SelectedCardsHolder.SelectedTowers.Count == 0)
        {
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

    void LoadTowers()
    {
        string json = PlayerPrefs.GetString("SelectedTowers", "");

        if (string.IsNullOrEmpty(json)) return;

        SpawnTowerData data = JsonUtility.FromJson<SpawnTowerData>(json);

        foreach (var id in data.towerIDs)
        {
            foreach (var tower in towerDataTypes)
            {
                if (tower.Name == id)
                {
                    SelectedCardsHolder.SelectedTowers.Add(tower);
                    break;
                }
            }
        }
    }
}
