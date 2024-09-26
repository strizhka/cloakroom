using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LineChangedSignal
{
    public readonly int PeopleInLine;

    public LineChangedSignal(int people)
    {
        PeopleInLine = people;
    }
}
