using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEditorInternal.Profiling.Memory.Experimental;
using UnityEngine;

public class Clock : MonoBehaviour
{
    private EventBus _eventBus;
    private float CurrentTime;
    private TextMeshProUGUI _clockText;

    private void Start()
    {
        _clockText = GetComponent<TextMeshProUGUI>();

        EventBus eventBus = FindObjectOfType<EventBus>();
        _eventBus = eventBus;

        if (_eventBus != null)
        {
            _eventBus.Subscribe<TimeGoingSignal>(GetTime);
        }
    }

    private void Update()
    {
        ShowTime();
    }

    private void GetTime(TimeGoingSignal signal)
    {
        CurrentTime = signal.Time;
    }

    private void ShowTime()
    {
        int hours = Mathf.FloorToInt(CurrentTime / 60);
        int minutes = Mathf.FloorToInt(CurrentTime);
        _clockText.text = string.Format("{0:00}:{1:00}", hours, minutes);
    }
}
