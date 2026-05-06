using UnityEngine;

public interface ISavable
{
    void WriteToSaveData(SaveData data);
    void ReadFromSaveData(SaveData data);
}
