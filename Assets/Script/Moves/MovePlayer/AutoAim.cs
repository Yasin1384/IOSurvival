using UnityEngine;
using static UnityEngine.GraphicsBuffer;

public class AutoAim : MonoBehaviour
{
    public float detectionRadius = 10f;
    public float RotationCharecter;

    public GameObject FindNearestEnemyInRange()
    {
        GameObject[] enemies = GameObject.FindGameObjectsWithTag("Enemy");

        GameObject nearest = null;
        float minDistance = detectionRadius;

        foreach (GameObject enemy in enemies)
        {
            float distance = Vector3.Distance(transform.position, enemy.transform.position);

            if (distance < minDistance)
            {
                minDistance = distance;
                nearest = enemy;
            }
        }

        return nearest;
    }

    public void RotateToEnemy(GameObject enemy)
    {
        Vector3 targetPosition = enemy.transform.position;
        targetPosition.y = 0;

        Vector3 directionToTarget = targetPosition - transform.position;
        directionToTarget.y = 0;
        directionToTarget.Normalize();

        Quaternion lookRotation = Quaternion.LookRotation(directionToTarget);

        transform.rotation = Quaternion.Lerp(
            transform.rotation,
            lookRotation,
            Time.deltaTime * RotationCharecter
            );
    }
}
