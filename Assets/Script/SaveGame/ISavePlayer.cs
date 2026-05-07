using UnityEngine;

public interface ISavePlayer
{
    void WriteToSaveData(SavePlayerData data);
    void ReadFromSaveData(SavePlayerData data);
}
