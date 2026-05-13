using System.Collections.Generic;
using UnityEngine;

public class WaveManager : MonoBehaviour
{
    private List<LevelTypes_SO> Types_SO;

    int countWave;
    int countLevel;

    private void Start()
    {
        Types_SO = GameManager.Instance.LevelTypes;
        LevelUp();
    }

    private void LevelUp()
    {
        foreach (var item in Types_SO)
        {
            countWave = item.Waves;
            countLevel = item.Level;
            if (countWave > 0)
            {
                Debug.Log(countWave);

                GameManager.Instance.timerGame.RestartTimer();
            }
            else if (countWave == 0)
            {
                countLevel++;
                GameManager.Instance.WinGame();
            }
        }
    }
}
