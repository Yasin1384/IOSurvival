using UnityEngine;
public class PlayerMovement : MonoBehaviour
{

    public int playerIndex;
    [SerializeField] private AutoAim _autoAim;

    public float speed;
    Rigidbody rb;

    IStrategyMove moveStrategy;

    private PlayerType_SO PlayerTypes;

    [SerializeField] private Animator _animator;

    void Awake()
    {
        var playerData = SelectedCardsHolder.SelectedPlayer;

        if (playerData == null)
        {
            playerData = PlayerManager.Instance != null
                ? PlayerManager.Instance.GetDefaultPlayer()
                : null;
        }
        speed = playerData.Speed;

        rb = GetComponent<Rigidbody>();
        moveStrategy = new JoystickMove();
    }

    public void Move(Vector3 input)
    {
        Vector3 dir = new Vector3(input.x, 0f, input.z).normalized;

        GameObject player = _autoAim.FindNearestEnemyInRange();

        if (player != null)
        {
            _autoAim.RotateToEnemy(player);
        }
        else
        {
            if (dir != Vector3.zero)
            {
                Quaternion targetRotation = Quaternion.LookRotation(dir);
                transform.rotation = Quaternion.Slerp(transform.rotation, targetRotation, Time.deltaTime * 10f);
            }
        }

        moveStrategy.Move(rb, input, speed);
    }

    public void AnimationCarecter(bool isRun, bool isIdel)
    {
        _animator.SetBool("Idel", isIdel);
        _animator.SetBool("Run", isRun);
    }
}

