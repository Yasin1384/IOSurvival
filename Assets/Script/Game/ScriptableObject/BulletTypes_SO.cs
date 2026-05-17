using UnityEngine;
[CreateAssetMenu(fileName = "BulletsData", menuName = "Bullet System/Bullets Data")]

public class BulletTypes_SO : ScriptableObject
{
    public string Name;

    public GameObject BulletPrefab;

    public float BulletSpeed;

    public float SpeedSpawnBullet;
}
