using System.IO;
using UnityEngine;

public class SaveSystem : MonoBehaviour
{
    private string filePath;

    private void Awake()
    {
        filePath = Path.Combine(Application.persistentDataPath, "saveData.json");
    }

    public void SaveAll()
    {
        var data = new SaveData();

        var savables = FindObjectsByType<MonoBehaviour>(FindObjectsSortMode.None);
        foreach (var m in savables)
        {
            if (m is ISavable savable)
                savable.WriteToSaveData(data);
        }

        string json = JsonUtility.ToJson(data, true);
        File.WriteAllText(filePath, json);
        Debug.Log("Saved to: " + filePath);
    }

    public void LoadAll()
    {
        if (!File.Exists(filePath))
        {
            Debug.LogWarning("Save file not found. Using defaults.");
            return;
        }

        string json = File.ReadAllText(filePath);
        var data = JsonUtility.FromJson<SaveData>(json);

        var savables = FindObjectsByType<MonoBehaviour>(FindObjectsSortMode.None);
        foreach (var m in savables)
        {
            if (m is ISavable savable)
                savable.ReadFromSaveData(data);
        }

        Debug.Log("Loaded from: " + filePath);
    }

    private void Start()
    {
        LoadAll();
    }
}
