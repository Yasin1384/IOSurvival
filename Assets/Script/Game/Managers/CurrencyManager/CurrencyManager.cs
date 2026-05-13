using System.Drawing;
using UnityEngine;

public class CurrencyManager : MonoBehaviour, ISavable
{
    public static CurrencyManager Instance;
    public const string SAVE_KEY = "CURRENCY_SAVE";



    public int currentLevel = 1;
    public int currentXP = 0;
    public int xpToNextLevel = 100;
    public int coins {  get; private set; }


    private void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
            LoadCurrnecy();
            DontDestroyOnLoad(gameObject);
        }
        else
        {
            Destroy(gameObject);
        }
    }
    // XP 
    public void AddXP(int amount)
    {
        currentXP += amount;

        if (currentXP >= xpToNextLevel)
        {
            currentXP -= xpToNextLevel;
            LevelUp();

        }
        SaveCurrnecy();
    }
    private void LevelUp()
    {
        currentLevel++;
        xpToNextLevel = CalculateNextXP();
    }
    private int CalculateNextXP()
    {
        return Mathf.RoundToInt(xpToNextLevel * 1.25f);
    }

    // Coin

    public void AddCoin(int amount)
    {
        coins += amount;
        SaveCurrnecy();
    }

    public bool SpendCoins(int amount)
    {
        if (coins >= amount)
        {
            coins -= amount;
            return true;
        }
        SaveCurrnecy();
        return false;
    }


    // SVE DATA JSON
    private void SaveCurrnecy()
    {
        SaveCurrencyData data = new SaveCurrencyData();
        WriteToSaveData(data);

        string json = JsonUtility.ToJson(data);
        PlayerPrefs.SetString(SAVE_KEY, json);
        PlayerPrefs.Save();
    }

    private void LoadCurrnecy()
    {
        if (!PlayerPrefs.HasKey(SAVE_KEY))
            return;

        string json = PlayerPrefs.GetString(SAVE_KEY);
        SaveCurrencyData data = JsonUtility.FromJson<SaveCurrencyData>(json);

        ReadFromSaveData(data);
    }
    public void WriteToSaveData(SaveCurrencyData data)
    {
        data.Coins = coins;
        data.currentLevel = currentLevel;
        data.xpToNextLevel = xpToNextLevel;
        data.currentXP = currentXP;
    }

    public void ReadFromSaveData(SaveCurrencyData data)
    {
        coins = data.Coins;
        currentLevel = data.currentLevel;
        xpToNextLevel = data.xpToNextLevel;
        currentXP = data.currentXP;
    }
}
