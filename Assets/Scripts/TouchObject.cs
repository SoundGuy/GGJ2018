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
    public JointController TouchingMe { get; private set; }
    public Collider OtherTarget;
    public bool Movable;
    public bool ClickToComplete;

	public static Action<TouchObject> Touched;

	public ObjectType Type;
	public TouchType Touch;
	public UnityEvent OnTouch;

    public bool FailOnWrongTouch;

	public int TouchLimit;

    public bool IsTouched
    {
        get; protected set;
    }

    public bool IsHeld
    {
        get
        {
            return (TouchingMe != null
                && (
                    (Input.GetButton("Right Trigger") && TouchingMe.Joint == UnityEngine.XR.XRNode.RightHand) 
                    || (Input.GetButton("Left Trigger") && TouchingMe.Joint == UnityEngine.XR.XRNode.LeftHand))
                );
        }
    }

    internal void NotifyAboutSelfDestruct()
    {
        FailGoal();
    }

    private int touchCount;

    private Transform oldParent;
    private void Start()
    {
        oldParent = transform.parent;
    }
    private void Update()
    {
        if (Movable)
        {
            if (transform.parent == oldParent)
            {
                if(IsHeld)
                    transform.SetParent(TouchingMe.transform);
            }
            else
            {
                if(!IsHeld)
                    transform.SetParent(oldParent);
            }
        }
        if(ClickToComplete && IsHeld)
        {
            CompleteGoal();
        }

        if(IsHeld)
        {
            Debug.Log(string.Format("{0} is Held By {1}", name, TouchingMe.Joint));
        }
    }
    void OnTriggerEnter(Collider other)
	{
		if (!enabled)
		{
			return;
		}


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
            else if (FailOnWrongTouch)
            {
                FailGoal();
            }
			break;
		case TouchType.Head:
			if (Head != null)
			{
				CallTouched(otherGo);
			}
            else if (FailOnWrongTouch)
            {
                FailGoal();
            }
            break;
		case TouchType.LeftHand:
			if (Hand != null && Hand.Joint == UnityEngine.XR.XRNode.LeftHand)
			{
				CallTouched(otherGo);
			}
            else if (FailOnWrongTouch)
            {
                FailGoal();
            }
                break;
		case TouchType.RightHand:
			if (Hand != null && Hand.Joint == UnityEngine.XR.XRNode.RightHand)
			{
				CallTouched(otherGo);
			}
            else if (FailOnWrongTouch)
            {
                FailGoal();
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
		if (!enabled)
		{
			return;
		}
        IsTouched = false;

        if(TouchingMe!= null && other.gameObject == TouchingMe.gameObject)
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

        if(!ClickToComplete)
            CompleteGoal();

        TouchingMe = other.GetComponent<JointController>();
	}


    public void Destroy()
    {
        Destroy(gameObject);
    }
}
