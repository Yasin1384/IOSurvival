using System;
using System.Collections;
using UnityEngine;
using UnityEngine.UI;

public class TimerGame : MonoBehaviour
{
    [SerializeField] private Text _timerText;
    public int _timeLow = 2;

    private DateTime _endTimer;
    private int _lastMinuteNotified = -1;

    public event Action<int> OnMinutePassed;
    public event Action OnTwoMinutesLeft;
    public event Action OnOneMinuteLeft;
    private void Start()
    {
        _endTimer = DateTime.Now.AddMinutes(_timeLow);

        StartCoroutine(TimeGame());
    }
    IEnumerator TimeGame()
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

            int currentMinute = remaining.Minutes;

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
