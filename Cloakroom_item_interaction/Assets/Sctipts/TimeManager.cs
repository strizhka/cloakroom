using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class TimeManager : MonoBehaviour
{
    public static bool IsTimeGoing = true;

    private EventBus _eventBus;

    private void Awake()
    {
        _eventBus = FindObjectOfType<EventBus>();

        if (_eventBus == null)
        {
            Debug.LogError("EventBus не найден!");
        }
        else
        {
            Debug.Log("EventBus найден!");
        }
    }

    private void Start()
    {
        StartCoroutine(TimeFlow());
    }

    private IEnumerator TimeFlow()
    {
        while (IsTimeGoing)
        {
            yield return new WaitForSeconds(2f);
            TimeGoing();
        }
    }

    private void TimeGoing()
    {
        _eventBus.Invoke(new TimeGoingSignal());
    }
}
