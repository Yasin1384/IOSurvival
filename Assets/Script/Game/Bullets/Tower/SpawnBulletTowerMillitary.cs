using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpawnBulletTowerMillitary : MonoBehaviour
{
    [SerializeField] private AutoAimTower _autoAim;
    [SerializeField] private int poolSize = 50;

    [SerializeField] private GameObject bulletPrefab;

    private List<BulletTowerMillitaryPool> pools = new List<BulletTowerMillitaryPool>();
    public TowerDataTypes_SO gunTypes;

    public BulletTowerMillitaryPool bulletPool;

    [SerializeField] private Transform defaultPosition;

    private float _spawnTimes;
    private float _SpeedBullet;

    private void Start()
    {
        _spawnTimes = gunTypes.SpeedSpawnBullet;
        _SpeedBullet = gunTypes.BulletSpeed;
    }

    private void Awake()
    {
        pools = new List<BulletTowerMillitaryPool>();

        bulletPool.Initialize(bulletPrefab, poolSize);
        pools.Add(bulletPool);

        StartCoroutine(SpawnBullets());
    }

    private void Spawn()
    {

        GameObject target = _autoAim.FindNearestEnemyInRange(); 

        if (target != null)
        {
            Vector3 predictedPos = _autoAim.PredictDirectEnemyPosition(target);

            GameObject bulletInstance = bulletPool.Spawn(defaultPosition.position);

            BulletTowerMilitary bullet = bulletInstance.GetComponent<BulletTowerMilitary>();

            bullet.SetPool(bulletPool);

            Rigidbody rb = bulletInstance.GetComponent<Rigidbody>();
            rb.useGravity = false;

            Vector3 direction = (predictedPos - defaultPosition.position).normalized;

            rb.linearVelocity = direction * _SpeedBullet;

            bulletInstance.transform.forward = direction;
        }
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
