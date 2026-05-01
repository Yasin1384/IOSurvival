using Unity.Properties;
using UnityEngine;

public class Gun : MonoBehaviour
{
    [SerializeField] private GameObject _bulletPrefab;
    [SerializeField] private Transform _firePoint;
    [SerializeField] private float _fireRate;
    [SerializeField] private AutoAim _autoAim;

    public float bulletSpeed = 20f;
    public float nextFireTime;


    void Update()
    {
        if (Time.time >= nextFireTime)
        {
            Shoot();
            nextFireTime = Time.time + 0.5f;
        }
    }

    void Shoot()
    {
        GameObject target = _autoAim.FindNearestEnemyInRange();

        if (target != null)
        {
            _autoAim.RotateToEnemy(target);
            GameObject bullet = Instantiate(_bulletPrefab, _firePoint.position, _firePoint.rotation);
            Rigidbody rb = bullet.GetComponent<Rigidbody>();
            rb.useGravity = false;
            rb.linearVelocity = _firePoint.forward * bulletSpeed;
        }


    }

    private void OnCollisionEnter(Collision collision)
    {        
        if (collision.gameObject.CompareTag("Enemy"))
        {

            Destroy(_bulletPrefab);
        }
    }
}
