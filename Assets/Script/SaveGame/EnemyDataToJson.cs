using UnityEngine;

public class EnemyDataToJson : MonoBehaviour
{
    private const string SAVE_KEY = "DATAENEMY_SAVE";

    public static void Save(SaveEnemyData data)
    {
        string json = JsonUtility.ToJson(data);
        PlayerPrefs.SetString(SAVE_KEY, json);
        PlayerPrefs.Save();
        Debug.Log("Saved: " + json);
    }

    public static SaveEnemyData Load()
    {
        if (!PlayerPrefs.HasKey(SAVE_KEY))
            return new SaveEnemyData();

        string json = PlayerPrefs.GetString(SAVE_KEY);
        SaveEnemyData data = JsonUtility.FromJson<SaveEnemyData>(json);
        Debug.Log("Loaded: " + json);
        return data;
    }
}
