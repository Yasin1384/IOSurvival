using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpawnBulletTowerCannon : MonoBehaviour
{
    [Header("Dependencies")]
    [SerializeField] private AutoAimTower _autoAim;
    [SerializeField] private BulletTowerPool bulletPool;
    [SerializeField] private Transform firePoint;
    [SerializeField] private TowerDataTypes_SO towerData;

    [Header("Bullet Settings")]
    public string currentBulletType = "Missile";

    [Header("Shooting Logic")]
    [SerializeField] private float arcHeight = 10f;
    private float _spawnTimes;
    private float nextFireTime = 0f;

    public void Initialize(BulletTowerPool poolManager)
    {
        bulletPool = poolManager;
        if (bulletPool != null)
        {
            bulletPool.Initialize();
        }
        else
        {
        }
    }

    private void Awake()
    {
        if (bulletPool == null)
        {
            bulletPool = FindObjectOfType<BulletTowerPool>();
            if (bulletPool == null)
            {
            }
        }
    }

    private void Start()
    {
        if (towerData != null)
        {
            _spawnTimes = towerData.SpeedSpawnBullet;
        }
        else
        {
            _spawnTimes = 1.0f;
        }

        if (bulletPool != null)
        {
            StartCoroutine(SpawnBulletsRoutine());
        }
    }

    private IEnumerator SpawnBulletsRoutine()
    {
        while (true)
        {
            if (Time.time >= nextFireTime)
            {
                if (_autoAim != null && _autoAim.enabled)
                {
                    GameObject target = _autoAim.FindNormalEnemyInRange();
                    if (target != null)
                    {
                        FireAtTarget(target);
                        nextFireTime = Time.time + _spawnTimes;
                    }
                }
                else
                {
                    nextFireTime = Time.time + _spawnTimes;
                }
            }
            yield return null;
        }
    }

    private void FireAtTarget(GameObject target)
    {

        BulletTypes_SO bulletSettings = bulletPool.GetBulletData(currentBulletType);

        Vector3 predictedPos = _autoAim.PredictBallisticEnemyPosition(target);

        GameObject bulletInstance = bulletPool.Spawn(currentBulletType, firePoint.position);

        BulletTowerCannon bullet = bulletInstance.GetComponent<BulletTowerCannon>();
        if (bullet != null)
        {
            bullet.SetPoolCannon(bulletPool);
        }

        Rigidbody rb = bulletInstance.GetComponent<Rigidbody>();

        Vector3 startPosition = firePoint.position;
        Vector3 launchVelocity = GetCannonLaunchVelocity(startPosition, predictedPos, arcHeight);

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
}
