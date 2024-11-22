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
        // �� �������� ��� �������� ������� ������ ������� ����� �� ������ ������� ������ ������
        // � ��������� ��
        var eventPoint = eventPoints.First(x => x.TimePoint == time);
        Debug.Log($"{Name} �������� � {time} ������ {eventPoint.EventPointName}");
        eventPoints.Remove(eventPoint);
    }

    public void StopAction()
    {
        // ����� ����������� ����� ���������� ��������� �������
        // ��� �������� npc ������������������
        gameObject.GetComponent<NpcMovement>().StopMoving();


        EventTrigger[] triggers = UnityEngine.Object.FindObjectsByType<EventTrigger>(FindObjectsSortMode.None);

        foreach (var trigger in triggers) {
            if (trigger.itemInserted) {
                // changing eventPoints list
            }
            else {
                // changing eventPoints list
            }
        }
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