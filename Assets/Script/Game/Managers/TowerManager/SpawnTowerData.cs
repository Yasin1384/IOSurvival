using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class SpawnTowerData
{
    public Transform TowerPosition;

    public List<string> towerIDs;

    public SpawnTowerData(List<string> ids)
    {
        towerIDs = ids;
    }
}
