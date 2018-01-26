using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;



public class Goal : MonoBehaviour {
    public static Action<Goal> Complete;

    protected void CompleteGoal()
    {
        if (Complete != null)
            Complete(this);
    }
}
