using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEditorInternal.Profiling.Memory.Experimental;
using UnityEngine;
using UnityEngine.UI;

public class Timer : MonoBehaviour
{
    [SerializeField] private TextMeshProUGUI _timerText;
    [SerializeField] private float _maxTimer = 120f;


    private Image _timerBar;
    private EventBus _eventBus;
    private float _secondsLeft;

    private void Start()
    {
        _timerBar = GetComponent<Image>();
        EventBus eventBus = FindObjectOfType<EventBus>();
        _eventBus = eventBus;

        if (_eventBus != null)
        {
            _eventBus.Subscribe<TimeChangedSignal>(GetTime);
        }
        else
        {
            Debug.LogError("EventBus component is missing!");
        }
    }

    private void Update()
    {
        ShowTime();
    }

    private void GetTime(TimeChangedSignal signal)
    {
        _secondsLeft = signal.SecondsLeft;
    }

    private void ShowTime()
    {
        int minutes = Mathf.FloorToInt(_secondsLeft / 60);
        int seconds = Mathf.FloorToInt(_secondsLeft % 60);
        _timerText.text = string.Format("{0:00}:{1:00}", minutes, seconds);
        _timerBar.fillAmount = _secondsLeft / _maxTimer;
    }

    private void TimeStopped()
    {
        //stop time
        if (_secondsLeft > 5)
        {
            _secondsLeft -= ((int)Time.deltaTime);
        }
        else if (_secondsLeft > 0)
        {
            _timerText.color = Color.red;
            _secondsLeft -= ((int)Time.deltaTime);
        }
        else
        {
            _secondsLeft = 0;
            //start time 
        }
    }
}
