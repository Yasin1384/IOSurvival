using UnityEngine;
[CreateAssetMenu(fileName = "GunsData", menuName = "Gun System/Guns Data")]

public class GunTypes_SO : ScriptableObject
{
    public string Name;

    public GameObject EnemyPrefab;

    public float BulletSpeed;

    public float SpeedSpawnBullet;
}
