using System.Drawing;
using UnityEngine;

public class CurrencyManager : MonoBehaviour, ISavable
{
    public static CurrencyManager Instance;

    public int coins { get; private set; }

    private const string SAVE_KEY = "CURRENCY_SAVE";


    private void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
            DontDestroyOnLoad(gameObject);
            LoadCoins();
        }
        else
        {
            Destroy(gameObject);
        }
    }

    public void AddCoin(int amount)
    {
        coins += amount;
        SaveCoins();
    }
    private void SaveCoins()
    {
        SaveCurrencyData data = new SaveCurrencyData();
        WriteToSaveData(data);

        string json = JsonUtility.ToJson(data);
        PlayerPrefs.SetString(SAVE_KEY, json);
        PlayerPrefs.Save();
    }

    private void LoadCoins()
    {
        if (!PlayerPrefs.HasKey(SAVE_KEY))
            return;

        string json = PlayerPrefs.GetString(SAVE_KEY);
        SaveCurrencyData data = JsonUtility.FromJson<SaveCurrencyData>(json);

        ReadFromSaveData(data);
    }

    public bool SpendCoins(int amount)
    {
        if (coins >= amount)
        {
            coins -= amount;
            SaveCoins();
            return true;
        }
        return false;
    }
    public void WriteToSaveData(SaveCurrencyData data)
    {
        data.Coins = coins;
    }

    public void ReadFromSaveData(SaveCurrencyData data)
    {
        coins = data.Coins;
    }
}
