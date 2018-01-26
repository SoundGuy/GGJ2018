using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LevelController : MonoBehaviour {

	public static Action<LevelController> OnLevelEnd;

	public TouchObject[] ActionsOrder;
	private int currentOrder = 0;

	void OnEnable()
	{
		TouchObject.Touched += OnTouchedOcject;
	}

	void OnDisable()
	{
		TouchObject.Touched -= OnTouchedOcject;
	}

	void OnTouchedOcject(TouchObject touchObject)
	{
		if (ActionsOrder[currentOrder] == touchObject)
		{
			currentOrder++;
			if (currentOrder == ActionsOrder.Length)
			{
				FinishLevel();
			}
		}
	}

	void FinishLevel()
	{
		if (OnLevelEnd != null)
		{
			OnLevelEnd(this);
		}
	}
}
