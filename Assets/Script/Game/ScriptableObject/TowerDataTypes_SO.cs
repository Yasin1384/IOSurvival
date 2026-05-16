using UnityEngine;

[CreateAssetMenu(fileName = "GunsDefensiveData", menuName = "GunGunsDefensive System/GunsDefensive Data")]

public class TowerDataTypes_SO:ScriptableObject
{
    public string Name;

    public GameObject TowerPrefab;

    public float BulletSpeed;

    public float SpeedSpawnBullet;
}
