using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System;

using UnityEngine;

[Serializable]
public class NPC
{
    public string Name;
    public GameObject gameObject;
    [SerializeField] private List<EventPoint> eventPoints;

    public void RunScheduledAtion(float time)
    {
        // ћы получаем все дейсвти€ которые должен сделать игрок из списка эвентов нашего игрока
        // и запускаем их
        var eventPoint = eventPoints.First(x => x.TimePoint == time);
        Debug.Log($"{Name} начинает в {time} делать {eventPoint.EventPointName}");
        eventPoints.Remove(eventPoint);
    }

    public void StopAction()
    {
        // ћетод вызываетьс€ когда просиходит остановка времени
        // все действи€ npc приостнаавливаютс€
    }

    public List<EventPoint> SearchEventPointsAtMoment(float time)
    {
        return eventPoints.Where(x => x.TimePoint == time).ToList();
    }

    [System.Serializable]
    public class EventPoint
    {
        public string EventPointName;
        public int TimePoint;

    }
}