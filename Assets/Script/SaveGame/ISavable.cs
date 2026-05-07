using UnityEngine;

public interface ISavable
{
    void WriteToSaveData(SaveCurrencyData data);
    void ReadFromSaveData(SaveCurrencyData data);
}
