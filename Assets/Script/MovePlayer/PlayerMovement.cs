using UnityEngine;
public class PlayerMovement : MonoBehaviour
{
    public float speed = 10f;
    Rigidbody rb;

    IStrategyMove moveStrategy;

    void Awake()
    {
        rb = GetComponent<Rigidbody>();
        moveStrategy = new JoystickMove();
    }

    public void Move(Vector3 input)
    {
        Vector3 dir = new Vector3(input.x, 0f, input.y).normalized;
        moveStrategy.Move(rb, input, speed);
    }
}

