using UnityEngine;

public class BulletTowerMilitary : MonoBehaviour
{
    private BulletTowerMillitaryPool poolTower;

    public void SetPool(BulletTowerMillitaryPool p)
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
