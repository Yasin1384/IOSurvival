using UnityEngine;

public class AutoAimTower : MonoBehaviour
{
    public float detectionRadius = 10f;
    public float rotationSpeed = 5f;
    public float bulletSpeed = 20f;

    private Rigidbody rb;

    private Vector3 lastEnemyPosition;
    private GameObject currentTarget;


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

    public void RotateToEnemy(GameObject enemy)
    {
        Vector3 predictedPosition = PredictEnemyPosition(enemy);

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

    public Vector3 PredictEnemyPosition(GameObject enemy)
    {
        Vector3 currentPos = enemy.transform.position;

        if (enemy != currentTarget)
        {
            lastEnemyPosition = currentPos;
            return currentPos;
        }

        Vector3 velocity = (currentPos - lastEnemyPosition) / Time.fixedDeltaTime;

        lastEnemyPosition = currentPos;

        float distance = Vector3.Distance(transform.position, currentPos);

        float timeToHit = distance / bulletSpeed;

        return currentPos + velocity * timeToHit;
    }
}
