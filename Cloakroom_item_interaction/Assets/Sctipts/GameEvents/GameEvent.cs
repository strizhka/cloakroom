using System;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.Events;

[CreateAssetMenu(fileName = "GameEvent", menuName = "GameEvent", order = 1)]
public class GameEvent: ScriptableObject
{
    public bool IsActive = false;
    
    public int StartTimeEvent;

    [SerializeField] private string eventName;
    [SerializeField] private List<NPC> involvedNpcs; // Список NPC, участвующих в событии

    //[SerializeField] private List<> intetactiveObj; // List<Класс придметов из ивенторя> листо придметов которые используются при остоновки времени

    public void PlayEvent(float Time)
    {
        UnityEngine.Debug.Log($"Событие {eventName} запущено ");
        IsActive = true;

        foreach(var npc in involvedNpcs)
        {
            npc.RunScheduledAtion(Time);
        }
    }

    public List<NPC> SearchNPCsAtMoment(float time)
    {
        return involvedNpcs.Where(x => x.SearchEventPointsAtMoment(time).Count != 0).ToList();
    }
}
