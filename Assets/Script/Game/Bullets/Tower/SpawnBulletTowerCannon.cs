using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpawnBulletTowerCannon : MonoBehaviour
{
    [SerializeField] private AutoAimTower _autoAim;
    [SerializeField] private int poolSize = 50;

    [SerializeField] private GameObject bulletPrefab;

    private List<BulletTowerCannonPool> pools = new List<BulletTowerCannonPool>();
    public TowerDataTypes_SO gunTypes;

    public BulletTowerCannonPool bulletPool;

    [SerializeField] private Transform defaultPosition;

    private float _spawnTimes;
    private float _SpeedBullet;
    private float arcHeight = 10;
    private void Start()
    {
        _spawnTimes = gunTypes.SpeedSpawnBullet;
        _SpeedBullet = gunTypes.BulletSpeed;
    }
    private void Awake()
    {
        pools = new List<BulletTowerCannonPool>();

        bulletPool.Initialize(bulletPrefab, poolSize);
        pools.Add(bulletPool);

        StartCoroutine(SpawnBullets());
    }

    private void Spawn()
    {
        GameObject target = _autoAim.FindNormalEnemyInRange();
        if (target == null) return;

        Vector3 predictedPos = _autoAim.PredictBallisticEnemyPosition(target);

        GameObject bulletInstance = bulletPool.Spawn(defaultPosition.position);
        if (bulletInstance == null) return;

        BulletTowerCannon bullet = bulletInstance.GetComponent<BulletTowerCannon>();
        bullet.SetPoolCannon(bulletPool);

        Rigidbody rb = bulletInstance.GetComponent<Rigidbody>();
        if (rb == null) return;

        Vector3 start = defaultPosition.position;

        Vector3 launchVelocity = GetCannonLaunchVelocity(start, predictedPos, 60f);

        rb.useGravity = true;
        rb.linearVelocity = launchVelocity;

        bulletInstance.transform.forward = launchVelocity.normalized;
    }

    private Vector3 GetCannonLaunchVelocity(Vector3 start, Vector3 target, float angleDegrees)
    {
        float g = Mathf.Abs(Physics.gravity.y);

        Vector3 toTarget = target - start;
        Vector3 toTargetXZ = new Vector3(toTarget.x, 0f, toTarget.z);

        float x = toTargetXZ.magnitude;
        float y = toTarget.y;

        float angle = angleDegrees * Mathf.Deg2Rad;

        float cos = Mathf.Cos(angle);
        float sin = Mathf.Sin(angle);

        float denominator = 2 * cos * cos * (x * Mathf.Tan(angle) - y);

        if (denominator <= 0f)
            return Vector3.zero;

        float speedSquared = (g * x * x) / denominator;

        if (speedSquared <= 0f)
            return Vector3.zero;

        float speed = Mathf.Sqrt(speedSquared);

        Vector3 velocity =
            toTargetXZ.normalized * speed * cos +
            Vector3.up * speed * sin;

        return velocity;
    }

    private IEnumerator SpawnBullets()
    {
        while (true)
        {
            yield return new WaitForSeconds(_spawnTimes);
            Spawn();
        }
    }
}
