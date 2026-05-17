using UnityEngine;

public class BulletTowerCannon : MonoBehaviour
{
    private BulletTowerCannonPool poolCannon;

    public void SetPoolCannon(BulletTowerCannonPool p)
    {
        poolCannon = p;
    }

    private void OnTriggerEnter(Collider other)
    {
        if (poolCannon != null)
        {
            poolCannon.Despawn(gameObject);
        }
        poolCannon.Despawn(gameObject);

    }
}
