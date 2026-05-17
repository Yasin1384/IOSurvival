using NUnit.Framework;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UIElements;
using static UnityEditor.Progress;

public class PlayerDamage : MonoBehaviour, ISavePlayer
{
    private const string SAVE_KEY = "DATAPLAYER_SAVE";


    private IDamageStratgy _damageStratgy;
    private int _damage;
    private PlayerType_SO playerTypes;
    private void Start()
    {
        var player = PlayerManager.Instance.spawnSuportPlayerDatas;

        for (int i = 0; i < player.Count; i++)
        {
            playerTypes = player[i].PlayerTypes;
            _damage = player[i].PlayerTypes.Hp;
        }


        SetDamage(_damageStratgy);
        SaveDamage();
    }

    private void SetDamage(IDamageStratgy damageStratgy)
    {
        _damageStratgy = new NormalDamageStratgy();
        LoadDamage();
    }

    public void TakeDamage(int baseDamage)
    {
        if (_damageStratgy == null)
        {
            return;
        }

        int finalDamage = _damageStratgy.Damage(baseDamage);
        _damage -= finalDamage;


        if (_damage <= 0)
        {
            //TODO : panel Game Over 
            GameManager.Instance.GameOver(gameObject);
            Destroy(gameObject);
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag("Enemy"))
        {
            TakeDamage(10);
        }
    }

    private void SaveDamage()
    {
        SavePlayerData data = new SavePlayerData();
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
        SavePlayerData data = JsonUtility.FromJson<SavePlayerData>(json);

        ReadFromSaveData(data);
    }

    public void WriteToSaveData(SavePlayerData data)
    {
        data.Hp = _damage;
    }

    public void ReadFromSaveData(SavePlayerData data)
    {
        _damage = data.Hp;
    }

}
