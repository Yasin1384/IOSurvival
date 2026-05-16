using UnityEngine;

public class BulletTower : MonoBehaviour
{
    private BulletTowerMillitaryPool poolTower;

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
