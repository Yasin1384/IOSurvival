using System.Drawing;
using UnityEngine;

public class CurrencyManager : MonoBehaviour
{
    public static CurrencyManager Instance;
    public const string SAVE_KEY = "CURRENCY_SAVE";

    public System.Action<int> OnCoinsChanged;

    public int currentLevel = 1;
    public int currentXP = 0;
    public int xpToNextLevel = 100;
    public int coins { get; private set; }


    private void Awake()
    {

        if (Instance == null)
        {
            Instance = this;
            DontDestroyOnLoad(gameObject);
            LoadCurrnecy();

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

        SaveCurrency();
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
        SaveCurrency();
        OnCoinsChanged?.Invoke(coins);

    }

    public bool SpendCoins(int amount)
    {
        if (coins >= amount)
        {
            coins -= amount;
            SaveCurrency();
            OnCoinsChanged?.Invoke(coins);
            return true;
        }
        return false;
    }


    public void SaveCurrency()
    {
        SaveCurrencyData data = new SaveCurrencyData();

        data.Coins = coins;
        data.currentLevel = currentLevel;
        data.currentXP = currentXP;
        data.xpToNextLevel = xpToNextLevel;

        string json = JsonUtility.ToJson(data);

        PlayerPrefs.SetString(SAVE_KEY, json);
        PlayerPrefs.Save();
    }

    public void LoadCurrnecy()
    {
        string json = PlayerPrefs.GetString(SAVE_KEY, "");

        if (string.IsNullOrEmpty(json)) return;

        SaveCurrencyData data = JsonUtility.FromJson<SaveCurrencyData>(json);

        coins = data.Coins;
        currentLevel = data.currentLevel;
        currentXP = data.currentXP;
        xpToNextLevel = data.xpToNextLevel;
    }
}
