using UnityEngine;

public class BulletTower : MonoBehaviour
{
    private BulletTowerPool poolTower;

    public void SetPool(BulletTowerPool p)
    {
        poolTower = p;
    }

    private void OnTriggerEnter(Collider other)
    {
        if (poolTower != null)
        {
            poolTower.Despawn(gameObject);
        }
        poolTower.Despawn(gameObject);
    }
}
