using UnityEngine;

public class BulletTowerMilitary : MonoBehaviour
{
    private BulletTowerMillitaryPool poolTower;
    private BulletTowerCannonPool poolCannon;

    public void SetPool(BulletTowerMillitaryPool p)
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
