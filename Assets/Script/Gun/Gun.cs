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

        GameObject bulletInstance = bulletPool.Spawn(defaultPosition.position);
        Rigidbody rb = bulletInstance.GetComponent<Rigidbody>();
        rb.useGravity = false;

        if (target != null)
        {
            Vector3 directionToTarget = (target.transform.position - defaultPosition.position).normalized;
            rb.linearVelocity = directionToTarget * _SpeedBullet;
        }
        else
        {
            rb.linearVelocity = defaultPosition.forward * _SpeedBullet;
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
    private void OnCollisionEnter(Collision collision)
    {        
        if (collision.gameObject.CompareTag("Enemy"))
        {
            Die();
        }
        Die();
    }

    public void Die()
    {
        bulletPool.Despawn(gameObject);
    }
}
