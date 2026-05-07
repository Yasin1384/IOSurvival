using System.Drawing;
using UnityEngine;

public class XpPlayer : MonoBehaviour
{
    public static XpPlayer Instance;
    private const string SAVE_KEY = "CURRENCY_SAVE";

    public int currentLevel = 1;
    public int currentXP = 0;
    public int xpToNextLevel = 100;

    private void Awake()
    {
        if (Instance != null && Instance != this)
        {
            Destroy(gameObject);
            return;
        }
        Instance = this;
        LoadXp();
        DontDestroyOnLoad(gameObject);

    }

    public void AddXP(int amount)
    {
        currentXP += amount;

        if (currentXP >= xpToNextLevel)
        {
            currentXP -= xpToNextLevel;
            LevelUp();

        }
        SaveXp();
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

    private void SaveXp()
    {
        SaveCurrencyData data = new SaveCurrencyData();
        WriteToSaveData(data);

        string json = JsonUtility.ToJson(data);
        PlayerPrefs.SetString(SAVE_KEY, json);
        PlayerPrefs.Save();
    }

    private void LoadXp()
    {
        if (!PlayerPrefs.HasKey(SAVE_KEY))
            return;

        string json = PlayerPrefs.GetString(SAVE_KEY);
        SaveCurrencyData data = JsonUtility.FromJson<SaveCurrencyData>(json);

        ReadFromSaveData(data);
    }

    public void WriteToSaveData(SaveCurrencyData data)
    {
        data.currentLevel = currentLevel;
        data.xpToNextLevel = xpToNextLevel;
        data.currentXP = currentXP;
    }

    public void ReadFromSaveData(SaveCurrencyData data)
    {
        currentLevel = data.currentLevel;
        xpToNextLevel = data.xpToNextLevel;
        currentXP = data.currentXP;
    }
}
