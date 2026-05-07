using UnityEngine;

public interface ISaveEnemy
{
    void WriteToSaveData(SaveEnemyData data);
    void ReadFromSaveData(SaveEnemyData data);
}
