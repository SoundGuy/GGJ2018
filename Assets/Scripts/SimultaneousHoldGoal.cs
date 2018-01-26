using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SimultaneousHoldGoal : Goal {

    public TouchObject[] SubGoals;

    private void OnEnable()
    {
        Goal.Complete += SubGoalTouched;
    }

    private void OnDisable()
    {
        Goal.Complete -= SubGoalTouched;
    }

    private void SubGoalTouched(Goal to)
    {
        bool complete = true;
        for (int i = 0; i < SubGoals.Length && complete; i++)
        {
            complete &= SubGoals[i].IsHeld;
        }
        if (complete)
        {
            CompleteGoal();
        }
    }
}
