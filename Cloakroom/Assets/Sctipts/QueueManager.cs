using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;

public class QueueManager : MonoBehaviour
{
    public static Queue<NpcMovement> queue = new Queue<NpcMovement>();
    private static EventBus _eventBus;

    private void Awake()
    {
        _eventBus = FindObjectOfType<EventBus>();

        if (_eventBus == null)
        {
            Debug.LogError("EventBus не найден!");
        }

        else { Debug.Log("EventBus найден!"); }
    }

    public static void Enqueue(NpcMovement npc)
    {
        queue.Enqueue(npc);
        UpdateLineCount();
    }

    public static void Dequeue()
    {
        if (queue.Count > 0)
        {
            queue.Dequeue();
            UpdateLineCount();
        }
    }

    public static void RemoveNpc(NpcMovement npc)
    {
        if (queue.Contains(npc))
        {
            Queue<NpcMovement> newQueue = new Queue<NpcMovement>(queue.Where(q => q != npc));
            queue = newQueue;
        }
    }


    public static void UpdateLineCount()
    {
        int peopleInLine = queue.Count;
        _eventBus?.Invoke(new LineChangedSignal(peopleInLine));
    }



}



