using NUnit.Framework;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class EnemyManager : MonoBehaviour, ISaveEnemy
{
    public static EnemyManager Instance { get; private set; }

    private const string SAVE_KEY = "DATAENEMY_SAVE";

    public float Speed;
    public int CurrentHp;
    public int KillBonus;

    private void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
        }
        else
        {
            Destroy(gameObject);
        }
    }

    //Damage
    public void SaveDamageEnemy()
    {
        SaveEnemyDatas();
    }
    public void LoadDamageEnemy()
    {
        LoadEnemyDatas();
    }

    //Speed
    public void SaveSpeedEnemy()
    {
        SaveEnemyDatas();
    }
    public void LoadSpeedEnemy()
    {
        LoadEnemyDatas();
    }

    //Json
    private void SaveEnemyDatas()
    {
        SaveEnemyData data = new SaveEnemyData();
        WriteToSaveData(data);

        string json = JsonUtility.ToJson(data);
        PlayerPrefs.SetString(SAVE_KEY, json);
        PlayerPrefs.Save();
    }

    private void LoadEnemyDatas()
    {
        if (!PlayerPrefs.HasKey(SAVE_KEY))
            return;

        string json = PlayerPrefs.GetString(SAVE_KEY);
        SaveEnemyData data = JsonUtility.FromJson<SaveEnemyData>(json);

        ReadFromSaveData(data);
    }

    public void WriteToSaveData(SaveEnemyData data)
    {
        data.Hp = CurrentHp;

        data.Speed = Speed;

        data.KillBonus = KillBonus;
    }

    public void ReadFromSaveData(SaveEnemyData data)
    {
        CurrentHp = data.Hp;
        Speed = data.Speed;
        KillBonus = data.KillBonus;

    }
}
