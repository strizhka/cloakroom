using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class TimeManager : MonoBehaviour
{
    public static bool IsTimeGoing = true;

    private EventBus _eventBus;
    private float _currentTime;

    private void Awake()
    {
        _eventBus = FindObjectOfType<EventBus>();

        if (_eventBus == null)
        {
            Debug.LogError("EventBus не найден!");
        }
        else
        {

            _eventBus.Subscribe<TimeGoingSignal>(GetTime);

        }
    }

    private void Start()
    {
        StartCoroutine(TimeFlow());
    }

    private IEnumerator TimeFlow()
    {
        while(true)
        {
            if (IsTimeGoing)
            {
                TimeGoing();
            }
            yield return new WaitForSeconds(2f);
        }
    }

    private void GetTime(TimeGoingSignal signal)
    {
        _currentTime = signal.Time;

    }

    private void TimeGoing()
    {
        float SecondsToAdd = _currentTime + 1;
        _eventBus.Invoke(new TimeGoingSignal(SecondsToAdd));
    }
}
