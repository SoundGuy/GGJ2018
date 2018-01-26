using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using System;

public class TouchObject : Goal {

	public enum ObjectType
	{
		Positive,
		Negative
	}

	public enum TouchType
	{
		Head,
		LeftHand,
		RightHand,
		BothHands,
		All
	}



	public static Action<TouchObject> Touched;

	public ObjectType Type;
	public TouchType Touch;
	public UnityEvent OnTouch;

	public int TouchLimit;

    public bool IsTouched
    {
        get; protected set;
    }

	private int touchCount;

	void OnTriggerEnter(Collider other)
	{
		Debug.Log("OnTriggerEnter");

        //here be dragons this should be moved somewhere else becasue now the trigger enter isnt the goal necceseraly
		if (TouchLimit > 0 && ++touchCount > TouchLimit)
			return;

		var Hand = other.GetComponent<JointController>();
		var Head = other.GetComponent<Camera>();
		switch (Touch)
		{
		case TouchType.All:
			if (Head != null || Hand != null)
			{
				CallTouched();
			}
			break;
		case TouchType.BothHands:
			if (Hand != null)
			{
				CallTouched();
			}
			break;
		case TouchType.Head:
			if (Head != null)
			{
				CallTouched();
			}
			break;
		case TouchType.LeftHand:
			if (Hand != null && Hand.Joint == UnityEngine.XR.XRNode.LeftHand)
			{
				CallTouched();
			}
			break;
		case TouchType.RightHand:
			if (Hand != null && Hand.Joint == UnityEngine.XR.XRNode.RightHand)
			{
				CallTouched();
			}
			break;
		}
	}

    private void OnTriggerExit(Collider other)
    {
        IsTouched = false;
    }

    private void CallTouched ()
	{
        IsTouched = true;
		if (Touched != null)
			Touched (this);
		OnTouch.Invoke();
        CompleteGoal();
	}
}
