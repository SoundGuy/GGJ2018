﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

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
				Touched();
				OnTouch.Invoke();
			}
			break;
		case TouchType.BothHands:
			if (Hand != null)
			{
				Touched();
				OnTouch.Invoke();
			}
			break;
		case TouchType.Head:
			if (Head != null)
			{
				Touched();
				OnTouch.Invoke();
			}
			break;
		case TouchType.LeftHand:
			if (Hand != null && Hand.Joint == UnityEngine.XR.XRNode.LeftHand)
			{
				Touched();
				OnTouch.Invoke();
			}
			break;
		case TouchType.RightHand:
			if (Hand != null && Hand.Joint == UnityEngine.XR.XRNode.RightHand)
			{
				Touched();
				OnTouch.Invoke();
			}
			break;
		}
	}

	void Touched()
	{
		Debug.Log(Type);
		if (Type == ObjectType.Positive)
		{
			if (GameController.OnPositiveObjectTouched != null)
			{
				GameController.OnPositiveObjectTouched();
			}
		}
		else
		{
			if (GameController.OnNegativeObjectTouched != null)
			{
				GameController.OnNegativeObjectTouched();
			}
		}
	}
}
