using System.IO;
using UnityEngine;

public class CurrencyDataJson : MonoBehaviour
{
    private const string SAVE_KEY = "CURRENCY_SAVE";

    public static void Save(SaveCurrencyData data)
    {
        string json = JsonUtility.ToJson(data);
        PlayerPrefs.SetString(SAVE_KEY, json);
        PlayerPrefs.Save();
        Debug.Log("Saved: " + json);
    }

    public static SaveCurrencyData Load()
    {
        if (!PlayerPrefs.HasKey(SAVE_KEY))
            return new SaveCurrencyData();

        string json = PlayerPrefs.GetString(SAVE_KEY);
        SaveCurrencyData data = JsonUtility.FromJson<SaveCurrencyData>(json);
        Debug.Log("Loaded: " + json);
        return data;
    }
}
