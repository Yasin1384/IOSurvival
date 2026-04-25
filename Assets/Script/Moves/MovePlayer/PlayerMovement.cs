using Unity.Properties;
using UnityEngine;
public class PlayerMovement : MonoBehaviour
{
    [SerializeField] private AutoAim _autoAim;

    public float speed = 10f;
    Rigidbody rb;

    IStrategyMove moveStrategy;

    [SerializeField] private Animator _animator;

    void Awake()
    {
        rb = GetComponent<Rigidbody>();
        moveStrategy = new JoystickMove();
    }

    public void Move(Vector3 input)
    {
        GameObject enemy = _autoAim.FindNearestEnemyInRange();

        if (enemy != null)
        {
            _autoAim.RotateToEnemy(enemy);
        }
        Vector3 dir = new Vector3(input.x, 0f, input.z).normalized;
        moveStrategy.Move(rb, input, speed);

    }

    public void AnimationCarecter(bool isRun, bool isIdel)
    {
        _animator.SetBool("Idel", isIdel);
        _animator.SetBool("Run", isRun);

    }
}

