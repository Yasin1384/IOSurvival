using UnityEngine;

public class PlayerInput : MonoBehaviour
{
    PlayerMovement movement;

    Vector2 startTouchPos;
    Vector2 currentTouchPos;
    Vector3 input;

    void Start()
    {
        movement = GetComponent<PlayerMovement>();
    }

    void Update()
    {
        if (Input.touchCount > 0)
        {
            Touch touch = Input.GetTouch(0);

            if (touch.phase == TouchPhase.Began)
            {
                startTouchPos = touch.position;
            }
            else if (touch.phase == TouchPhase.Moved)
            {
                currentTouchPos = touch.position;
                Vector2 delta = currentTouchPos - startTouchPos;

                input = new Vector3(delta.x, 0f, delta.y).normalized;
            }
            else if (touch.phase == TouchPhase.Ended)
            {
                input = Vector3.zero;
            }
        }
        else
        {
            input = Vector3.zero;
        }
    }

    void FixedUpdate()
    {
        movement.Move(input);
    }
}