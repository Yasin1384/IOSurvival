using System;
using System.Collections;
using Unity.Properties;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.UIElements;

public class Timer : MonoBehaviour
{
    [SerializeField] private int _timeLeft = 10;
    [SerializeField] private Text _timerText;

    private DateTime _endTimer;

    private void Start()
    {
        _endTimer = DateTime.Now.AddMinutes(_timeLeft);

        StartCoroutine(TimerGame());
    }
    IEnumerator TimerGame()
    {
        while (true)
        {
            TimeSpan remaining = _endTimer - DateTime.Now;

            if (remaining.TotalSeconds <= 0)
            {
                _timerText.text = "Time's up!";
                Debug.Log("Time's up!");
                yield break;
            }

            _timerText.text = $"{remaining.Minutes:00}:{remaining.Seconds:00}";

            yield return new WaitForSeconds(1f);
        }
    }
}
