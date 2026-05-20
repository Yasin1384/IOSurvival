using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpawnBulletTowerMillitary : MonoBehaviour
{
    [Header("Dependencies")]
    [SerializeField] private AutoAimTower _autoAim;
    [SerializeField] private BulletTowerPool bulletPool;
    [SerializeField] private Transform firePoint;
    [SerializeField] private TowerDataTypes_SO towerData;

    [Header("Bullet Settings")]
    public string currentBulletType = "Missile";

    private float _spawnInterval;
    private float _bulletSpeed;

    private void Awake()
    {
        if (bulletPool == null)
        {
            bulletPool = FindObjectOfType<BulletTowerPool>();
        }

        if (bulletPool != null)
        {
            bulletPool.Initialize();
        }
    }

    private void Start()
    {
        if (towerData != null)
        {
            _spawnInterval = towerData.SpeedSpawnBullet;
            _bulletSpeed = towerData.BulletSpeed;
        }
        else
        {
            _spawnInterval = 1.0f;
            _bulletSpeed = 20.0f;
        }

        if (bulletPool != null)
        {
            StartCoroutine(SpawnBulletsRoutine());
        }
    }

    private void Fire()
    {
        BulletTypes_SO bulletSettings = bulletPool.GetBulletData(currentBulletType);

        GameObject target = _autoAim.FindNearestEnemyInRange();

        if (target != null)
        {
            Vector3 predictedPos = _autoAim.PredictDirectEnemyPosition(target);

            GameObject bulletInstance = bulletPool.Spawn(currentBulletType, firePoint.position);

            BulletTowerMilitary bullet = bulletInstance.GetComponent<BulletTowerMilitary>();

            if (bullet != null)
            {
                bullet.SetPool(bulletPool);
            }
            Rigidbody rb = bulletInstance.GetComponent<Rigidbody>();
            rb.useGravity = false;

            Vector3 direction = (predictedPos - firePoint.position).normalized;

            rb.linearVelocity = direction * _bulletSpeed;

            bulletInstance.transform.forward = direction;
        }
    }

    private IEnumerator SpawnBulletsRoutine()
    {
        while (true)
        {
            yield return new WaitForSeconds(_spawnInterval);
            Fire();
        }
    }
}
