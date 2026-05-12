using UnityEngine;

[CreateAssetMenu(fileName = "GunsDefensiveData", menuName = "GunGunsDefensive System/GunsDefensive Data")]

public class DefenseTypes_SO:ScriptableObject
{
    public string Name;

    public GameObject EnemyPrefab;

    public float BulletSpeed;

    public float SpeedSpawnBullet;
}
