using UnityEngine;

public class EnemyDamage : MonoBehaviour, ISaveEnemy
{
    private const string SAVE_KEY = "DATAENEMY_SAVE";

    private IDamageStratgy _damageStratgy;
    private EnemyType_SO EnemyType_SO;
    private int currentHp;

    private void Awake()
    {
        SaveDamage();
    }
    public void Init(EnemyType_SO data)
    {
        LoadDamage();
        EnemyType_SO = data;
        
        currentHp = data.Hp;

        _damageStratgy = new NormalDamageStratgy();
    }

    public void TakeDamage(int baseDamage)
    {
        if (_damageStratgy == null) return;

        currentHp -= baseDamage;

        if (currentHp <= 0)
        {
            GetComponent<MovementEnemy>().Die();
        }
    }
    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag("Bullet"))
        {
            TakeDamage(10);
        }
        else if (other.gameObject.CompareTag("Base") || other.gameObject.CompareTag("Player"))
        {
            TakeDamage(currentHp);
        }
    }

    private void SaveDamage()
    {
        SaveEnemyData data = new SaveEnemyData();
        WriteToSaveData(data);

        string json = JsonUtility.ToJson(data);
        PlayerPrefs.SetString(SAVE_KEY, json);
        PlayerPrefs.Save();
    }

    private void LoadDamage()
    {
        if (!PlayerPrefs.HasKey(SAVE_KEY))
            return;

        string json = PlayerPrefs.GetString(SAVE_KEY);
        SaveEnemyData data = JsonUtility.FromJson<SaveEnemyData>(json);

        ReadFromSaveData(data);
    }

    public void WriteToSaveData(SaveEnemyData data)
    {
        data.Hp = currentHp;
    }

    public void ReadFromSaveData(SaveEnemyData data)
    {
        currentHp = data.Hp;
    }
}
