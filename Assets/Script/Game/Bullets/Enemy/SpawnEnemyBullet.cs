using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpawnEnemyBullet : MonoBehaviour
{
    [SerializeField] private AutoAimEnemy _autoAim;
    [SerializeField] private int poolSize = 50;


    public int Index;

    private List<BulletEnemyPool> pools = new List<BulletEnemyPool>();

    private BulletEnemyPool bulletPool;

    [SerializeField] private Transform defaultPosition;

    private float _spawnTimes;
    private float _SpeedBullet;
    void OnEnable()
    {
        StopAllCoroutines();
        StartCoroutine(SpawnBullet());
    }
    private void Awake()
    {
        var bulletType = BulletManager.Instance.spawnBulletDatas[Index];
        _spawnTimes = bulletType.GunTypes.SpeedSpawnBullet;
        _SpeedBullet = bulletType.GunTypes.BulletSpeed;
        GameObject _gameObject = bulletType.GunTypes.BulletPrefab;

        pools = new List<BulletEnemyPool>();

        BulletEnemyPool pool = gameObject.AddComponent<BulletEnemyPool>();
        pool.Initialize(_gameObject, poolSize);
        pools.Add(pool);

        bulletPool = pool;
    }

    private void Spawn()
    {
        GameObject target = _autoAim.FindNearestPlayerInRange();

        if (target != null)
        {
            Debug.Log(target);

            Vector3 predictedPos = _autoAim.PredictPlayerPosition(target);

            GameObject bulletInstance = bulletPool.Spawn(defaultPosition.position);

            BulletEnemy bullet = bulletInstance.GetComponent<BulletEnemy>();

            bullet.SetPool(bulletPool);

            Rigidbody rb = bulletInstance.GetComponent<Rigidbody>();
            rb.useGravity = false;

            Vector3 direction = (predictedPos - defaultPosition.position).normalized;

            rb.linearVelocity = direction * _SpeedBullet;

            bulletInstance.transform.forward = direction;

        }
    }
    private IEnumerator SpawnBullet()
    {
        while (true)
        {
            yield return new WaitForSeconds(1);
            Spawn();
        }
    }
}
