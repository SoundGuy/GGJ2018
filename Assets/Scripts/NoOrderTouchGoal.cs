using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NoOrderTouchGoal : Goal {
    public TouchObject[] SubGoals;

    private HashSet<TouchObject> GoalsSet;

    private void OnEnable()
    {
        TouchObject.Touched += SubGoalTouched;
        GoalsSet = new HashSet<TouchObject>(SubGoals);
    }

    private void OnDisable()
    {
        TouchObject.Touched -= SubGoalTouched;
    }

    private void SubGoalTouched(TouchObject to)
    {
        GoalsSet.Remove(to);

        if (GoalsSet.Count == 0)
        {
            CompleteGoal();
            Debug.Log("NoOrderGoalComplete");
        }
    }
}
