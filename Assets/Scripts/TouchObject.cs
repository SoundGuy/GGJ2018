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
		All,
        Other
	}
    public string InputAxisName;
    public GameObject TouchingMe { get; private set; }
    public Collider OtherTarget;
    public bool Movable;

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

    private Transform oldParent;

    private void Update()
    {
        if (Movable)
        {
            if (oldParent == null && (Input.GetButton("Right Trigger") || Input.GetButton("Left Trigger")) && TouchingMe != null)
            {
                oldParent = transform.parent;
                transform.SetParent(TouchingMe.transform);
            }
            else
            {
                transform.SetParent(oldParent);
                oldParent = null;
            }
        }
    }
    void OnTriggerEnter(Collider other)
	{
		Debug.Log("OnTriggerEnter");

        //here be dragons this should be moved somewhere else becasue now the trigger enter isnt the goal necceseraly
		if (TouchLimit > 0 && ++touchCount > TouchLimit)
			return;
        var otherGo = other.gameObject;
		var Hand = other.GetComponent<JointController>();
		var Head = other.GetComponent<Camera>();
		switch (Touch)
		{
		case TouchType.All:
			if (Head != null || Hand != null)
			{
				CallTouched(otherGo);
			}
			break;
		case TouchType.BothHands:
			if (Hand != null)
			{
				CallTouched(otherGo);
			}
			break;
		case TouchType.Head:
			if (Head != null)
			{
				CallTouched(otherGo);
			}
			break;
		case TouchType.LeftHand:
			if (Hand != null && Hand.Joint == UnityEngine.XR.XRNode.LeftHand)
			{
				CallTouched(otherGo);
			}
			break;
		case TouchType.RightHand:
			if (Hand != null && Hand.Joint == UnityEngine.XR.XRNode.RightHand)
			{
				CallTouched(otherGo);
			}
			break;
        case TouchType.Other:
            if(other == OtherTarget)
            {
                CallTouched(otherGo);
            }
            break;
		}
	}

    private void OnTriggerExit(Collider other)
    {
        IsTouched = false;

        if(other.gameObject == TouchingMe)
        {
            TouchingMe = null;
        }
    }

    private void CallTouched (GameObject other)
	{
        IsTouched = true;
		if (Touched != null)
			Touched (this);
		OnTouch.Invoke();
        CompleteGoal();
        TouchingMe = other;
	}


}
