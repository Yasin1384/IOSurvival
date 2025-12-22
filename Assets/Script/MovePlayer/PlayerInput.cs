using UnityEngine;

public class PlayerInput : MonoBehaviour
{
    PlayerMovement movement;

    void Start()
    {
        movement = GetComponent<PlayerMovement>();
    }

    void Update()
    {
        Vector3 input = new Vector3(
            Input.GetAxis("Horizontal"),
            0,
            Input.GetAxis("Vertical"));

        movement.Move(input);
    }
}