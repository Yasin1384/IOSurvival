using UnityEngine;

public class JoystickMove : IStrategyMove
{
    public void Move(Rigidbody rigidbody, Vector3 input, float speed)
    {
        Vector3 dir = input.normalized;
        rigidbody.linearVelocity = dir * speed;
    }
}
