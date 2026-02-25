using UnityEngine;

public interface IStrategyMove
{
    void Move(Rigidbody rigidbody, Vector3 vector3, float speed);    
}
