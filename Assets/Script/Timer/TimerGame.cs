using System;
using System.Collections;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class TimerGame : MonoBehaviour
{
    [SerializeField] private Text _timerText;
    public int _timeLow = 2;

    private DateTime _endTimer;
    private int _lastMinuteNotified = -1;

    [SerializeField] private string sceneToLoad;

    public event Action<int> OnMinutePassed;
    public event Action OnTwoMinutesLeft;
    public event Action OnOneMinuteLeft;
    public event Action Finish;

    private Coroutine _timerCoroutine;

    private void Start()
    {
        RestartTimer();
    }

    public void RestartTimer()
    {
        ResetTimer();
        StartTimer();
    }

    public void StartTimer()
    {
        if (_timerCoroutine != null)
        {
            StopCoroutine(_timerCoroutine);
        }
        _timerCoroutine = StartCoroutine(TimeGame());
    }
    public void ResetTimer()
    {
        _endTimer = DateTime.Now.AddMinutes(_timeLow);
        _lastMinuteNotified = -1;

        if (_timerText != null)
        {
            _timerText.text = $"{_timeLow:00}:00";
        }
    }

    IEnumerator TimeGame()
    {
        _endTimer = DateTime.Now.AddMinutes(_timeLow);

        while (true)
        {
            TimeSpan remaining = _endTimer - DateTime.Now;

            if (remaining.TotalSeconds <= 0)
            {
                yield break;
            }
            _timerText.text = $"{remaining.Minutes:00}:{remaining.Seconds:00}";

            int currentMinute = remaining.Minutes;
            int currentSecond = remaining.Seconds;

            if (currentSecond == 0 && currentMinute == 0)
            {
                Debug.Log("Finished");
                Finish?.Invoke();
            }


            if (currentMinute != _lastMinuteNotified)
            {
                _lastMinuteNotified = currentMinute;
                OnMinutePassed?.Invoke(currentMinute);

                if (currentMinute == 2)
                {
                    OnTwoMinutesLeft?.Invoke();
                } 
                else if (currentMinute == 1)
                {
                    OnOneMinuteLeft?.Invoke();
                }
            }

            yield return new WaitForSeconds(1f);
        }


    }
}
