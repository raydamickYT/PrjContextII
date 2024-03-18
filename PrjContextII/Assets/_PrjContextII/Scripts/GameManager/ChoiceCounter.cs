using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ChoiceCounter
{
    public int DayIndex = 0;
    public ChoiceCounter()
    {
        // DayIndex = _DayIndex;
    }

    public int AddDay(int DayIndex)
    {
        return DayIndex++;
    }

}
