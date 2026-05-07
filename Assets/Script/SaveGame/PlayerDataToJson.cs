using UnityEngine;

public class PlayerDataToJson : MonoBehaviour
{
    private const string SAVE_KEY = "DATAPLAYER_SAVE";

    public static void Save(SavePlayerData data)
    {
        string json = JsonUtility.ToJson(data);
        PlayerPrefs.SetString(SAVE_KEY, json);
        PlayerPrefs.Save();
        Debug.Log("Saved: " + json);
    }

    public static SavePlayerData Load()
    {
        if (!PlayerPrefs.HasKey(SAVE_KEY))
            return new SavePlayerData();

        string json = PlayerPrefs.GetString(SAVE_KEY);
        SavePlayerData data = JsonUtility.FromJson<SavePlayerData>(json);
        Debug.Log("Loaded: " + json);
        return data;
    }
}
