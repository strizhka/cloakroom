using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TimerChangedSignal
{
    public readonly float SecondsLeft;

    public TimerChangedSignal(float seconds)
    {
        SecondsLeft = seconds;
    }
}
