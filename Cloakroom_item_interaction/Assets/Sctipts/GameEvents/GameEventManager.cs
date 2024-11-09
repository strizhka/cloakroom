using System;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class GameEventManager : MonoBehaviour
{
    [SerializeField] private List<GameEvent> _gameEvents;

    private EventBus _eventBus;


    private void Start()
    {
        _eventBus = FindObjectOfType<EventBus>();

        if (_eventBus != null)
        {
            _eventBus.Subscribe<TimeGoingSignal>(StartEvent);
            Debug.Log($"{gameObject.name} подписан на событие TimeGoingSignal");
        }
        else
        {
            Debug.LogWarning("EventBus не найден!");
        }
    }

    private void StartEvent(TimeGoingSignal signal)
    {
        foreach (var gameEvent in _gameEvents) 
        {
            if (gameEvent.StartTimeEvent == signal.Time)
            {
                gameEvent.PlayEvent(signal.Time);
            }

            var actors = gameEvent.SearchNPCsAtMoment(signal.Time);

            foreach (var actor in actors)
            {
                actor.RunScheduledAtion(signal.Time);
            }

        }
    }
}
