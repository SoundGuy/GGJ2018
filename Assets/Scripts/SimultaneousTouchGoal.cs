using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SimultaneousTouchGoal : Goal {

    public TouchObject[] SubGoals;

    private void OnEnable()
    {
        TouchObject.Touched += SubGoalTouched;
    }

    private void OnDisable()
    {
        TouchObject.Touched -= SubGoalTouched;
    }

    private void SubGoalTouched(TouchObject to)
    {
        bool complete = true;
        for (int i = 0; i < SubGoals.Length && complete; i++)
        {
            complete &= SubGoals[i].IsTouched;
        }
        if (complete)
        {
            CompleteGoal();
            Debug.Log("SimGoalComplete");
        }
    }
}
