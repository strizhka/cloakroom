using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TimeChangedSignal
{
    public readonly int SecondsLeft;

    public TimeChangedSignal(int seconds)
    {
        SecondsLeft = seconds;
    }
}
