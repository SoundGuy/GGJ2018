using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;



public class Goal : MonoBehaviour {
    public static Action<Goal> Complete;

    public AudioSource SoundOnComplete;

    protected void CompleteGoal()
    {
        if (SoundOnComplete != null)
        {
            SoundOnComplete.Play();
        }
        if (Complete != null)
            Complete(this);
    }
}
