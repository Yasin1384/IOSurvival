using System.Collections;
using System.Collections.Generic;
using Unity.Properties;
using UnityEngine;
using static UnityEditor.PlayerSettings;

public class Gun : MonoBehaviour
{
    [SerializeField] private AutoAim _autoAim;
    [SerializeField] private int poolSize = 50;

    [SerializeField] private GameObject bulletPrefab;

    private List<BulletPool> pools = new List<BulletPool>();

    private BulletPool bulletPool;

    [SerializeField] private Transform defaultPosition;

    private float _spawnTimes = 0.5f;
    private float _SpeedBullet = 10f;



    private void Awake()
    {
        pools = new List<BulletPool>();

        BulletPool pool = gameObject.AddComponent<BulletPool>();
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

            Bullet bullet = bulletInstance.GetComponent<Bullet>();
            
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
