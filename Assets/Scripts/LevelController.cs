using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LevelController : MonoBehaviour {

	public static Action<LevelController> OnLevelEnd;

	public Goal[] ActionsOrder;
	private int currentOrder = 0;

	void OnEnable()
	{
		Goal.Complete += OnTouchedOcject;
	}

	void OnDisable()
	{
		Goal.Complete -= OnTouchedOcject;
	}

	void OnTouchedOcject(Goal touchObject)
	{
		if (currentOrder< ActionsOrder.Length && ActionsOrder[currentOrder] == touchObject)
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
