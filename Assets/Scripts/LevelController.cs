using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LevelController : MonoBehaviour {

	public int[] ActionsOrder;
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
		if (ActionsOrder[currentOrder] == touchObject.GetInstanceID())
		{
			currentOrder++;
			if (currentOrder == ActionsOrder.Length)
			{
				LoadNextLevel();
			}
		}
	}

	void LoadNextLevel()
	{
	}
}
