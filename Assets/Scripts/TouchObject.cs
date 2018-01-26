using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using System;

public class TouchObject : MonoBehaviour {

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


	void OnTriggerEnter(Collider other)
	{
		Debug.Log("OnTriggerEnter");
		var Hand = other.GetComponent<JointController>();
		var Head = other.GetComponent<Camera>();
		switch (Touch)
		{
		case TouchType.All:
			if (Head != null || Hand != null)
			{
				Touched(this);
				OnTouch.Invoke();
			}
			break;
		case TouchType.BothHands:
			if (Hand != null)
			{
				Touched(this);
				OnTouch.Invoke();
			}
			break;
		case TouchType.Head:
			if (Head != null)
			{
				Touched(this);
				OnTouch.Invoke();
			}
			break;
		case TouchType.LeftHand:
			if (Hand != null && Hand.Joint == UnityEngine.XR.XRNode.LeftHand)
			{
				Touched(this);
				OnTouch.Invoke();
			}
			break;
		case TouchType.RightHand:
			if (Hand != null && Hand.Joint == UnityEngine.XR.XRNode.RightHand)
			{
				Touched(this);
				OnTouch.Invoke();
			}
			break;
		}
	}
}
