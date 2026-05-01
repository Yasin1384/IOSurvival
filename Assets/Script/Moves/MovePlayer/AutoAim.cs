using UnityEngine;

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

        Vector3 directionToEnemy = targetPosition - transform.position;
        directionToEnemy.y = 0;
        directionToEnemy.Normalize();

        Vector3 enemyForward = enemy.transform.forward;
        enemyForward.y = 0;
        enemyForward.Normalize();


        float lookAheadDistance = 2.0f;
        Vector3 desiredPosition = targetPosition + enemyForward * lookAheadDistance;


        Vector3 direction = desiredPosition - transform.position;
        direction.y = 0; 

        Quaternion lookRotation = Quaternion.LookRotation(direction);

        transform.rotation = Quaternion.Lerp(
            transform.rotation,
            lookRotation,
            Time.deltaTime * RotationCharecter
        );
    }
}
