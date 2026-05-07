using Unity.Properties;
using UnityEngine;
using static UnityEngine.GraphicsBuffer;
public class PlayerMovement : MonoBehaviour, ISavePlayer
{
    private const string SAVE_KEY = "DATAPLAYER_SAVE";


    [SerializeField] private AutoAim _autoAim;

    public float speed;
    Rigidbody rb;

    IStrategyMove moveStrategy;

    [SerializeField] private Animator _animator;

    void Awake()
    {
        LoadSpeed();
        PlayerType_SO playerType = GameManager.Instance.PlayerType;
        speed = playerType.Speed;
        rb = GetComponent<Rigidbody>();
        moveStrategy = new JoystickMove();

    }

    public void Move(Vector3 input)
    {
        GameObject player = _autoAim.FindNearestEnemyInRange();

        if (player != null)
        {
            _autoAim.RotateToEnemy(player);
        }
        Vector3 dir = new Vector3(input.x, 0f, input.z).normalized;
        moveStrategy.Move(rb, input, speed);
        SaveSpeed();

    }

    public void AnimationCarecter(bool isRun, bool isIdel)
    {
        _animator.SetBool("Idel", isIdel);
        _animator.SetBool("Run", isRun);

    }

    private void SaveSpeed()
    {
        SavePlayerData data = new SavePlayerData();
        WriteToSaveData(data);

        string json = JsonUtility.ToJson(data);
        PlayerPrefs.SetString(SAVE_KEY, json);
        PlayerPrefs.Save();
    }

    private void LoadSpeed()
    {
        if (!PlayerPrefs.HasKey(SAVE_KEY))
            return;

        string json = PlayerPrefs.GetString(SAVE_KEY);
        SavePlayerData data = JsonUtility.FromJson<SavePlayerData>(json);

        ReadFromSaveData(data);
    }

    public void WriteToSaveData(SavePlayerData data)
    {
        data.Speed = speed;
    }

    public void ReadFromSaveData(SavePlayerData data)
    {
        speed = data.Speed;
    }
}

