using UnityEngine;

public class AutoAimTower : MonoBehaviour
{
    public float detectionRadius = 10f;
    public float rotationSpeed = 5f;
    public float bulletSpeed = 20f;
    private Rigidbody rb;

    private Vector3 lastEnemyPosition;
    private GameObject currentTarget;

    public AimType aimType = AimType.Direct;


    public enum AimType
    {
        Direct,
        Ballistic
    }
    void Start()
    {
        rb = GetComponent<Rigidbody>();

        if (rb == null)
        {
            Debug.LogWarning("AutoAim requires a Rigidbody.");
        }
    }

    void FixedUpdate()
    {
        currentTarget = FindNearestEnemyInRange();
        currentTarget = FindNormalEnemyInRange();

        if (currentTarget != null)
        {
            RotateToEnemy(currentTarget);
        }
    }

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

    public GameObject FindNormalEnemyInRange()
    {
        GameObject[] enemies = GameObject.FindGameObjectsWithTag("Enemy");

        GameObject farthest = null;
        float maxDistance = 0f;

        foreach (GameObject enemy in enemies)
        {
            float distance = Vector3.Distance(transform.position, enemy.transform.position);

            if (distance <= detectionRadius && distance > maxDistance)
            {
                maxDistance = distance;
                farthest = enemy;
            }
        }

        return farthest;
    }

    public void RotateToEnemy(GameObject enemy)
    {
        Vector3 predictedPosition = (aimType == AimType.Direct)
                    ? PredictDirectEnemyPosition(enemy)
                    : PredictBallisticEnemyPosition(enemy);
        Vector3 originPos = (rb != null) ? rb.position : transform.position;

        Vector3 direction = predictedPosition - originPos;

        float distanceToEnemy = direction.magnitude;

        if (distanceToEnemy < 2f)
            return;

        direction.y = 0f;

        if (direction.sqrMagnitude < 0.001f)
            return;

        direction.Normalize();

        Quaternion targetRotation = Quaternion.LookRotation(direction);

        if (rb != null)
        {
            rb.MoveRotation(
                Quaternion.Lerp(rb.rotation, targetRotation, Time.fixedDeltaTime * rotationSpeed)
            );
        }
        else
        {
            transform.rotation = Quaternion.Lerp(
                transform.rotation, targetRotation, Time.fixedDeltaTime * rotationSpeed
            );
        }
    }
    public Vector3 PredictDirectEnemyPosition(GameObject enemy)
    {
        Rigidbody enemyRb = enemy.GetComponent<Rigidbody>();
        if (enemyRb == null) return enemy.transform.position;

        Vector3 targetPos = enemy.transform.position;
        Vector3 targetVel = enemyRb.linearVelocity;

        float distance = Vector3.Distance(transform.position, targetPos);
        float timeToHit = distance / bulletSpeed;

        return targetPos + targetVel * timeToHit;
    }

    public Vector3 PredictBallisticEnemyPosition(GameObject enemy)
    {
        Rigidbody enemyRb = enemy.GetComponent<Rigidbody>();
        if (enemyRb == null) return enemy.transform.position;

        Vector3 targetPos = enemy.transform.position;
        Vector3 targetVel = enemyRb.linearVelocity;

        float distance = Vector3.Distance(transform.position, targetPos);

        float leadTime = distance / bulletSpeed * 0.5f;

        return targetPos + targetVel * leadTime;
    }
}
