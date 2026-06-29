using System.Collections.Generic;
using UnityEngine;

public class SupportSoliderManager : MonoBehaviour
{
    public List<SpawnSupportSoliderData> spawnSuportPlayerDatas;

    private void Start()
    {

        if (SelectedCardsHolder.SelectedSupportSolider.Count == 0)
        {
            return;
        }
        SpawnSupportSolider();
    }
    private void SpawnSupportSolider()
    {
        int count = Mathf.Min(SelectedCardsHolder.SelectedSupportSolider.Count, spawnSuportPlayerDatas.Count);



        for (int i = 0; i < count; i++)
        {
            var tower = SelectedCardsHolder.SelectedSupportSolider[i];

            if (tower == null || tower.SoliderSupportPrefab == null)
            {
                Debug.LogError("SupportSolider or Prefab is NULL at index " + i);
                continue;
            }
            Instantiate(
                SelectedCardsHolder.SelectedSupportSolider[i].SoliderSupportPrefab,
                spawnSuportPlayerDatas[i].PlayersPosition.position,
                spawnSuportPlayerDatas[i].PlayersPosition.rotation
            );
        }
    }
}
