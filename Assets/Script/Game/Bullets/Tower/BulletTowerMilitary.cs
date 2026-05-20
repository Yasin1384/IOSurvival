using UnityEngine;

public class BulletTowerMilitary : MonoBehaviour
{
    private BulletTowerPool poolTower;

    public void SetPool(BulletTowerPool p)
    {
        poolTower = p;
    }
    private void OnTriggerEnter(Collider other)
    {
        poolTower.Despawn(gameObject);
        if (poolTower != null)
        {
            poolTower.Despawn(gameObject);
        }
    }
}
