using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BulletTower : MonoBehaviour
{
    [SerializeField] private AutoAimTower _autoAim;
    [SerializeField] private int poolSize = 50;

    [SerializeField] private GameObject bulletPrefab;

    private List<BulletTowerPool> pools = new List<BulletTowerPool>();
    private List<DefenseTypes_SO> gunTypes = new List<DefenseTypes_SO>();

    private BulletTowerPool bulletPool;

    [SerializeField] private Transform defaultPosition;

    private float _spawnTimes;
    private float _SpeedBullet;

    private void Start()
    {
        gunTypes = GameManager.Instance.defenseTypes;

        foreach (var item in gunTypes)
        {
            _spawnTimes = item.SpeedSpawnBullet;
            _SpeedBullet = item.BulletSpeed;
        }
    }

    private void Awake()
    {
        pools = new List<BulletTowerPool>();

        BulletTowerPool pool = gameObject.AddComponent<BulletTowerPool>();
        pool.Initialize(bulletPrefab, poolSize);
        pools.Add(pool);

        bulletPool = pool;
        StartCoroutine(SpawnBullets());
    }

    private void Spawn()
    {

        GameObject target = _autoAim.FindNearestEnemyInRange(); 

        if (target != null)
        {
            Vector3 predictedPos = _autoAim.PredictEnemyPosition(target);

            GameObject bulletInstance = bulletPool.Spawn(defaultPosition.position);

            BulletT bullet = bulletInstance.GetComponent<BulletT>();

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
