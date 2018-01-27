using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;



public class Goal : MonoBehaviour {
    public static Action<Goal> Complete;
    public static Action<Goal> Failed;

    public AudioSource SoundOnComplete;
    public AudioSource SoundOnFailure;

    protected void FailGoal()
    {
        if(SoundOnFailure)
        {
            SoundOnFailure.Play();
        }
        if (Failed != null)
            Failed(this);
    }

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
